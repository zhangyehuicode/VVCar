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
        /// 批量新增
        /// </summary>
        /// <param name="pickUpOrderItems"></param>
        /// <returns></returns>
        PickUpOrder BatchAdd(IEnumerable<PickUpOrderItem> pickUpOrderItems);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        PickUpOrder BatchDelete(Guid[] ids);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="pickUpOrderItem"></param>
        /// <returns></returns>
        PickUpOrder UpdatePickUpOrder(PickUpOrderItem pickUpOrderItem);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        IEnumerable<PickUpOrderItem> Search(PickUpOrderItemFilter filter, out int totalCount);
    }
}
