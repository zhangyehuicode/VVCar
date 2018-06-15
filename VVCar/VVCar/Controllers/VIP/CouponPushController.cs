using System.Linq;
using System.Web.Http;
using VVCar.VIP.Domain.Dtos;
using VVCar.VIP.Domain.Entities;
using VVCar.VIP.Domain.Filters;
using VVCar.VIP.Domain.Services;
using YEF.Core.Dtos;

namespace VVCar.Controllers.VIP
{
    /// <summary>
    /// 卡券推送
    /// </summary>
    [RoutePrefix("api/CouponPush")]
    public class CouponPushController : BaseApiController
    {
        #region ctor.

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="couponPushService"></param>
        public CouponPushController(ICouponPushService couponPushService)
        {
            CouponPushService = couponPushService;
        }

        #endregion

        ICouponPushService CouponPushService { get; set; }

        /// <summary>
        /// 批量删除卡券推送任务
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        [HttpDelete, Route("deleteCouponPushs")]
        public JsonActionResult<bool> DeleteCouponPushs(BatchOperationDto parameter)
        {
            return SafeExecute(() =>
            {
                return CouponPushService.DeleteCouponPushs(parameter.IdList.ToArray());
            });
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPut]
        public JsonActionResult<bool> Update(CouponPush entity)
        {
            return SafeExecute(() =>
            {
                return CouponPushService.Update(entity);
            });
        }

        /// <summary>
        /// 手动批量推送卡券
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        [HttpPost, Route("batchHandCouponPush")]
        public JsonActionResult<bool> BatchHandCouponPush(BatchOperationDto parameter)
        {
            return SafeExecute(() =>
            {
                return CouponPushService.BatchHandCouponPush(parameter.IdList.ToArray());
            });
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet, AllowAnonymous]
        public PagedActionResult<CouponPushDto> Search([FromUri]CouponPushFilter filter)
        {
            return SafeGetPagedData<CouponPushDto>((result) =>
            {
                var totlalCount = 0;
                var data = this.CouponPushService.Search(filter, out totlalCount);
                result.Data = data;
                result.TotalCount = totlalCount;
            });
        }

        /// <summary>
        /// 新增卡券推送
        /// </summary>
        /// <param name="couponPush"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonActionResult<CouponPush> AddCouponPush(CouponPush couponPush)
        {
            return SafeExecute(() =>
            {
                return this.CouponPushService.Add(couponPush);
            });
        }
    }
}