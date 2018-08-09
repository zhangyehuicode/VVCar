using System;
using System.Collections.Generic;
using System.Linq;
using VVCar.VIP.Domain.Dtos;
using VVCar.VIP.Domain.Entities;
using VVCar.VIP.Domain.Enums;
using VVCar.VIP.Domain.Filters;
using VVCar.VIP.Domain.Services;
using YEF.Core;
using YEF.Core.Data;
using YEF.Core.Domain;

namespace VVCar.VIP.Services.DomainServices
{
    /// <summary>
    /// 图文消息服务
    /// </summary>
    public class ArticleService : DomainServiceBase<IRepository<Article>, Article, Guid>, IArticleService
    {
        /// <summary>
        /// ctor
        /// </summary>
        public ArticleService()
        {
        }

        #region properties

        IRepository<ArticleItem> ArticleItemRepo { get => UnitOfWork.GetRepository<IRepository<ArticleItem>>(); }

        #endregion

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override Article Add(Article entity)
        {
            if (entity == null)
                throw new DomainException("参数错误");
            entity.ID = Util.NewID();
            entity.CreatedDate = DateTime.Now;
            entity.CreatedUserID = AppContext.CurrentSession.UserID;
            entity.CreatedUser = AppContext.CurrentSession.UserName;
            entity.MerchantID = AppContext.CurrentSession.MerchantID;
            return base.Add(entity);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override bool Update(Article entity)
        {
            if (entity == null)
                return false;
            var article = Repository.GetByKey(entity.ID);
            if (article == null)
                return false;
            article.Code = entity.Code;
            article.Name = entity.Name;
            article.PushDate = entity.PushDate;
            article.IsPushAllMembers = entity.IsPushAllMembers;
            article.LastUpdateDate = DateTime.Now;
            article.LastUpdateUser = AppContext.CurrentSession.UserName;
            article.LastUpdateUserID = AppContext.CurrentSession.UserID;
            return Repository.Update(article) > 0;
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
            var articleList = Repository.GetInclude(t => t.ArtitleItems, false).Where(t => ids.Contains(t.ID)).ToList();
            if (articleList == null || articleList.Count() < 1)
                throw new DomainException("数据不存在");
            UnitOfWork.BeginTransaction();
            try
            {
                foreach (var article in articleList)
                {
                    if (article.ArtitleItems.Count > 0)
                    {
                        var articleItems = article.ArtitleItems;
                        articleItems.ForEach(t => t.IsDeleted = true);
                        ArticleItemRepo.UpdateRange(article.ArtitleItems);
                        article.ArtitleItems = null;
                        article.IsDeleted = true;
                    }
                }
                Repository.UpdateRange(articleList);
                UnitOfWork.CommitTransaction();
                return true;
            }
            catch (Exception e)
            {
                UnitOfWork.RollbackTransaction();
                throw e;
            }
        }

        /// <summary>
        /// 手动批量推送
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool BatchHandArticle(Guid[] ids)
        {
            if (ids == null || ids.Length < 1)
                throw new DomainException("参数不正确");
            var notPushData = Repository.GetInclude(t => t.ArtitleItems, false).Where(t => ids.Contains(t.ID) && t.Status == EArticlePushStatus.NotPush).ToList();
            if (notPushData.Count() < 1)
                return true;
            notPushData.ForEach(t =>
            {

            });
            return true;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public IEnumerable<ArticleDto> Search(ArticleFilter filter, out int totalCount)
        {
            var queryable = Repository.GetQueryable(false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID);
            if (!string.IsNullOrEmpty(filter.Name))
                queryable = queryable.Where(t => t.Name.Contains(filter.Name));
            if (filter.Status.HasValue)
                queryable = queryable.Where(t => t.Status == filter.Status);
            totalCount = queryable.Count();
            if (filter.Start.HasValue && filter.Limit.HasValue)
                queryable = queryable.OrderByDescending(t => t.CreatedDate).Skip(filter.Start.Value).Take(filter.Limit.Value);
            return queryable.MapTo<ArticleDto>().ToArray();
        }
    }
}
