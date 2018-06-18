﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.BaseData.Domain;
using VVCar.BaseData.Domain.Entities;
using VVCar.BaseData.Domain.Services;
using VVCar.BaseData.Services;
using VVCar.Shop.Domain.Dtos;
using VVCar.Shop.Domain.Entities;
using VVCar.Shop.Domain.Enums;
using VVCar.Shop.Domain.Filters;
using VVCar.Shop.Domain.Services;
using VVCar.VIP.Domain.Dtos;
using VVCar.VIP.Domain.Enums;
using VVCar.VIP.Domain.Services;
using YEF.Core;
using YEF.Core.Data;
using YEF.Core.Domain;
using YEF.Utility;

namespace VVCar.Shop.Services.DomainServices
{
    /// <summary>
    /// 订单领域服务
    /// </summary>
    public class OrderService : DomainServiceBase<IRepository<Order>, Order, Guid>, IOrderService
    {
        public OrderService()
        {
        }

        #region properties

        IRepository<MakeCodeRule> MakeCodeRuleRepo { get => UnitOfWork.GetRepository<IRepository<MakeCodeRule>>(); }

        ICouponService CouponService { get => ServiceLocator.Instance.GetService<ICouponService>(); }

        IWeChatService WeChatService { get => ServiceLocator.Instance.GetService<IWeChatService>(); }

        ISystemSettingService SystemSettingService { get => ServiceLocator.Instance.GetService<ISystemSettingService>(); }

        IMemberService MemberService { get => ServiceLocator.Instance.GetService<IMemberService>(); }

        IShoppingCartService ShoppingCartService { get => ServiceLocator.Instance.GetService<IShoppingCartService>(); }

        IProductService ProductService { get => ServiceLocator.Instance.GetService<IProductService>(); }

        IRepository<OrderPaymentDetails> OrderPaymentDetailsRepo { get => UnitOfWork.GetRepository<IRepository<OrderPaymentDetails>>(); }

        IRepository<User> UserRepo { get => UnitOfWork.GetRepository<IRepository<User>>(); }

        #endregion

        public override bool Delete(Guid key)
        {
            var entity = Repository.GetByKey(key);
            if (entity == null)
                throw new DomainException("数据不存在");
            if (entity.Status == EOrderStatus.Delivered)
                throw new DomainException("订单已发货");
            if (entity.Status == EOrderStatus.Finish)
                throw new DomainException("订单已完成");
            if (entity.Status == EOrderStatus.PayUnshipped)
                throw new DomainException("订单已付款");
            if (entity.Status == EOrderStatus.UnEnough)
                throw new DomainException("订单付款不足");
            entity.IsDeleted = true;
            entity.LastUpdatedDate = DateTime.Now;
            entity.LastUpdatedUserID = AppContext.CurrentSession.UserID;
            entity.LastUpdatedUser = AppContext.CurrentSession.UserName;
            return Repository.Update(entity) > 0;
        }

        private int GetIndex()
        {
            var index = 1;
            var indexList = Repository.GetQueryable(false).Select(t => t.Index);
            if (indexList.Count() > 0)
                index = indexList.Max() + 1;
            return index;
        }

        public string GetTradeNo()
        {
            var newTradeNo = string.Empty;
            var existNo = false;
            var makeCodeRuleService = ServiceLocator.Instance.GetService<IMakeCodeRuleService>();
            var entity = Repository.GetQueryable(false).OrderByDescending(t => t.CreatedDate).FirstOrDefault();
            if (entity != null && entity.CreatedDate.Date != DateTime.Now.Date)
            {
                var rule = MakeCodeRuleRepo.GetQueryable().Where(t => t.Code == "Order" && t.IsAvailable).FirstOrDefault();
                if (rule != null)
                {
                    rule.CurrentValue = 0;
                    MakeCodeRuleRepo.Update(rule);
                }
            }
            do
            {
                newTradeNo = makeCodeRuleService.GenerateCode("Order", DateTime.Now);
                existNo = Repository.Exists(t => t.Code == newTradeNo);
            } while (existNo);
            return newTradeNo;
        }

        public override Order Add(Order entity)
        {
            if (entity == null || entity.OrderItemList == null || entity.OrderItemList.Count < 1)
                return null;
            entity.ID = Util.NewID();
            entity.Index = GetIndex();
            if (string.IsNullOrEmpty(entity.Code))
                entity.Code = GetTradeNo();
            var existNo = Repository.Exists(t => t.Code == entity.Code);
            if (existNo)
                throw new DomainException($"创建订单失败，订单号{entity.Code}已存在");
            entity.CreatedDate = DateTime.Now;
            entity.OrderItemList.ForEach(t =>
            {
                t.ID = Util.NewID();
                t.OrderID = entity.ID;
            });
            entity.MerchantID = AppContext.CurrentSession.MerchantID;

            Order result = null;
            UnitOfWork.BeginTransaction();
            try
            {
                var cardItems = entity.OrderItemList.Where(t => t.ProductType == EProductType.MemberCard).ToList();
                var couponTemplateIDs = cardItems.Select(t => t.GoodsID).ToList();
                if (couponTemplateIDs != null && couponTemplateIDs.Count > 0)
                    ReceiveCoupons(couponTemplateIDs, entity.OpenID);
                //if (cardItems.Count == entity.OrderItemList.Count)
                //    entity.Status = EOrderStatus.Finish;
                RecountMoney(entity);
                result = base.Add(entity);
                //try
                //{
                //    MemberService.AdjustMemberPoint(result.OpenID, EMemberPointType.MemberConsume, (double)result.Money);
                //}
                //catch
                //{
                //}
                ShoppingCartService.ClearShoppingCart(result.OpenID);
                UnitOfWork.CommitTransaction();
            }
            catch (Exception e)
            {
                UnitOfWork.RollbackTransaction();
                throw new DomainException(e.Message);
            }

            return result;
        }

        public void RecountMoney(Order entity, bool isNotify = false)
        {
            if (entity == null)
                return;
            if (entity.OrderItemList == null || entity.OrderItemList.Count < 1)
                entity.Money = 0;
            else
            {
                decimal totalMoney = 0;
                entity.OrderItemList.ForEach(t =>
                {
                    //t.Money = t.Quantity * t.PriceSale;
                    totalMoney += t.Quantity * t.PriceSale;
                });
                entity.Money = totalMoney;
            }

            decimal paymoney = 0;
            var paymentdetails = OrderPaymentDetailsRepo.GetQueryable(false).Where(t => t.OrderCode == entity.Code).ToList();
            if (paymentdetails != null && paymentdetails.Count > 0)
            {
                paymentdetails.ForEach(t =>
                {
                    paymoney += t.PayMoney;
                });
            }
            entity.ReceivedMoney = paymoney;
            entity.StillOwedMoney = entity.Money - entity.ReceivedMoney;
            if (entity.StillOwedMoney < 0)
                entity.StillOwedMoney = 0;

            if (entity.ReceivedMoney > 0 && entity.ReceivedMoney < entity.Money)
                entity.Status = EOrderStatus.UnEnough;
            else if (entity.ReceivedMoney >= entity.Money)
            {
                entity.Status = EOrderStatus.PayUnshipped;
                if (isNotify)
                {
                    StockOut(entity.OrderItemList.Where(t => t.ProductType == EProductType.Goods).ToList());
                    SendWeChatNotify(entity);
                    SendOrderWeChatNotify(entity);
                }
            }
        }

        public bool RecountMoneySave(string code, bool isNotify = false)
        {
            if (string.IsNullOrEmpty(code))
                return false;
            var entity = Repository.GetInclude(t => t.OrderItemList).FirstOrDefault(t => t.Code == code);
            if (entity == null)
                return false;
            RecountMoney(entity, isNotify);
            return Repository.Update(entity) > 0;
        }

        private bool StockOut(List<OrderItem> orderItemList)
        {
            if (orderItemList == null || orderItemList.Count < 1)
                return false;
            orderItemList.ForEach(t =>
            {
                ProductService.StockOutIn(new StockRecord
                {
                    ProductID = t.GoodsID,
                    Quantity = -t.Quantity,
                    StockRecordType = EStockRecordType.Out,
                    Reason = "商城下单",
                    StaffID = AppContext.CurrentSession.UserID,
                    StaffName = AppContext.CurrentSession.UserName,
                    OrderID = t.OrderID,
                    Source = EStockRecordSource.WeChat,
                });
            });
            return true;
        }

        private void ReceiveCoupons(List<Guid> couponTemplateIDs, string openId)
        {
            if (string.IsNullOrEmpty(openId) || couponTemplateIDs == null || couponTemplateIDs.Count < 1)
                throw new DomainException("参数错误");
            CouponService.ReceiveCouponsAtcion(new ReceiveCouponDto
            {
                ReceiveOpenID = openId,
                CouponTemplateIDs = couponTemplateIDs,
                ReceiveChannel = "微信",
            });
        }

        /// <summary>
        /// 发送下单成功通知给顾客
        /// </summary>
        /// <param name="order"></param>
        private void SendWeChatNotify(Order order)
        {
            if (order == null || order.OrderItemList == null || order.OrderItemList.Count < 1 || string.IsNullOrEmpty(order.OpenID))
                return;
            var message = new WeChatTemplateMessageDto
            {
                touser = order.OpenID,
                template_id = SystemSettingService.GetSettingValue(SysSettingTypes.WXMsg_OrderSuccess),
                url = $"{AppContext.Settings.SiteDomain}/Mobile/Customer/MyOrder?mch={AppContext.CurrentSession.CompanyCode}",
                data = new System.Dynamic.ExpandoObject(),
            };
            var productNames = string.Empty;
            var totalQuantity = 0;
            var cardItems = order.OrderItemList.Where(t => t.ProductType == EProductType.MemberCard).ToList();

            order.OrderItemList.ForEach(t =>
            {
                if (string.IsNullOrEmpty(productNames))
                    productNames += t.ProductName;
                else
                    productNames += $"、{t.ProductName}";
                totalQuantity += t.Quantity;
            });

            var remark = "请等待商家发货";
            if (cardItems != null && cardItems.Count == totalQuantity)
            {
                remark = "您购买的会员卡已添加到您的卡包";
                message.url = $"{AppContext.Settings.SiteDomain}/Mobile/Customer/MemberCard?mch={AppContext.CurrentSession.CompanyCode}";
            }
            else if (cardItems != null && cardItems.Count > 0 && cardItems.Count < totalQuantity)
            {
                remark = "您购买的商品请等待商家发货，会员卡已添加到您的卡包";
            }

            message.data.first = new WeChatTemplateMessageDto.MessageData("您已成功下单");
            message.data.keyword1 = new WeChatTemplateMessageDto.MessageData(order.Code);
            message.data.keyword2 = new WeChatTemplateMessageDto.MessageData(productNames);
            message.data.keyword3 = new WeChatTemplateMessageDto.MessageData(totalQuantity.ToString());
            message.data.keyword4 = new WeChatTemplateMessageDto.MessageData(order.Money.ToString("0.00"));
            message.data.keyword5 = new WeChatTemplateMessageDto.MessageData("微信");
            message.data.remark = new WeChatTemplateMessageDto.MessageData(remark);
            WeChatService.SendWeChatNotifyAsync(message);
        }

        /// <summary>
        /// 发送新订单提醒给商户
        /// </summary>
        /// <param name="order"></param>
        private void SendOrderWeChatNotify(Order order)
        {
            if (order == null || order.OrderItemList == null || order.OrderItemList.Count < 1)
                return;
            var users = UserRepo.GetQueryable(false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID && t.OpenID != null && t.OpenID != "").ToList();
            if (users == null || users.Count < 1)
                return;

            var message = new WeChatTemplateMessageDto
            {
                touser = "",
                template_id = SystemSettingService.GetSettingValue(SysSettingTypes.WXMsg_OrderRemind),
                url = "",
                data = new System.Dynamic.ExpandoObject(),
            };
            var productNames = string.Empty;
            var cardItems = order.OrderItemList.Where(t => t.ProductType == EProductType.Goods).ToList();
            if (cardItems == null || cardItems.Count < 1)
                return;
            cardItems.ForEach(t =>
            {
                if (string.IsNullOrEmpty(productNames))
                    productNames += t.ProductName;
                else
                    productNames += $"、{t.ProductName}";
            });

            message.data.first = new WeChatTemplateMessageDto.MessageData("您有新订单，请及时处理");
            message.data.keyword1 = new WeChatTemplateMessageDto.MessageData(order.Code);
            message.data.keyword2 = new WeChatTemplateMessageDto.MessageData(order.CreatedDate.ToString("yyyy-MM-dd HH:mm:ss"));
            message.data.remark = new WeChatTemplateMessageDto.MessageData($"订单项：{productNames}。在管理后台查看订单详情，请及时发货");

            users.ForEach(t =>
            {
                message.touser = t.OpenID;
                WeChatService.SendWeChatNotifyAsync(message);
            });
        }

        public override bool Update(Order entity)
        {
            if (entity == null)
                return false;
            var order = Repository.GetByKey(entity.ID);
            if (order == null)
                return false;
            order.ExpressNumber = entity.ExpressNumber;
            order.Status = entity.Status;
            order.LastUpdatedDate = DateTime.Now;
            order.LastUpdatedUserID = AppContext.CurrentSession.UserID;
            order.LastUpdatedUser = AppContext.CurrentSession.UserName;
            return Repository.Update(order) > 0;
        }

        public IEnumerable<Order> Search(OrderFilter filter, out int totalCount)
        {
            var queryable = Repository.GetInclude(t => t.OrderItemList, false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID);
            if (!string.IsNullOrEmpty(filter.OpenID))
                queryable = queryable.Where(t => t.OpenID == filter.OpenID);
            if (!string.IsNullOrEmpty(filter.TradeNo))
                queryable = queryable.Where(t => t.Code.Contains(filter.TradeNo));
            if (!string.IsNullOrEmpty(filter.LinkMan))
                queryable = queryable.Where(t => t.LinkMan.Contains(filter.LinkMan));
            if (!string.IsNullOrEmpty(filter.Phone))
                queryable = queryable.Where(t => t.Phone.Contains(filter.Phone));
            if (!string.IsNullOrEmpty(filter.Address))
                queryable = queryable.Where(t => t.Address.Contains(filter.Address));
            if (!string.IsNullOrEmpty(filter.ExpressNumber))
                queryable = queryable.Where(t => t.ExpressNumber.Contains(filter.ExpressNumber));
            if (!string.IsNullOrEmpty(filter.TNoLMPAddEN))
                queryable = queryable.Where(t => t.Code.Contains(filter.TNoLMPAddEN) || t.LinkMan.Contains(filter.TNoLMPAddEN) || t.Address.Contains(filter.TNoLMPAddEN) || t.Phone.Contains(filter.TNoLMPAddEN) || t.ExpressNumber.Contains(filter.TNoLMPAddEN));
            totalCount = queryable.Count();
            if (filter.Start.HasValue && filter.Limit.HasValue)
                queryable = queryable.OrderByDescending(t => t.CreatedDate).Skip(filter.Start.Value).Take(filter.Limit.Value);
            return queryable.OrderByDescending(t => t.CreatedDate).ToArray();
        }
    }
}
