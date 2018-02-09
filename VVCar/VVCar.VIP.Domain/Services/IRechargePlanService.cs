using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.VIP.Domain.Dtos;
using VVCar.VIP.Domain.Entities;
using VVCar.VIP.Domain.Enums;
using VVCar.VIP.Domain.Filters;
using YEF.Core.Data;
using YEF.Core.Domain;

namespace VVCar.VIP.Domain.Services
{
    /// <summary>
    /// 储值方案 领域服务接口
    /// </summary>
    public partial interface IRechargePlanService : IDomainService<IRepository<RechargePlan>, RechargePlan, Guid>
    {
        /// <summary>
        /// 改变状态
        /// </summary>
        /// <param name="planID">储值方案ID</param>
        /// <param name="isAvailable">是否可用</param>
        /// <returns></returns>
        bool ChangeStatus(Guid planID, bool isAvailable);

        /// <summary>
        /// 查询储值方案
        /// </summary>
        /// <param name="filter">过滤条件</param>
        /// <param name="totalCount">总记录数</param>
        /// <returns></returns>
        IEnumerable<RechargePlan> Search(RechargePlanFilter filter, out int totalCount);

        /// <summary>
        /// 获取可用的储值方案
        /// </summary>
        /// <returns></returns>
        IEnumerable<RechargePlanDto> GetUsablePlans(EClientType clientType, string cardTypeId = null);

        /// <summary>
        /// 新增储值方案
        /// </summary>
        /// <param name="entitydto"></param>
        /// <returns></returns>
        RechargePlan NewRechargePlan(NewUpdateRechargePlanDto entitydto);

        /// <summary>
        /// 更新储值方案
        /// </summary>
        /// <param name="entitydto"></param>
        /// <returns></returns>
        bool UpdateRechargePlan(NewUpdateRechargePlanDto entitydto);

        ///// <summary>
        ///// 获取储值方案优惠券
        ///// </summary>
        ///// <param name="filter">The filter.</param>
        ///// <param name="totalCount">The total count.</param>
        ///// <returns></returns>
        //IEnumerable<RechargePlanCouponTemplate> GetRechargePlanCouponTemplates(RechargePlanFilter filter, out int totalCount);
    }
}
