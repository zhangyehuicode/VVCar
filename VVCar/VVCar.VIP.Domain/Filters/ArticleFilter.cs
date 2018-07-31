using System.ComponentModel.DataAnnotations;
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
    }
}
