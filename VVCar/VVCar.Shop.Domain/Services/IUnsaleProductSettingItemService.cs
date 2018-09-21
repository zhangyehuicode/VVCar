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
    /// 滞销产品设置领域服务接口
    /// </summary>
    public partial interface IUnsaleProductSettingItemService : IDomainService<IRepository<UnsaleProductSettingItem>, UnsaleProductSettingItem, Guid>
    {
        /// <summary>
        /// 批量新增滞销产品子项
        /// </summary>
        /// <param name="unsaleProductSettingItems"></param>
        /// <returns></returns>
        bool BatchAdd(IEnumerable<UnsaleProductSettingItem> unsaleProductSettingItems);

        /// <summary>
        /// 批量删除滞销产品子项
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        bool BatchDelete(Guid[] ids);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        IEnumerable<UnsaleProductSettingItemDto> Search(UnsaleProductSettingItemFilter filter, out int totalCount);
    }
}
