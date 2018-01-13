using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEF.Core.Data;
using System.ComponentModel.DataAnnotations;

namespace VVCar.BaseData.Domain.Entities
{
    /// <summary>
    /// 会员卡主题
    /// </summary>
    public class MemberCardTheme : EntityBase
    {
        /// <summary>
        /// 分组ID
        /// </summary>
        [Display(Name = "分组ID")]
        public Guid? CardThemeGroupID { get; set; }

        /// <summary>
        /// 分组
        /// </summary>
        [Display(Name = "分组")]
        public virtual CardThemeGroup CardThemeGroup { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [Display(Name = "排序")]
        public int Index { get; set; }

        /// <summary>
        /// 卡片类型ID
        /// </summary>
        [Display(Name = "卡片类型ID")]
        public Guid CardTypeID { get; set; }

        /// <summary>
        /// 主题名称
        /// </summary>
        [Display(Name = "主题名称")]
        public string Name { get; set; }

        /// <summary>
        /// 主题图片
        /// </summary>
        [Display(Name = "主题图片")]
        public string ImgUrl { get; set; }

        /// <summary>
        /// 是否默认
        /// </summary>
        [Display(Name = "是否默认")]
        public bool IsDefault { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        [Display(Name = "是否启用")]
        public bool IsAvailable { get; set; }
    }
}
