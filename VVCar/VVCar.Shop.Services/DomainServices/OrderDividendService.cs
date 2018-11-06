using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.BaseData.Services;
using VVCar.Shop.Domain.Dtos;
using VVCar.Shop.Domain.Entities;
using VVCar.Shop.Domain.Filters;
using VVCar.Shop.Domain.Services;
using YEF.Core;
using YEF.Core.Data;
using YEF.Core.Domain;

namespace VVCar.Shop.Services.DomainServices
{
    /// <summary>
    /// 订单分红领域服务
    /// </summary>
    public class OrderDividendService : DomainServiceBase<IRepository<OrderDividend>, OrderDividend, Guid>, IOrderDividendService
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public IEnumerable<OrderDividendDto> Search(OrderDividendFilter filter, out int totalCount)
        {
            var queryable = Repository.GetQueryable(false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID);
            if (filter.UserID.HasValue)
                queryable = queryable.Where(t => t.UserID == filter.UserID);
            if (filter.StartDate.HasValue)
                queryable = queryable.Where(t => t.CreatedDate >= filter.StartDate);
            if(filter.EndDate.HasValue)
                queryable = queryable.Where(t => t.CreatedDate < filter.EndDate);
            totalCount = queryable.Count();
            if (filter.Start.HasValue && filter.Limit.HasValue)
                queryable = queryable.OrderByDescending(t => t.CreatedDate).Skip(filter.Start.Value).Take(filter.Limit.Value);
            return queryable.MapTo<OrderDividendDto>().ToList();
        }
    }
}
