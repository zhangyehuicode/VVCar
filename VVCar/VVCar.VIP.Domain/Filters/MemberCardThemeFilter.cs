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
    /// 卡片类型主题 过滤
    /// </summary>
    public class MemberCardThemeFilter : BasePageFilter
    {
        /// <summary>
        /// 分组ID
        /// </summary>
        [Display(Name = "分组ID")]
        public Guid? CardThemeGroupID { get; set; }

        /// <summary>
        /// 最大的分组ID
        /// </summary>
        [Display(Name = "最大的分组ID")]
        public Guid? CardThemeCategoryID { get; set; }


        /// <summary>
        /// 卡片类型ID
        /// </summary>
        [Display(Name = "卡片类型ID")]
        public Guid? CardTypeID { get; set; }

        /// <summary>
        /// 主题名称
        /// </summary>
        [Display(Name = "主题名称")]
        public string Name { get; set; }

        /// <summary>
        /// 是否默认
        /// </summary>
        [Display(Name = "是否默认")]
        public bool? IsDefault { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        [Display(Name = "是否启用")]
        public bool? IsAvailable { get; set; }
    }
}
