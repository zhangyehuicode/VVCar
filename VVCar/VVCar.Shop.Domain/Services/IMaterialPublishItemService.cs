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
    /// 信息推送子项服务
    /// </summary>
    public interface IMaterialPublishItemService : IDomainService<IRepository<MaterialPublishItem>, MaterialPublishItem, Guid>
    {
        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="materialPublishItems"></param>
        /// <returns></returns>
        bool BatchAdd(IEnumerable<MaterialPublishItem> materialPublishItems);

        /// <summary>
        /// 批量删除
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
        IEnumerable<MaterialPublishItemDto> Search(MaterialPublishItemFilter filter, out int totalCount);

        /// <summary>
        /// 调整索引
        /// </summary>
        /// <param name="param">The parameter.</param>
        /// <returns></returns>
        bool AdjustIndex(AdjustIndexParam param);
    }
}
