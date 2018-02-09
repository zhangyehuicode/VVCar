using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.VIP.Domain.Dtos;
using VVCar.VIP.Domain.Entities;
using VVCar.VIP.Domain.Filters;
using VVCar.VIP.Domain.Services;
using YEF.Core.Data;
using YEF.Core.Domain;

namespace VVCar.VIP.Services.DomainServices
{
    /// <summary>
    /// 卡片主题类别领域服务
    /// </summary>
    /// <seealso cref="VVCar.VIP.Domain.Services.ICardThemeCategoryService" />
    public class CardThemeCategoryService : DomainServiceBase<IRepository<CardThemeCategory>, CardThemeCategory, Guid>, ICardThemeCategoryService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CardThemeCategoryService"/> class.
        /// </summary>
        public CardThemeCategoryService()
        {
        }

        public IEnumerable<CardThemeCategoryMenu> GetCardThemeCategoryMenu()
        {
            return Repository.GetInclude(t => t.CardThemeGroupList, false).ToArray().Select(t => new CardThemeCategoryMenu
            {
                ID = t.ID,
                text = t.Grade != 0 ? $"{t.Name}({t.CardThemeGroupList.Where(s => s.IsDeleted == false).Count()})" : t.Name,
                expanded = false,
                Children = t.CardThemeGroupList.Where(s => s.IsDeleted == false).Select(item => new CardThemeCategoryMenu
                {
                    ID = item.ID,
                    ParentID = t.ID,
                    text = item.Name,
                    leaf = true,
                }).ToArray()
            }).ToArray();

        }

        public IEnumerable<CardThemeCategory> Search(CardThemeCategoryFilter filter, ref int totalCount)
        {
            var queryable = Repository.GetQueryable(false).Where(t => t.Grade != 0);
            totalCount = queryable.Count();
            if (filter.Start.HasValue && filter.Limit.HasValue)
                queryable = queryable.OrderBy(t => t.Grade).Skip(filter.Start.Value).Take(filter.Limit.Value);
            return queryable.OrderBy(t => t.Grade).ToArray();
        }
    }
}
