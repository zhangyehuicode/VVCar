using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using VVCar.BaseData.Domain.Dtos;
using VVCar.BaseData.Domain.Entities;
using VVCar.BaseData.Domain.Filters;
using VVCar.BaseData.Domain.Services;
using YEF.Core;
using YEF.Core.Dtos;

namespace VVCar.Controllers.Api
{
    /// <summary>
    /// 用户会员关联
    /// </summary>
    [RoutePrefix("api/UserMember")]
    public class UserMemberController : BaseApiController
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="userMemberService"></param>
        public UserMemberController(IUserMemberService userMemberService)
        {
            UserMemberService = userMemberService;
        }

        #region properties

        IUserMemberService UserMemberService { get; set; }

        #endregion 

        /// <summary>
        /// 新增用户会员关联
        /// </summary>
        /// <param name="userMembers"></param>
        /// <returns></returns>
        [HttpPost, Route("BatchAdd")]
        public JsonActionResult<bool> BatchAdd(IEnumerable<UserMember> userMembers)
        {
            return SafeExecute(() =>
            {
                if(userMembers == null)
                    throw new DomainException("参数错误");
                return UserMemberService.BatchAdd(userMembers);
            });
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpDelete, Route("BatchDelete")]
        public JsonActionResult<bool> BatchDelete(IEnumerable<Guid> ids)
        {
            return SafeExecute(() =>
            {
                if (ids == null || ids.Count() < 1)
                    throw new DomainException("参数错误");
                return UserMemberService.BatchDelete(ids);
            });
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        public PagedActionResult<UserMemberDto> Search([FromUri]UserMemberFilter filter)
        {
            return SafeGetPagedData<UserMemberDto>((result) =>
            {
                if (!ModelState.IsValid)
                    throw new DomainException("参数错误");
                var totalCount = 0;
                var data = UserMemberService.Search(filter, out totalCount);
                result.Data = data;
                result.TotalCount = totalCount;
            });
        }
    }
}
