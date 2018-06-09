using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using VVCar.VIP.Domain.Dtos;
using VVCar.VIP.Domain.Entities;
using VVCar.VIP.Domain.Filters;
using VVCar.VIP.Domain.Services;
using YEF.Core;
using YEF.Core.Dtos;

namespace VVCar.Controllers.VIP
{
    /// <summary>
    /// 卡券推送子项
    /// </summary>
    [RoutePrefix("api/CouponPushItem")]
    public class CouponPushItemController : BaseApiController
    {
        #region ctor.

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="couponPushItemService"></param>
        public CouponPushItemController(ICouponPushItemService couponPushItemService)
        {
            CouponPushItemService = couponPushItemService;
        }

        #endregion

        ICouponPushItemService CouponPushItemService { get; set; }

        /// <summary>
        /// 批量新增卡券推送子项
        /// </summary>
        /// <param name="couponPushItems"></param>
        /// <returns></returns>
        [HttpPost, Route("BatchAdd")]
        public JsonActionResult<bool> BatchAdd(IEnumerable<CouponPushItem> couponPushItems)
        {
            return SafeExecute(() =>
            {
                if (couponPushItems == null)
                {
                    throw new DomainException("参数错误");
                }
                return CouponPushItemService.BatchAdd(couponPushItems);
            });
        }

        /// <summary>
        /// 批量删除卡券推送子项
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        [HttpDelete, Route("deleteCouponPushItems")]
        public JsonActionResult<bool> DeleteCouponPushItems(BatchOperationDto parameter)
        {
            return SafeExecute(() =>
            {
                return CouponPushItemService.DeleteCouponPushItems(parameter.IdList.ToArray());
            });
        }

        /// <summary>
        /// 查询卡券推送子项
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        public PagedActionResult<CouponPushItemDto> Search([FromUri]CouponPushItemFilter filter)
        {
            return SafeGetPagedData<CouponPushItemDto>((result) =>
            {
                var totalCount = 0;
                var data = this.CouponPushItemService.Search(filter, out totalCount);
                result.Data = data;
                result.TotalCount = totalCount;
            });
        }
    }
}