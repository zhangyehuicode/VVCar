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
    /// 卡片主题类别 领域服务接口
    /// </summary>
    public interface ICardThemeCategoryService : IDomainService<IRepository<CardThemeCategory>, CardThemeCategory, Guid>
    {
        /// <summary>
        /// 获取卡片主题类别菜单
        /// </summary>
        /// <returns></returns>
        IEnumerable<CardThemeCategoryMenu> GetCardThemeCategoryMenu();

        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="totalCount">The total count.</param>
        /// <returns></returns>
        IEnumerable<CardThemeCategory> Search(CardThemeCategoryFilter filter, ref int totalCount);
    }
}
