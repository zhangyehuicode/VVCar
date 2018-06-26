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
    /// 游戏设置服务
    /// </summary>
    public partial interface IGameSettingService : IDomainService<IRepository<GameSetting>, GameSetting, Guid>
    {
        /// <summary>
        /// 查询游戏设置
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        IEnumerable<GameSetting> Search(GameSettingFilter filter, out int totalCount);
    }
}
