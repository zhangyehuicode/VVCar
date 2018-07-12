using System.ComponentModel.DataAnnotations;
using VVCar.VIP.Domain.Enums;
using YEF.Core.Dtos;

namespace VVCar.VIP.Domain.Filters
{
    /// <summary>
    /// 超能课堂过滤条件
    /// </summary>
    public class SuperClassFilter : BasePageFilter
    {
        /// <summary>
        /// 视频名称
        /// </summary>
        [Display(Name = "视频名称")]
        public string Name { get; set; }

        /// <summary>
        /// 视频类型
        /// </summary>
        [Display(Name = "视频类型")]
        public EVideoType? VideoType { get; set; }
    }
}
