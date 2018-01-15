using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEF.Core.Data;

namespace VVCar.BaseData.Domain.Entities
{
    /// <summary>
    /// 礼品卡主题分组时间子表
    /// </summary>
    public class CardThemeGroupUseTime : NormalEntityBase
    {
        /// <summary>
        /// 礼品卡主题分组的ID
        /// </summary>
        [Display(Name = "礼品卡主题分组的ID")]
        public Guid CardThemeGroupID { get; set; }

        /// <summary>
        /// 分组
        /// </summary>
        [Display(Name = "分组")]
        public virtual CardThemeGroup CardThemeGroup { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        [Display(Name = "开始时间")]
        public string BeginTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        [Display(Name = "结束时间")]
        public string EndTime { get; set; }

    }
}
