using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.BaseData.Domain.Entities;
using VVCar.BaseData.Domain.Services;
using VVCar.Shop.Domain.Entities;
using VVCar.Shop.Domain.Filters;
using VVCar.Shop.Domain.Services;
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
            return base.Add(entity);
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
