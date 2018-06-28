using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.VIP.Domain.Entities;
using VVCar.VIP.Domain.Filters;
using YEF.Core.Data;
using YEF.Core.Domain;

namespace VVCar.VIP.Domain.Services
{
    /// <summary>
    /// 游戏卡券记录服务
    /// </summary>
    public partial interface IGameCouponRecordService : IDomainService<IRepository<GameCouponRecord>, GameCouponRecord, Guid>
    {
        /// <summary>
        /// 判断游戏设置
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        bool ValidateGameSetting(GameCouponRecordFilter filter);

        /// <summary>
        /// 查询游戏卡券领取记录
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        IEnumerable<GameCouponRecord> Search(GameCouponRecordFilter filter, out int totalCount);
    }
}
