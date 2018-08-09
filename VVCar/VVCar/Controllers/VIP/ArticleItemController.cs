using System.Linq;
using System.Web.Http;
using VVCar.VIP.Domain.Entities;
using VVCar.VIP.Domain.Filters;
using VVCar.VIP.Domain.Services;
using YEF.Core.Dtos;

namespace VVCar.Controllers.VIP
{
    /// <summary>
    /// 图文消息子项
    /// </summary>
    [RoutePrefix("api/ArticleItem")]
    public class ArticleItemController : BaseApiController
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="articleItemService"></param>
        public ArticleItemController(IArticleItemService articleItemService)
        {
            ArticleItemService = articleItemService;
        }

        IArticleItemService ArticleItemService { get; set; }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonActionResult<ArticleItem> Add(ArticleItem entity)
        {
            return SafeExecute(() =>
            {
                return ArticleItemService.Add(entity);
            });
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPut]
        public JsonActionResult<bool> Update(ArticleItem entity)
        {
            return SafeExecute(() =>
            {
                return ArticleItemService.Update(entity);
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
                return ArticleItemService.BatchDelete(parameter.IdList.ToArray());
            });
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        public PagedActionResult<ArticleItem> Search([FromUri]ArticleItemFilter filter)
        {
            return SafeGetPagedData<ArticleItem>((result) =>
            {
                var totalCount = 0;
                var data = ArticleItemService.Search(filter, out totalCount);
                result.Data = data;
                result.TotalCount = totalCount;
            });
        }
    }
}
