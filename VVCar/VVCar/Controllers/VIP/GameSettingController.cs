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
    /// 游戏设置
    /// </summary>
    [RoutePrefix("api/GameSetting")]
    public class GameSettingController : BaseApiController
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="gameSettingService"></param>
        public GameSettingController(IGameSettingService gameSettingService)
        {
            GameSettingService = gameSettingService;
        }

        IGameSettingService GameSettingService { get; set; }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPut]
        public JsonActionResult<bool> Update(GameSetting entity)
        {
            return SafeExecute(() =>
            {
                return GameSettingService.Update(entity);
            });
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet, AllowAnonymous]
        public PagedActionResult<GameSetting> Search([FromUri]GameSettingFilter filter)
        {
            return SafeGetPagedData<GameSetting>((result) =>
            {
                var totalCount = 0;
                var data = this.GameSettingService.Search(filter, out totalCount);
                result.Data = data;
                result.TotalCount = totalCount;
            });
        }
    }
}
