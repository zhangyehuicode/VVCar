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
    /// 套餐子项服务
    /// </summary>
    public interface IComboItemService : IDomainService<IRepository<ComboItem>, ComboItem, Guid>
    {
        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="comboItems"></param>
        /// <returns></returns>
        bool BatchAdd(IEnumerable<ComboItem> comboItems);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        bool BatchDelete(Guid[] ids);

        /// <summary>
        /// 查询套餐子项
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        IEnumerable<ComboItem> Search(ComboItemFilter filter, out int totalCount);
    }
}
