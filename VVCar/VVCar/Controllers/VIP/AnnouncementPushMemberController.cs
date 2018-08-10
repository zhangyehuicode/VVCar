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
    /// 公告推送会员
    /// </summary>
    [RoutePrefix("api/AnnouncementPushMember")]
    public class AnnouncementPushMemberController : BaseApiController
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="announcementPushMemberService"></param>
        public AnnouncementPushMemberController(IAnnouncementPushMemberService announcementPushMemberService)
        {
            AnnouncementPushMemberService = announcementPushMemberService;
        }

        IAnnouncementPushMemberService AnnouncementPushMemberService { get; set; }

        /// <summary>
        /// 批量新增公告推送会员
        /// </summary>
        /// <param name="announcementPushMembers"></param>
        /// <returns></returns>
        [HttpPost, Route("BatchAdd")]
        public JsonActionResult<bool> BatchAdd(IEnumerable<AnnouncementPushMember> announcementPushMembers)
        {
            return SafeExecute(() =>
            {
                if (announcementPushMembers == null || announcementPushMembers.Count() < 1)
                    throw new DomainException("参数错误");
                return AnnouncementPushMemberService.BatchAdd(announcementPushMembers);
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
                return AnnouncementPushMemberService.BatchDelete(parameter.IdList.ToArray());
            });
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        public PagedActionResult<AnnouncementPushMemberDto> Search([FromUri]AnnouncementPushMemberFilter filter)
        {
            return SafeGetPagedData<AnnouncementPushMemberDto>((result) =>
            {
                var totalCount = 0;
                var data = this.AnnouncementPushMemberService.Search(filter, out totalCount);
                result.Data = data;
                result.TotalCount = totalCount;
            });
        }
    }
}
