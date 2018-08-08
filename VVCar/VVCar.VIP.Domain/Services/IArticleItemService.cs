using System;
using System.Collections.Generic;
using VVCar.VIP.Domain.Entities;
using VVCar.VIP.Domain.Filters;
using YEF.Core.Data;
using YEF.Core.Domain;

namespace VVCar.VIP.Domain.Services
{
    /// <summary>
    /// 图文消息子项
    /// </summary>
    public partial interface IArticleItemService : IDomainService<IRepository<ArticleItem>, ArticleItem, Guid>
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        IEnumerable<ArticleItem> Search(ArticleItemFilter filter, out int totalCount);
    }
}
