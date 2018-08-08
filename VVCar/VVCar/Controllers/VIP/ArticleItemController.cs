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
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        public PagedActionResult<ArticleItem> Search(ArticleItemFilter filter)
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
