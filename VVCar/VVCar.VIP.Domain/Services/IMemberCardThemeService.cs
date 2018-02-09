using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.VIP.Domain.Dtos;
using VVCar.VIP.Domain.Entities;
using VVCar.VIP.Domain.Filters;
using YEF.Core.Data;
using YEF.Core.Domain;

namespace VVCar.VIP.Domain.Services
{
    /// <summary>
    /// 卡片主题领域服务接口
    /// </summary>
    public interface IMemberCardThemeService : IDomainService<IRepository<MemberCardTheme>, MemberCardTheme, Guid>
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="totalCount">The total count.</param>
        /// <returns></returns>
        IEnumerable<MemberCardTheme> Query(MemberCardThemeFilter filter, ref int totalCount);

        /// <summary>
        /// 调整卡片主题顺序
        /// </summary>
        /// <param name="adjustInfo">The adjust information.</param>
        /// <returns></returns>
        bool SetIndex(MemberCardThemeSetIndexDto adjustInfo);

        /// <summary>
        /// 设置启用与否
        /// </summary>
        /// <param name="settingInfo">The setting information.</param>
        /// <returns></returns>
        bool SetAvailable(MemberCardThemeSetAvailableDto settingInfo);
    }
}
