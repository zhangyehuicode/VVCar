using System;
using System.Linq;
using System.Web.Http;
using VVCar.VIP.Domain.Dtos;
using VVCar.VIP.Domain.Entities;
using VVCar.VIP.Domain.Filters;
using VVCar.VIP.Domain.Services;
using YEF.Core.Dtos;

namespace VVCar.Controllers.VIP
{
    /// <summary>
    /// 游戏卡券配置
    /// </summary>
    [RoutePrefix("api/GameCoupon")]
    public class GameCouponController : BaseApiController
    {
        /// <summary>
        /// 构造函数 
        /// </summary>
        public GameCouponController(IGameCouponService gameCouponService)
        {
            GameCouponService = gameCouponService;
        }

        IGameCouponService GameCouponService { get; set; }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonActionResult<GameCoupon> Add(GameCoupon entity)
        {
            return SafeExecute(() =>
            {
                return GameCouponService.Add(entity);
            });
        }

        /// <summary>
        /// 新增游戏卡券配置
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("AddGameCoupon")]
        public JsonActionResult<bool> AddGameCoupon(BatchOperationDto parameter)
        {
            return SafeExecute(() =>
            {
                return GameCouponService.AddGameCoupon(parameter.IdList.ToArray());
            });
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        [HttpGet, AllowAnonymous]
        public PagedActionResult<GameCouponDto> Search([FromUri]GameCouponFilter filter)
        {
            return SafeGetPagedData<GameCouponDto>((result) =>
            {
                var totalCount = 0;
                var data = this.GameCouponService.Search(filter, out totalCount);
                result.Data = data;
                result.TotalCount = totalCount;
            });
        }

        /// <summary>
        /// 删除游戏卡券配置
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public JsonActionResult<bool> DeleteGameCoupon(Guid id)
        {
            return SafeExecute(() =>
            {
                return this.GameCouponService.Delete(id);
            });
        }
    }
}