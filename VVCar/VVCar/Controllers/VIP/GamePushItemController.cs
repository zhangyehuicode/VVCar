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
    /// 游戏推送子项
    /// </summary>
    [RoutePrefix("api/GamePushItem")]
    public class GamePushItemController : BaseApiController
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="gamePushItemService"></param>
        public GamePushItemController(IGamePushItemService gamePushItemService)
        {
            GamePushItemService = gamePushItemService;
        }

        IGamePushItemService GamePushItemService { get; set; } 

        /// <summary>
        /// 批量新增游戏推送子项
        /// </summary>
        /// <param name="gamePushItems"></param>
        /// <returns></returns>
        [HttpPost, Route("BatchAdd")]
        public JsonActionResult<bool> BatchAdd(IEnumerable<GamePushItem> gamePushItems)
        {
            return SafeExecute(() =>
            {
                if(gamePushItems == null)
                {
                    throw new DomainException("参数错误");
                }
                return GamePushItemService.BatchAdd(gamePushItems);
            });
        }

        /// <summary>
        /// 批量删除游戏推送子项
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        [HttpDelete, Route("BatchDelete")]
        public JsonActionResult<bool> BatchDelete(BatchOperationDto parameter)
        {
            return SafeExecute(() =>
            {
                return GamePushItemService.BatchDelete(parameter.IdList.ToArray());
            });
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        public PagedActionResult<GamePushItemDto> Search([FromUri]GamePushItemFilter filter)
        {
            return SafeGetPagedData<GamePushItemDto>((result) =>
            {
                var totalCount = 0;
                var data = GamePushItemService.Search(filter, out totalCount);
                result.Data = data;
                result.TotalCount = totalCount;
            });
        }
    }
}
