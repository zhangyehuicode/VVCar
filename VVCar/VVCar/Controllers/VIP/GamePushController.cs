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
using YEF.Core.Dtos;

namespace VVCar.Controllers.VIP
{
    /// <summary>
    /// 游戏推送
    /// </summary>
    [RoutePrefix("api/GamePush")]
    public class GamePushController : BaseApiController
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="gamePushService"></param>
        public GamePushController(IGamePushService gamePushService)
        {
            GamePushService = gamePushService;
        }

        IGamePushService GamePushService { get; set; }

        /// <summary>
        /// 新增游戏推送
        /// </summary>
        /// <param name="gamePush"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonActionResult<GamePush> AddGamePush(GamePush gamePush)
        {
            return SafeExecute(() =>
            {
                return GamePushService.Add(gamePush);
            });
        }

        /// <summary>
        /// 批量删除游戏推送任务
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        [HttpDelete, Route("DeleteGamePushs")]
        public JsonActionResult<bool> DeleteGamePushs(BatchOperationDto parameter)
        {
            return SafeExecute(() =>
            {
                return GamePushService.DeleteGamePushs(parameter.IdList.ToArray());
            });
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPut]
        public JsonActionResult<bool> Update(GamePush entity)
        {
            return SafeExecute(() =>
            {
                return GamePushService.Update(entity);
            });
        }

        /// <summary>
        /// 手动批量推送游戏
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        [HttpPost, Route("BatchHandGamePush")]
        public JsonActionResult<bool> BatchHandGamePush(BatchOperationDto parameter)
        {
            return SafeExecute(() =>
            {
                return GamePushService.BatchHandGamePush(parameter.IdList.ToArray());
            });
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        public PagedActionResult<GamePushDto> Search([FromUri] GamePushFilter filter)
        {
            return SafeGetPagedData<GamePushDto>((result) =>
            {
                var totalCount = 0;
                var data = GamePushService.Search(filter, out totalCount);
                result.Data = data;
                result.TotalCount = totalCount;
            });
        }
    }
}
