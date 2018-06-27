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
            totalCount = queryable.Count();
            if (filter.Start.HasValue && filter.Limit.HasValue)
                queryable = queryable.OrderByDescending(t => t.CreatedDate).Skip(filter.Start.Value).Take(filter.Limit.Value);
            return queryable.ToArray();
        }
    }
}
