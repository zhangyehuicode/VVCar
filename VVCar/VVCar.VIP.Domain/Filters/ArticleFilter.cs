using System.ComponentModel.DataAnnotations;
using VVCar.VIP.Domain.Enums;
using YEF.Core.Dtos;

namespace VVCar.VIP.Domain.Filters
{
    /// <summary>
    /// 图文消息过滤条件
    /// </summary>
    public class ArticleFilter : BasePageFilter
    {
        /// <summary>
        /// 图文标题
        /// </summary>
        [Display(Name = "图文标题")]
        public string Name { get; set; }

        /// <summary>
        /// 推送状态
        /// </summary>
        [Display(Name = "推送状态")]
        public EArticlePushStatus? Status { get; set; }
    }
}
