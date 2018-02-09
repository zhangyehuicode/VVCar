using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VVCar.VIP.Domain.Dtos;
using VVCar.VIP.Domain.Entities;
using VVCar.VIP.Domain.Enums;
using VVCar.VIP.Domain.Filters;
using VVCar.VIP.Domain.Services;
using YEF.Core;
using YEF.Core.Dtos;

namespace VVCar.Controllers.VIP
{
    /// <summary>
    /// 储值方案
    /// </summary>
    [RoutePrefix("api/RechargePlan")]
    public class RechargePlanController : BaseApiController
    {
        #region ctor.

        /// <summary>
        /// 储值方案
        /// </summary>
        /// <param name="rechargePlanService"></param>
        public RechargePlanController(IRechargePlanService rechargePlanService)
        {
            this.RechargePlanService = rechargePlanService;
        }

        #endregion

        #region properties

        IRechargePlanService RechargePlanService { get; set; }

        #endregion

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="rechargePlan">储值方案</param>
        /// <returns></returns>
        [HttpPost, Route("NewRechargePlan")]
        public JsonActionResult<RechargePlan> NewRechargePlan(NewUpdateRechargePlanDto rechargePlan)
        {
            return SafeExecute(() =>
            {
                return this.RechargePlanService.NewRechargePlan(rechargePlan);
            });
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="rechargePlan">储值方案</param>
        /// <returns></returns>
        [HttpPost, Route("UpdateRechargePlan")]
        public JsonActionResult<bool> UpdateRechargePlan(NewUpdateRechargePlanDto rechargePlan)
        {
            return SafeExecute(() =>
            {
                return this.RechargePlanService.UpdateRechargePlan(rechargePlan);
            });
        }

        /// <summary>
        /// 修改状态
        /// </summary>
        /// <param name="rechargePlan">储值方案</param>
        /// <returns></returns>
        [HttpPut, Route("{planID}/ChangeStatus/{isAvailable}")]
        public JsonActionResult<bool> ChangeStatus(Guid planID, bool isAvailable)
        {
            return SafeExecute(() =>
            {
                return this.RechargePlanService.ChangeStatus(planID, isAvailable);
            });
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter">过滤条件</param>
        /// <returns></returns>
        [HttpGet]
        public PagedActionResult<RechargePlan> Search([FromUri]RechargePlanFilter filter)
        {
            return SafeGetPagedData<RechargePlan>((result) =>
            {
                if (!ModelState.IsValid)//表示没有过滤参数成功匹配，判定为错误请求。
                {
                    throw new DomainException("查询参数错误。");
                }
                int totalCount = 0;
                var pagedData = this.RechargePlanService.Search(filter, out totalCount);
                result.Data = pagedData;
                result.TotalCount = totalCount;
            });
        }

        ///// <summary>
        ///// 获取储值方案优惠券
        ///// </summary>
        ///// <param name="filter">The filter.</param>
        ///// <returns></returns>
        //[HttpGet, Route("GetRechargePlanCouponTemplates"), AllowAnonymous]
        //public PagedActionResult<RechargePlanCouponTemplate> GetRechargePlanCouponTemplates([FromUri]RechargePlanFilter filter)
        //{
        //    return SafeGetPagedData<RechargePlanCouponTemplate>((result) =>
        //    {
        //        var totalCount = 0;
        //        var data = RechargePlanService.GetRechargePlanCouponTemplates(filter, out totalCount);
        //        result.Data = data;
        //        result.TotalCount = totalCount;
        //    });
        //}

        /// <summary>
        /// 管理后台
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("UsablePlans")]
        public JsonActionResult<IEnumerable<RechargePlanDto>> GetUsablePlans(string cardTypeId)
        {
            return SafeExecute(() =>
            {
                return this.RechargePlanService.GetUsablePlans(EClientType.Portal, cardTypeId);
            });
        }
    }
}
