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
    /// 游戏推送会员
    /// </summary>
    [RoutePrefix("api/GamePushMember")]
    public class GamePushMemberController : BaseApiController
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="gamePushMemberService"></param>
        public GamePushMemberController(IGamePushMemberService gamePushMemberService)
        {
            GamePushMemberService = gamePushMemberService;
        }

        IGamePushMemberService GamePushMemberService { get; set; }

        /// <summary>
        /// 批量新增游戏推送会员
        /// </summary>
        /// <param name="gamePushMembers"></param>
        /// <returns></returns>
        [HttpPost, Route("BatchAdd")]
        public JsonActionResult<bool> BatchAdd(IEnumerable<GamePushMember> gamePushMembers)
        {
            return SafeExecute(() => {
                if(gamePushMembers == null || gamePushMembers.Count()<1)
                    throw new DomainException("参数错误");
                return GamePushMemberService.BatchAdd(gamePushMembers);
            });
        }

        /// <summary>
        /// 批量删除游戏推送会员
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        [HttpDelete, Route("BatchDelete")]
        public JsonActionResult<bool> BatchDelete(BatchOperationDto parameter)
        {
            return SafeExecute(() =>
            {
                return GamePushMemberService.BatchDelete(parameter.IdList.ToArray());
            });
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        public PagedActionResult<GamePushMemberDto> Search([FromUri]GamePushMemberFilter filter)
        {
            return SafeGetPagedData<GamePushMemberDto>((result) =>
            {
                var totalCount = 0;
                var data = GamePushMemberService.Search(filter, out totalCount);
                result.Data = data;
                result.TotalCount = totalCount;
            });
        }
    }
}
