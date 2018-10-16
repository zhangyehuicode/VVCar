using System;
using System.Collections.Generic;
using System.Linq;
using VVCar.Shop.Domain.Entities;
using VVCar.Shop.Domain.Filters;
using VVCar.Shop.Domain.Services;
using YEF.Core;
using YEF.Core.Data;
using YEF.Core.Domain;

namespace VVCar.Shop.Services.DomainServices
{
    /// <summary>
    /// 接车单子项领域服务
    /// </summary>
    public class PickUpOrderItemService : DomainServiceBase<IRepository<PickUpOrderItem>, PickUpOrderItem, Guid>, IPickUpOrderItemService
    {
        public PickUpOrderItemService()
        {
        }

        #region properties

        IPickUpOrderService PickUpOrderService { get => ServiceLocator.Instance.GetService<IPickUpOrderService>(); }

        IRepository<PickUpOrder> PickUpOrderRepo { get => UnitOfWork.GetRepository<IRepository<PickUpOrder>>(); }

        #endregion

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override bool Update(PickUpOrderItem entity)
        {
            entity.Money = entity.PriceSale * entity.Quantity;
            var pickUpOrder = PickUpOrderRepo.GetByKey(entity.PickUpOrderID);
            if (pickUpOrder.Status == Domain.Enums.EPickUpOrderStatus.Payed)
                throw new DomainException("订单已付款");
            Repository.Update(entity);
            return PickUpOrderService.RecountMoneySave(pickUpOrder.Code);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public IEnumerable<PickUpOrderItem> Search(PickUpOrderItemFilter filter, out int totalCount)
        {
            var queryable = Repository.GetQueryable(false).Where(t => t.PickUpOrder.MerchantID == AppContext.CurrentSession.MerchantID);
            if (filter.PickUpOrderID.HasValue)
                queryable = queryable.Where(t => t.PickUpOrderID == filter.PickUpOrderID.Value);
            totalCount = queryable.Count();
            if (filter.Start.HasValue && filter.Limit.HasValue)
                queryable = queryable.OrderBy(t => t.ID).Skip(filter.Start.Value).Take(filter.Limit.Value);
            return queryable.ToArray();
        }
    }
}
