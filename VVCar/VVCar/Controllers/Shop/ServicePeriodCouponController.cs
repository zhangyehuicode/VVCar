using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using VVCar.Shop.Domain.Dtos;
using VVCar.Shop.Domain.Entities;
using VVCar.Shop.Domain.Filters;
using VVCar.Shop.Domain.Services;
using YEF.Core.Dtos;

namespace VVCar.Controllers.Shop
{
    /// <summary>
    /// 服务周期卡券领域接口
    /// </summary>
    [RoutePrefix("api/ServicePeriodCoupon")]
    public class ServicePeriodCouponController : BaseApiController
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ServicePeriodCouponController(IServicePeriodCouponService servicePeriodCouponservice)
        {
            ServicePeriodCouponService = servicePeriodCouponservice;
        }

        IServicePeriodCouponService ServicePeriodCouponService { get; set; }

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="servicePeriodCoupons"></param>
        /// <returns></returns>
        [HttpPost, Route("BatchAdd")]
        public JsonActionResult<bool> BatchAdd(IEnumerable<ServicePeriodCoupon> servicePeriodCoupons)
        {
            return SafeExecute(() =>
            {
                return ServicePeriodCouponService.BatchAdd(servicePeriodCoupons);
            });
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        [HttpDelete, Route("BatchDelete")]
        public JsonActionResult<bool> BatchDelete(BatchOperationDto parameter)
        {
            return SafeExecute(() =>
            {
                return ServicePeriodCouponService.BatchDelete(parameter.IdList.ToArray());
            });
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        public PagedActionResult<ServicePeriodCouponDto> Search([FromUri]ServicePeriodCouponFilter filter)
        {
            return SafeGetPagedData<ServicePeriodCouponDto>((result) =>
            {
                int totalCount = 0;
                var pageData = this.ServicePeriodCouponService.Search(filter, out totalCount);
                result.Data = pageData;
                result.TotalCount = totalCount;
            });
        }
    }
}