using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.Shop.Domain.Dtos;
using VVCar.Shop.Domain.Entities;
using VVCar.Shop.Domain.Filters;
using YEF.Core.Data;
using YEF.Core.Domain;

namespace VVCar.Shop.Domain.Services
{
    /// <summary>
    /// 订单分红领域服务接口
    /// </summary>
    public interface IOrderDividendService : IDomainService<IRepository<OrderDividend>, OrderDividend, Guid>
    {
        /// <summary>
        /// 结算
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        bool Balance(Guid[] ids);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        IEnumerable<OrderDividendDto> Search(OrderDividendFilter filter, out int totalCount);
    }
}
