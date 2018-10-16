using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VVCar.VIP.Domain.Entities;
using VVCar.VIP.Domain.Filters;
using VVCar.VIP.Domain.Services;
using YEF.Core.Dtos;

namespace VVCar.Controllers.VIP
{
    /// <summary>
    /// 会员车牌
    /// </summary>
    [RoutePrefix("api/MemberPlate")]

    public class MemberPlateController : BaseApiController
    {
        public MemberPlateController(IMemberPlateService memberPlateService)
        {
            MemberPlateService = memberPlateService;
        }

        IMemberPlateService MemberPlateService { get; set; }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost, AllowAnonymous]
        public JsonActionResult<MemberPlate> Add(MemberPlate entity)
        {
            return SafeExecute(() =>
            {
                return MemberPlateService.Add(entity);
            });
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet, Route("Delete"), AllowAnonymous]
        public JsonActionResult<bool> Delete(Guid id)
        {
            return SafeExecute(() =>
            {
                return MemberPlateService.Delete(id);
            });
        }

        /// <summary>
        /// 通过车牌号获取会员
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet, Route("GetMemberByPlate"), AllowAnonymous]
        public PagedActionResult<Member> GetMemberByPlate([FromUri]MemberPlateFilter filter)
        {
            return SafeGetPagedData<Member>(result =>
            {
                var totalCount = 0;
                var data = MemberPlateService.GetMemberByPlate(filter, ref totalCount);
                result.TotalCount = totalCount;
                result.Data = data ;
            });
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet, AllowAnonymous]
        public PagedActionResult<MemberPlate> Search([FromUri]MemberPlateFilter filter)
        {
            return SafeGetPagedData<MemberPlate>((result) =>
            {
                var totalCount = 0;
                var data = MemberPlateService.Search(filter, ref totalCount);
                result.Data = data;
                result.TotalCount = totalCount;
            });
        }
    }
}
