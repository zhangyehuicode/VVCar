using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.VIP.Domain.Entities;
using VVCar.VIP.Domain.Filters;
using VVCar.VIP.Domain.Services;
using YEF.Core;
using YEF.Core.Data;
using YEF.Core.Domain;

namespace VVCar.VIP.Services.DomainServices
{
    /// <summary>
    /// 游戏设置服务
    /// </summary>
    public class GameSettingService : DomainServiceBase<IRepository<GameSetting>, GameSetting, Guid>, IGameSettingService
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public GameSettingService()
        {
        }

        #region properties

        IRepository<CouponTemplate> CouponTemplateRepo { get => UnitOfWork.GetRepository<IRepository<CouponTemplate>>(); }

        #endregion 

        /// <summary>
        /// 查询游戏设置
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public IEnumerable<GameSetting> Search(GameSettingFilter filter, out int totalCount)
        {
            var gameSettingList = new List<GameSetting>();
            var queryable = this.Repository.GetInclude(t => t.GameCoupons, false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID);
            if (filter.GameType.HasValue)
                queryable = queryable.Where(t => t.GameType == filter.GameType);
            totalCount = queryable.Count();
            if (filter.Start.HasValue && filter.Limit.HasValue)
                queryable = queryable.OrderByDescending(t => t.CreatedDate).Skip(filter.Start.Value).Take(filter.Limit.Value);
            gameSettingList = queryable.ToList();
            gameSettingList.ForEach(t =>
            {
                t.GameCoupons.ForEach(m =>
                {
                    m.CouponTemplate = CouponTemplateRepo.GetByKey(m.CouponTemplateID, false);
                });
            });
            return gameSettingList;
        }
    }
}
