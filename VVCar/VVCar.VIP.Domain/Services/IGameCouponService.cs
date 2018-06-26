using System;
using System.Collections.Generic;
using VVCar.VIP.Domain.Dtos;
using VVCar.VIP.Domain.Entities;
using VVCar.VIP.Domain.Filters;
using YEF.Core.Data;
using YEF.Core.Domain;
using YEF.Core.Dtos;

namespace VVCar.VIP.Domain.Services
{
    /// <summary>
    /// 游戏卡券配置 领域接口
    /// </summary>
    public partial interface IGameCouponService : IDomainService<IRepository<GameCoupon>, GameCoupon, Guid>
    {
        /// <summary>
        /// 查询卡券配置
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        IEnumerable<GameCouponDto> Search(GameCouponFilter filter, out int totalCount);

        /// <summary>
        /// 批量新增游戏卡券设置
        /// </summary>
        /// <param name="gameCoupons"></param>
        /// <returns></returns>
        bool BatchAdd(IEnumerable<GameCoupon> gameCoupons);
    }
}
