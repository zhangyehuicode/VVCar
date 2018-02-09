using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEF.Core.Dtos;

namespace VVCar.VIP.Domain.Filters
{
    /// <summary>
    /// 卡片主题分组 过滤
    /// </summary>
    public class CardThemeGroupFilter : BasePageFilter
    {
        /// <summary>
        /// ID
        /// </summary>
        [Display(Name = "分组ID")]
        public Guid? ID { get; set; }

        /// <summary>
        /// 类别ID
        /// </summary>
        [Display(Name = "类别ID")]
        public Guid? CategoryID { get; set; }

        /// <summary>
        /// 最大的分组ID
        /// </summary>
        [Display(Name = "最大的分组ID")]
        public Guid? CardThemeCategoryID { get; set; }

        /// <summary>
        /// 是否来源于管理后台
        /// </summary>
        [Display(Name = "是否来源于管理后台")]
        public bool IsFromPortal { get; set; }

        /// <summary>
        /// 是否为非推荐主题
        /// </summary>
        [Display(Name = "是否为非推荐主题")]
        public bool IsNotRecommended { get; set; }
    }
}
