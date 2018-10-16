using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.Shop.Domain.Entities;
using VVCar.Shop.Domain.Filters;
using YEF.Core.Data;
using YEF.Core.Domain;

namespace VVCar.Shop.Domain.Services
{
    /// <summary>
    /// 接车单子项领域服务接口
    /// </summary>
    public interface IPickUpOrderItemService : IDomainService<IRepository<PickUpOrderItem>, PickUpOrderItem, Guid>
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        IEnumerable<PickUpOrderItem> Search(PickUpOrderItemFilter filter, out int totalCount);
    }
}
