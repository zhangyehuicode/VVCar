using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.Shop.Domain.Entities;
using VVCar.Shop.Domain.Filters;
using VVCar.Shop.Domain.Services;
using YEF.Core;
using YEF.Core.Data;
using YEF.Core.Domain;

namespace VVCar.Shop.Services.DomainServices
{
    /// <summary>
    /// 车比特订单子项 领域服务
    /// </summary>
    public class CarBitCoinOrderItemService : DomainServiceBase<IRepository<CarBitCoinOrderItem>, CarBitCoinOrderItem, Guid>, ICarBitCoinOrderItemService
    {
        public CarBitCoinOrderItemService()
        {
        }

        public IEnumerable<CarBitCoinOrderItem> Search(OrderItemFilter filter, ref int totalCount)
        {
            var queryable = Repository.GetQueryable(false).Where(t => t.Order.MerchantID == AppContext.CurrentSession.MerchantID);
            if (filter.OrderID.HasValue)
                queryable = queryable.Where(t => t.CarBitCoinOrderID == filter.OrderID.Value);
            totalCount = queryable.Count();
            if (filter.Start.HasValue && filter.Limit.HasValue)
                queryable = queryable.OrderBy(t => t.ID).Skip(filter.Start.Value).Take(filter.Limit.Value);
            return queryable.ToArray();
        }
    }
}
