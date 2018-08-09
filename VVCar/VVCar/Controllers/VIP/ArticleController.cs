using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VVCar.VIP.Domain.Dtos;
using VVCar.VIP.Domain.Entities;
using VVCar.VIP.Domain.Filters;
using VVCar.VIP.Domain.Services;
using YEF.Core.Dtos;

namespace VVCar.Controllers.VIP
{
    /// <summary>
    /// 图文消息
    /// </summary>
    [RoutePrefix("api/Article")]
    public class ArticleController : BaseApiController
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="articleService"></param>
        public ArticleController(IArticleService articleService)
        {
            ArticleService = articleService;
        }

        IArticleService ArticleService { get; set; }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonActionResult<Article> Add(Article entity)
        {
            return SafeExecute(() =>
            {
                return ArticleService.Add(entity);
            });
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPut]
        public JsonActionResult<bool> Update(Article entity)
        {
            return SafeExecute(() =>
            {
                return ArticleService.Update(entity);
            });
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        [HttpDelete, Route("BatchDelete")]
        public JsonActionResult<bool> BatchDelete(BatchOperationDto parameter)
        {
            return SafeExecute(() =>
            {
                return ArticleService.BatchDelete(parameter.IdList.ToArray());
            });
        }

        /// <summary>
        /// 手动批量推送图文消息
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        [HttpPost, Route("BatchHandArticle")]
        public JsonActionResult<bool> BatchHandArticle(BatchOperationDto parameter)
        {
            return SafeExecute(() =>
            {
                return ArticleService.BatchHandArticle(parameter.IdList.ToArray());
            });
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        public PagedActionResult<ArticleDto> Search([FromUri]ArticleFilter filter)
        {
            return SafeGetPagedData<ArticleDto>((result) =>
            {
                var totalCount = 0;
                var data = ArticleService.Search(filter, out totalCount);
                result.Data = data;
                result.TotalCount = totalCount;
            });
        }
    }
}
