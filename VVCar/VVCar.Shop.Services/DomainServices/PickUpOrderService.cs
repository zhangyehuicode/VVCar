using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.BaseData.Domain.Entities;
using VVCar.BaseData.Domain.Services;
using VVCar.Shop.Domain.Entities;
using VVCar.Shop.Domain.Enums;
using VVCar.Shop.Domain.Filters;
using VVCar.Shop.Domain.Services;
using YEF.Core;
using YEF.Core.Data;
using YEF.Core.Domain;

namespace VVCar.Shop.Services.DomainServices
{
    public class PickUpOrderService : DomainServiceBase<IRepository<PickUpOrder>, PickUpOrder, Guid>, IPickUpOrderService
    {
        public PickUpOrderService()
        {
        }

        #region properties

        IRepository<MakeCodeRule> MakeCodeRuleRepo { get => UnitOfWork.GetRepository<IRepository<MakeCodeRule>>(); }

        #endregion

        public string GetTradeNo()
        {
            var newTradeNo = string.Empty;
            var existNo = false;
            var makeCodeRuleService = ServiceLocator.Instance.GetService<IMakeCodeRuleService>();
            var entity = Repository.GetQueryable(false).OrderByDescending(t => t.CreatedDate).FirstOrDefault();
            if (entity != null && entity.CreatedDate.Date != DateTime.Now.Date)
            {
                var rule = MakeCodeRuleRepo.GetQueryable().Where(t => t.Code == "PickUpOrder" && t.IsAvailable).FirstOrDefault();
                if (rule != null)
                {
                    rule.CurrentValue = 0;
                    MakeCodeRuleRepo.Update(rule);
                }
            }
            do
            {
                newTradeNo = makeCodeRuleService.GenerateCode("PickUpOrder", DateTime.Now);
                existNo = Repository.Exists(t => t.Code == newTradeNo);
            } while (existNo);
            return newTradeNo;
        }

        public override PickUpOrder Add(PickUpOrder entity)
        {
            if (entity == null || entity.PickUpOrderItemList == null || entity.PickUpOrderItemList.Count < 1)
                return null;
            entity.ID = Util.NewID();
            entity.CreatedDate = DateTime.Now;
            entity.MerchantID = AppContext.CurrentSession.MerchantID;

            if (string.IsNullOrEmpty(entity.Code))
                entity.Code = GetTradeNo();

            entity.PickUpOrderItemList.ForEach(t =>
            {
                t.ID = Util.NewID();
                t.PickUpOrderID = entity.ID;
                t.MerchantID = entity.MerchantID;
            });
            RecountMoney(entity);
            return base.Add(entity);
        }

        public void RecountMoney(PickUpOrder entity)
        {
            if (entity == null)
                return;
            if (entity.PickUpOrderItemList == null || entity.PickUpOrderItemList.Count < 1)
                entity.Money = 0;
            decimal totalMoney = 0;
            entity.PickUpOrderItemList.ForEach(t =>
            {
                t.Money = t.Quantity * t.PriceSale;
                totalMoney += t.Money;
            });
            entity.Money = totalMoney;
        }

        public IEnumerable<PickUpOrder> Search(PickUpOrderFilter filter, ref int totalCount)
        {
            var queryable = Repository.GetInclude(t => t.PickUpOrderItemList, false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID);
            if (!string.IsNullOrEmpty(filter.PlateNumber))
                queryable = queryable.Where(t => t.PlateNumber == filter.PlateNumber);
            if (filter.CreatedDate.HasValue)
            {
                var date = filter.CreatedDate.Value.Date;
                var nextdate = filter.CreatedDate.Value.Date.AddDays(1);
                queryable = queryable.Where(t => t.CreatedDate >= date && t.CreatedDate < nextdate);
            }
            totalCount = queryable.Count();
            if (filter.Start.HasValue && filter.Limit.HasValue)
                queryable = queryable.OrderByDescending(t => t.CreatedDate).Skip(filter.Start.Value).Take(filter.Limit.Value);
            return queryable.OrderByDescending(t => t.CreatedDate).ToArray();
        }

        /// <summary>
        /// 结账
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public bool CheckOut(string code)
        {
            if (string.IsNullOrEmpty(code))
                return false;
            var order = Repository.GetQueryable().Where(t => t.Code == code && t.MerchantID == AppContext.CurrentSession.MerchantID).FirstOrDefault();
            if (order == null)
                return false;
            order.Status = EPickUpOrderStatus.Payed;
            return Repository.Update(order) > 0;
        }

        /// <summary>
        /// 获取接车单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PickUpOrder GetOrder(Guid id)
        {
            return Repository.GetByKey(id);
        }
    }
}
