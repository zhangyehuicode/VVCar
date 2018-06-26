using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.VIP.Domain.Dtos;
using VVCar.VIP.Domain.Entities;
using VVCar.VIP.Domain.Filters;
using VVCar.VIP.Domain.Services;
using YEF.Core;
using YEF.Core.Data;
using YEF.Core.Domain;
using YEF.Core.Dtos;

namespace VVCar.VIP.Services.DomainServices
{
    /// <summary>
    /// 游戏卡券配置 领域接口
    /// </summary>
    public partial class GameCouponService : DomainServiceBase<IRepository<GameCoupon>, GameCoupon, Guid>, IGameCouponService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GameCouponService"/> class.
        /// </summary>
        public GameCouponService()
        {
        }

        /// <summary>
        /// Get
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public override GameCoupon Get(Guid key)
        {
            return this.Repository.GetByKey(key, false);
        }

        /// <summary>
        /// 批量新增游戏卡券设置
        /// </summary>
        /// <param name="gameCoupons"></param>
        /// <returns></returns>
        public bool BatchAdd(IEnumerable<GameCoupon> gameCoupons)
        {
            if (gameCoupons == null || gameCoupons.Count() < 1)
                throw new DomainException("参数错误");
            var gameSettingId = gameCoupons.ToList().FirstOrDefault().GameSettingID;
            var totalCount = this.Repository.GetQueryable(false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID && t.GameSettingID == gameSettingId).Count();
            if (totalCount >= 8 || (totalCount + gameCoupons.Count()) > 8)
                throw new DomainException("最多配置8个");
            gameCoupons.ForEach(t =>
            {
                t.ID = Util.NewID();
                t.MerchantID = AppContext.CurrentSession.MerchantID;
                t.CreatedDate = DateTime.Now;
            });
            return this.Repository.AddRange(gameCoupons).Count() > 0;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="gameCoupon"></param>
        /// <returns></returns>
        public override GameCoupon Add(GameCoupon entity)
        {
            if (entity == null)
                return null;
            entity.ID = Util.NewID();
            entity.CreatedDate = DateTime.Now;
            entity.MerchantID = AppContext.CurrentSession.MerchantID;
            return base.Add(entity);
        }

        /// <summary>
        /// 查询卡券配置
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public IEnumerable<GameCouponDto> Search(GameCouponFilter filter, out int totalCount)
        {
            var queryable = this.Repository.GetQueryable(false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID);
            totalCount = queryable.Count();
            if (filter.GameSettingID.HasValue)
                queryable = queryable.Where(t => t.GameSettingID == filter.GameSettingID);
            if (filter.Start.HasValue && filter.Limit.HasValue)
                queryable = queryable.OrderByDescending(t => t.ID).Skip(filter.Start.Value).Take(filter.Limit.Value);
            return queryable.MapTo<GameCouponDto>().ToArray();
        }
    }
}
