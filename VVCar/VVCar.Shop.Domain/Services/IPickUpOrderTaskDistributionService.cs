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
    /// 接车单任务分配领域服务接口
    /// </summary>
    public interface IPickUpOrderTaskDistributionService : IDomainService<IRepository<PickUpOrderTaskDistribution>, PickUpOrderTaskDistribution, Guid>
    {
        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="pickUpOrderTaskDistributions"></param>
        /// <returns></returns>
        bool BatchAdd(IEnumerable<PickUpOrderTaskDistribution> pickUpOrderTaskDistributions);

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
        IEnumerable<PickUpOrderTaskDistributionDto> Search(PickUpOrderTaskDistributionFilter filter, out int totalCount);
    }
}
