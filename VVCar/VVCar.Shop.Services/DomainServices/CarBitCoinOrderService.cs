using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.BaseData.Domain;
using VVCar.BaseData.Domain.Entities;
using VVCar.BaseData.Domain.Services;
using VVCar.Shop.Domain.Entities;
using VVCar.Shop.Domain.Enums;
using VVCar.Shop.Domain.Filters;
using VVCar.Shop.Domain.Services;
using VVCar.VIP.Domain.Dtos;
using VVCar.VIP.Domain.Services;
using YEF.Core;
using YEF.Core.Data;
using YEF.Core.Domain;

namespace VVCar.Shop.Services.DomainServices
{
    /// <summary>
    /// 车比特订单领域服务
    /// </summary>
    public class CarBitCoinOrderService : DomainServiceBase<IRepository<CarBitCoinOrder>, CarBitCoinOrder, Guid>, ICarBitCoinOrderService
    {
        public CarBitCoinOrderService()
        {
        }
        #region properties

        IRepository<MakeCodeRule> MakeCodeRuleRepo { get => UnitOfWork.GetRepository<IRepository<MakeCodeRule>>(); }

        ICouponService CouponService { get => ServiceLocator.Instance.GetService<ICouponService>(); }

        IWeChatService WeChatService { get => ServiceLocator.Instance.GetService<IWeChatService>(); }

        ISystemSettingService SystemSettingService { get => ServiceLocator.Instance.GetService<ISystemSettingService>(); }

        ICarBitCoinMemberService MemberService { get => ServiceLocator.Instance.GetService<ICarBitCoinMemberService>(); }

        IRepository<CarBitCoinOrderPaymentDetails> OrderPaymentDetailsRepo { get => UnitOfWork.GetRepository<IRepository<CarBitCoinOrderPaymentDetails>>(); }

        IRepository<User> UserRepo { get => UnitOfWork.GetRepository<IRepository<User>>(); }

        IRepository<CarBitCoinMember> CarBitCoinMemberRepo { get => UnitOfWork.GetRepository<IRepository<CarBitCoinMember>>(); }

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
                var rule = MakeCodeRuleRepo.GetQueryable().Where(t => t.Code == "CarBitCoinOrder" && t.IsAvailable).FirstOrDefault();
                if (rule != null)
                {
                    rule.CurrentValue = 0;
                    MakeCodeRuleRepo.Update(rule);
                }
            }
            do
            {
                newTradeNo = makeCodeRuleService.GenerateCode("CarBitCoinOrder", DateTime.Now);
                existNo = Repository.Exists(t => t.Code == newTradeNo);
            } while (existNo);
            return newTradeNo;
        }

        public override CarBitCoinOrder Add(CarBitCoinOrder entity)
        {
            if (entity == null || entity.CarBitCoinOrderItemList == null || entity.CarBitCoinOrderItemList.Count < 1)
                return null;
            entity.ID = Util.NewID();
            entity.Index = GetIndex();
            if (string.IsNullOrEmpty(entity.Code))
                entity.Code = GetTradeNo();
            var existNo = Repository.Exists(t => t.Code == entity.Code);
            if (existNo)
                throw new DomainException($"创建订单失败，订单号{entity.Code}已存在");
            entity.CreatedDate = DateTime.Now;
            entity.CarBitCoinOrderItemList.ForEach(t =>
            {
                t.ID = Util.NewID();
                t.CarBitCoinOrderID = entity.ID;
            });
            entity.MerchantID = AppContext.CurrentSession.MerchantID;

            CarBitCoinOrder result = null;
            UnitOfWork.BeginTransaction();
            try
            {
                RecountMoney(entity);
                result = base.Add(entity);
                UnitOfWork.CommitTransaction();
            }
            catch (Exception e)
            {
                UnitOfWork.RollbackTransaction();
                throw new DomainException(e.Message);
            }

            return result;
        }

        public void RecountMoney(CarBitCoinOrder entity, bool isNotify = false)
        {
            if (entity == null)
                return;
            if (entity.CarBitCoinOrderItemList == null || entity.CarBitCoinOrderItemList.Count < 1)
                entity.Money = 0;
            else
            {
                decimal totalMoney = 0;
                entity.CarBitCoinOrderItemList.ForEach(t =>
                {
                    totalMoney += t.Quantity * t.PriceSale;
                });
                entity.Money = totalMoney;
            }

            decimal paymoney = 0;
            var paymentdetails = OrderPaymentDetailsRepo.GetQueryable(false).Where(t => t.CarBitCoinOrderCode == entity.Code).ToList();
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
                var cardItems = entity.CarBitCoinOrderItemList.Where(t => t.ProductType == ECarBitCoinProductType.Engine).ToList();
                if (cardItems != null && cardItems.Count == entity.CarBitCoinOrderItemList.Count)
                    entity.Status = EOrderStatus.Finish;
                if (isNotify)
                {
                    SendWeChatNotify(entity);
                    //SendOrderWeChatNotify(entity);

                    if (cardItems != null && cardItems.Count > 0)
                    {
                        if (entity.MemberID.HasValue)
                        {
                            var cbcmember = CarBitCoinMemberRepo.GetByKey(entity.MemberID.Value, false);
                            if (cbcmember != null)
                            {
                                var horsepower = cardItems.GroupBy(g => 1).Select(t => t.Sum(s => s.Horsepower)).FirstOrDefault();
                                MemberService.ChangeHorsepowerCarBitCoin(cbcmember.ID, string.Empty, ECarBitCoinRecordType.BuyEngine, horsepower, 0, entity.Code, string.Empty);
                            }
                        }
                    }
                }
            }
        }

        public bool RecountMoneySave(string code, bool isNotify = false)
        {
            if (string.IsNullOrEmpty(code))
                return false;
            var entity = Repository.GetInclude(t => t.CarBitCoinOrderItemList).FirstOrDefault(t => t.Code == code);
            if (entity == null)
                return false;
            UnitOfWork.BeginTransaction();
            try
            {
                RecountMoney(entity, isNotify);
                Repository.Update(entity);
                UnitOfWork.CommitTransaction();
                return true;
            }
            catch (Exception e)
            {
                UnitOfWork.RollbackTransaction();
                throw e;
            }
        }

        /// <summary>
        /// 发送下单成功通知给顾客
        /// </summary>
        /// <param name="order"></param>
        private void SendWeChatNotify(CarBitCoinOrder carBitCoinOrder)
        {
            if (carBitCoinOrder == null || carBitCoinOrder.CarBitCoinOrderItemList == null || carBitCoinOrder.CarBitCoinOrderItemList.Count < 1 || string.IsNullOrEmpty(carBitCoinOrder.OpenID))
                return;
            var message = new WeChatTemplateMessageDto
            {
                touser = carBitCoinOrder.OpenID,
                template_id = SystemSettingService.GetSettingValue(SysSettingTypes.WXMsg_OrderSuccess),
                url = $"{AppContext.Settings.SiteDomain}/Mobile/CarBitCoin?mch={AppContext.CurrentSession.CompanyCode}",
                data = new System.Dynamic.ExpandoObject(),
            };
            var productNames = string.Empty;
            var totalQuantity = 0;
            var cardItems = carBitCoinOrder.CarBitCoinOrderItemList.Where(t => t.ProductType == ECarBitCoinProductType.Engine).ToList();

            carBitCoinOrder.CarBitCoinOrderItemList.ForEach(t =>
            {
                if (string.IsNullOrEmpty(productNames))
                    productNames += t.ProductName;
                else
                    productNames += $"、{t.ProductName}";
                totalQuantity += t.Quantity;
            });

            var remark = "请等待商家发货";
            if (cardItems != null && cardItems.Count == totalQuantity)
                remark = "您购买的引擎已转化为对应的车比特马力";
            else if (cardItems != null && cardItems.Count > 0 && cardItems.Count < totalQuantity)
            {
                remark = "您购买的商品请等待商家发货，引擎已转化为对应的车比特马力";
            }

            message.data.first = new WeChatTemplateMessageDto.MessageData("您已成功下单");
            message.data.keyword1 = new WeChatTemplateMessageDto.MessageData(carBitCoinOrder.Code);
            message.data.keyword2 = new WeChatTemplateMessageDto.MessageData(productNames);
            message.data.keyword3 = new WeChatTemplateMessageDto.MessageData(totalQuantity.ToString());
            message.data.keyword4 = new WeChatTemplateMessageDto.MessageData(carBitCoinOrder.Money.ToString("0.00"));
            message.data.keyword5 = new WeChatTemplateMessageDto.MessageData("微信");
            message.data.remark = new WeChatTemplateMessageDto.MessageData(remark);
            WeChatService.SendWeChatNotifyAsync(message);
        }

        /// <summary>
        /// 发送新订单提醒给商户
        /// </summary>
        /// <param name="order"></param>
        private void SendOrderWeChatNotify(CarBitCoinOrder carBitCoinOrder)
        {
            if (carBitCoinOrder == null || carBitCoinOrder.CarBitCoinOrderItemList == null || carBitCoinOrder.CarBitCoinOrderItemList.Count < 1)
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
            var cardItems = carBitCoinOrder.CarBitCoinOrderItemList.Where(t => t.ProductType == ECarBitCoinProductType.Goods).ToList();
            if (cardItems == null || cardItems.Count < 1)
                return;
            cardItems.ForEach(t =>
            {
                if (string.IsNullOrEmpty(productNames))
                    productNames += t.ProductName;
                else
                    productNames += $"、{t.ProductName}";
            });

            message.data.first = new WeChatTemplateMessageDto.MessageData("您有新的车比特商城订单，请及时处理");
            message.data.keyword1 = new WeChatTemplateMessageDto.MessageData(carBitCoinOrder.Code);
            message.data.keyword2 = new WeChatTemplateMessageDto.MessageData(carBitCoinOrder.CreatedDate.ToString("yyyy-MM-dd HH:mm:ss"));
            message.data.remark = new WeChatTemplateMessageDto.MessageData($"订单项：{productNames}。在管理后台查看订单详情，请及时发货");

            users.ForEach(t =>
            {
                message.touser = t.OpenID;
                WeChatService.SendWeChatNotifyAsync(message);
            });
        }

        public override bool Update(CarBitCoinOrder entity)
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

        public IEnumerable<CarBitCoinOrder> Search(OrderFilter filter, out int totalCount)
        {
            var queryable = Repository.GetInclude(t => t.CarBitCoinOrderItemList, false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID);
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
