using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
    /// 卡券推送会员
    /// </summary>
    [RoutePrefix("api/CouponPushMember")]
    public class CouponPushMemberController : BaseApiController
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public CouponPushMemberController(ICouponPushMemberService couponPushMemberService)
        {
            CouponPushMemberService = couponPushMemberService;
        }

        ICouponPushMemberService CouponPushMemberService { get; set; }

        /// <summary>
        /// 批量新增卡券推送会员
        /// </summary>
        /// <param name="couponPushMembers"></param>
        /// <returns></returns>
        [HttpPost, Route("BatchAdd")]
        public JsonActionResult<bool> BatchAdd(IEnumerable<CouponPushMember> couponPushMembers)
        {
            return SafeExecute(() =>
            {
                if (couponPushMembers == null || couponPushMembers.Count() < 1)
                    throw new DomainException("参数错误");
                return CouponPushMemberService.BatchAdd(couponPushMembers);
            });
        }

        /// <summary>
        /// 批量删除卡券推送会员
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        [HttpDelete, Route("BatchDelete")]
        public JsonActionResult<bool> BatchDelete(BatchOperationDto parameter)
        {
            return SafeExecute(() =>
            {
                return CouponPushMemberService.BatchDelete(parameter.IdList.ToArray());
            });
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        public PagedActionResult<CouponPushMemberDto> Search([FromUri]CouponPushMemberFilter filter)
        {
            return SafeGetPagedData<CouponPushMemberDto>((result) =>
            {
                var totalCount = 0;
                var data = this.CouponPushMemberService.Search(filter, out totalCount);
                result.Data = data;
                result.TotalCount = totalCount;
            });
        }

    }
}
