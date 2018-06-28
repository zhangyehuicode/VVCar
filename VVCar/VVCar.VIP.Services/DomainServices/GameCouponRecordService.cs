using System;
using System.Collections.Generic;
using System.Linq;
using VVCar.VIP.Domain.Entities;
using VVCar.VIP.Domain.Filters;
using VVCar.VIP.Domain.Services;
using YEF.Core;
using YEF.Core.Data;
using YEF.Core.Domain;

namespace VVCar.VIP.Services.DomainServices
{
    /// <summary>
    /// 游戏卡券服务
    /// </summary>
    public partial class GameCouponRecordService : DomainServiceBase<IRepository<GameCouponRecord>, GameCouponRecord, Guid>, IGameCouponRecordService
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public GameCouponRecordService()
        {
        }

        #region properties

        IRepository<GameSetting> GameSettingRepo { get => UnitOfWork.GetRepository<IRepository<GameSetting>>(); }

        #endregion

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override GameCouponRecord Add(GameCouponRecord entity)
        {
            entity.ID = Util.NewID();
            entity.MerchantID = AppContext.CurrentSession.MerchantID;
            entity.CreatedDate = DateTime.Now;
            return base.Add(entity);
        }

        /// <summary>
        /// 判断游戏设置
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public bool ValidateGameSetting(GameCouponRecordFilter filter)
        {
            if (string.IsNullOrEmpty(filter.ReceiveOpenID) || !filter.GameType.HasValue)
                throw new DomainException("参数错误");

            var gameSetting = GameSettingRepo.GetQueryable(false).Where(t => t.GameType == filter.GameType.Value && t.MerchantID == AppContext.CurrentSession.MerchantID).FirstOrDefault();
            var now = DateTime.Now.Date;
            if (!gameSetting.IsAvailable)
            {
                throw new DomainException("游戏未开启");
            }
            if (now < gameSetting.StartTime)
            {
                throw new DomainException("游戏未开始");
            }
            if (now > gameSetting.EndTime.AddDays(1))
            {
                throw new DomainException("游戏已结束");
            }

            var endTime = gameSetting.EndTime.AddDays(1);
            var queryable = this.Repository.GetQueryable(false).Where(t => t.GameType == filter.GameType && t.CreatedDate > gameSetting.StartTime && t.CreatedDate < endTime && t.MerchantID == AppContext.CurrentSession.MerchantID);
            var count = queryable.Count();
            if (count >= gameSetting.Limit)
                throw new DomainException("游戏次数已达上限");

            var startTime = now.AddDays(-(gameSetting.PeriodDays));
            endTime = now.AddDays(1);
            count = queryable.Where(t => t.CreatedDate > startTime && t.CreatedDate < endTime).Count();
            if (count >= gameSetting.PeriodCounts)
                throw new DomainException("游戏次数已达" + gameSetting.PeriodDays + "天" + gameSetting.PeriodCounts + "次的限制");
            return true;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public IEnumerable<GameCouponRecord> Search(GameCouponRecordFilter filter, out int totalCount)
        {
            var queryable = this.Repository.GetQueryable(false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID);
            if (filter.GameType.HasValue)
                queryable = queryable.Where(t => t.GameType == filter.GameType);
            if (!string.IsNullOrEmpty(filter.ReceiveOpenID))
                queryable = queryable.Where(t => t.ReceiveOpenID == filter.ReceiveOpenID);
            if (filter.StartTime.HasValue)
                queryable = queryable.Where(t => t.CreatedDate >= filter.StartTime.Value);
            if (filter.EndTime.HasValue)
            {
                var endTime = filter.EndTime.Value.AddDays(1);
                queryable = queryable.Where(t => t.CreatedDate < endTime);
            }
            totalCount = queryable.Count();
            if (filter.Start.HasValue && filter.Limit.HasValue)
                queryable = queryable.OrderByDescending(t => t.CreatedDate).Skip(filter.Start.Value).Take(filter.Limit.Value);
            return queryable.ToArray();
        }
    }
}
