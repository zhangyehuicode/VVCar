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
    /// 图文消息子项服务
    /// </summary>
    public class ArticleItemService : DomainServiceBase<IRepository<ArticleItem>, ArticleItem, Guid>, IArticleItemService
    {
        /// <summary>
        /// ctor
        /// </summary>
        public ArticleItemService()
        {
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override ArticleItem Add(ArticleItem entity)
        {
            if (entity == null)
                return null;
            entity.ID = Util.NewID();
            entity.CreatedDate = DateTime.Now;
            entity.CreatedUser = AppContext.CurrentSession.UserName;
            entity.CreatedUserID = AppContext.CurrentSession.UserID;
            entity.MerchantID = AppContext.CurrentSession.MerchantID;
            return base.Add(entity);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override bool Update(ArticleItem entity)
        {
            if (entity == null)
                return false;
            var articleItem = Repository.GetByKey(entity.ID);
            if (articleItem == null)
                throw new DomainException("更新的产品不存在");

            articleItem.Title = entity.Title;
            articleItem.ThumbMediaID = entity.ThumbMediaID;
            articleItem.Author = entity.Author;
            articleItem.Digest = entity.Digest;
            articleItem.IsShowCoverPic = entity.IsShowCoverPic;
            articleItem.CoverPicUrl = entity.CoverPicUrl;
            articleItem.Content = entity.Content;
            articleItem.ContentSourceUrl = entity.ContentSourceUrl;
            articleItem.LastUpdateDate = DateTime.Now;
            articleItem.LastUpdateUser = AppContext.CurrentSession.UserName;
            articleItem.LastUpdateUserID = AppContext.CurrentSession.UserID;
            return base.Update(entity);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool BatchDelete(Guid[] ids)
        {
            if (ids == null || ids.Length < 1)
                throw new DomainException("参数错误");

            var articleItemList = Repository.GetQueryable(false).Where(t => ids.Contains(t.ID)).ToList();
            if (articleItemList == null || articleItemList.Count() < 1)
                throw new DomainException("数据不存在");
            articleItemList.ForEach(t => t.IsDeleted = true);
            return Repository.Update(articleItemList) > 0;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public IEnumerable<ArticleItem> Search(ArticleItemFilter filter, out int totalCount)
        {
            if (filter.ArticleID == null)
            {
                throw new DomainException("参数错误");
            }
            var queryable = Repository.GetQueryable(false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID && t.ArticleID == filter.ArticleID);
            totalCount = queryable.Count();
            if (filter.Start.HasValue && filter.Limit.HasValue)
                queryable = queryable.OrderByDescending(t => t.CreatedDate).Skip(filter.Start.Value).Take(filter.Limit.Value);
            return queryable.ToArray();
        }
    }
}
