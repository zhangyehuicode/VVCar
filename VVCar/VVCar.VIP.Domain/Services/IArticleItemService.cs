using System;
using System.Collections.Generic;
using VVCar.VIP.Domain.Entities;
using VVCar.VIP.Domain.Filters;
using YEF.Core.Data;
using YEF.Core.Domain;

namespace VVCar.VIP.Domain.Services
{
    /// <summary>
    /// 图文消息子项领域服务接口
    /// </summary>
    public partial interface IArticleItemService : IDomainService<IRepository<ArticleItem>, ArticleItem, Guid>
    {
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        bool BatchDelete(Guid[] ids);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        IEnumerable<ArticleItem> Search(ArticleItemFilter filter, out int totalCount);
    }
}
