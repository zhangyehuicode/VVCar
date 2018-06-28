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
    /// 游戏卡券记录
    /// </summary>
    [RoutePrefix("api/GameCouponRecord")]
    public class GameCouponRecordController : BaseApiController
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="gameCouponRecordService"></param>
        public GameCouponRecordController(IGameCouponRecordService gameCouponRecordService)
        {
            GameCouponRecordService = gameCouponRecordService;
        }

        IGameCouponRecordService GameCouponRecordService { get; set; }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost, AllowAnonymous]
        public JsonActionResult<GameCouponRecord> Add(GameCouponRecord entity)
        {
            return SafeExecute(() =>
            {
                return GameCouponRecordService.Add(entity);
            });
        }

        /// <summary>
        /// 判断游戏设置
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet, AllowAnonymous, Route("ValidateGameSetting")]
        public JsonActionResult<bool> ValidateGameSetting([FromUri]GameCouponRecordFilter filter)
        {
            return SafeExecute(() =>
            {
                return GameCouponRecordService.ValidateGameSetting(filter);
            });
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet, AllowAnonymous]
        public PagedActionResult<GameCouponRecord> Search([FromUri]GameCouponRecordFilter filter)
        {
            return SafeGetPagedData<GameCouponRecord>((result) =>
            {
                var totalCount = 0;
                var data = this.GameCouponRecordService.Search(filter, out totalCount);
                result.Data = data;
                result.TotalCount = totalCount;
            });
        }
    }
}
