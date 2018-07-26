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
    public interface ILogisticsService : IDomainService<IRepository<Logistics>, Logistics, Guid>
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        IEnumerable<LogisticsDto> Search(LogisticsFilter filter, out int totalCount);

        /// <summary>
        /// 发货
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Delivery(Guid id);
    }
}
