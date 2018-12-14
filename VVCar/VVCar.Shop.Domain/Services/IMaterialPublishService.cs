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
    /// 信息发布接口
    /// </summary>
    public partial interface IMaterialPublishService : IDomainService<IRepository<MaterialPublish>, MaterialPublish, Guid>
    {
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        bool BatchDelete(Guid[] ids);

        /// <summary>
        /// 批量发布
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        bool BatchHandMaterialPublish(Guid[] ids);

        /// <summary>
        /// 批量取消发布
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        bool BatchHandCancelMaterialPublish(Guid[] ids);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        IEnumerable<MaterialPublish> Search(MaterialPublishFilter filter, out int totalCount);
    }
}
