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
using VVCar.VIP.Domain.Enums;
using VVCar.VIP.Domain.Services;
using YEF.Core;
using YEF.Core.Data;
using YEF.Core.Domain;

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

        IRepository<MakeCodeRule> MakeCodeRuleRepo { get; set; }

        ICouponService CouponService { get => ServiceLocator.Instance.GetService<ICouponService>(); }

        IWeChatService WeChatService { get => ServiceLocator.Instance.GetService<IWeChatService>(); }

        ISystemSettingService SystemSettingService { get => ServiceLocator.Instance.GetService<ISystemSettingService>(); }

        IMemberService MemberService { get => ServiceLocator.Instance.GetService<IMemberService>(); }

        #endregion

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
                var couponTemplateIDs = cardItems.Select(t => t.ProductID).ToList();
                if (couponTemplateIDs != null && couponTemplateIDs.Count > 0)
                    ReceiveCoupons(couponTemplateIDs, entity.OpenID);
                if (cardItems.Count == entity.OrderItemList.Count)
                    entity.Status = EOrderStatus.Finish;
                result = base.Add(entity);
                try
                {
                    MemberService.AdjustMemberPoint(result.OpenID, EMemberPointType.MemberConsume, (int)result.Money);
                }
                catch
                {
                }
                SenWeChatNotify(result);
                UnitOfWork.CommitTransaction();
            }
            catch (Exception e)
            {
                UnitOfWork.RollbackTransaction();
                throw new DomainException(e.Message);
            }

            return result;
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

        private void SenWeChatNotify(Order order)
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

        public override bool Update(Order entity)
        {
            if (entity == null)
                return false;
            var order = Repository.GetByKey(entity.ID);
            if (order == null)
                return false;
            order.ExpressNumber = entity.ExpressNumber;
            order.Status = entity.Status;
            return base.Update(entity);
        }

        public IEnumerable<Order> Search(OrderFilter filter, out int totalCount)
        {
            var queryable = Repository.GetQueryable(false);
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
                queryable = queryable.OrderBy(t => t.CreatedDate).Skip(filter.Start.Value).Take(filter.Limit.Value);
            return queryable.ToArray();
        }
    }
}
