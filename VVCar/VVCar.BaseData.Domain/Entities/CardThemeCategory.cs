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
    /// 卡片主题分类
    /// </summary>
    public class CardThemeCategory : NormalEntityBase
    {
        public CardThemeCategory()
        {
            CardThemeGroupList = new List<CardThemeGroup>();
        }

        /// <summary>
        /// 名称
        /// </summary>
        [Display(Name = "名称")]
        public string Name { get; set; }

        /// <summary>
        /// 等级
        /// </summary>
        [Display(Name = "等级")]
        public int Grade { get; set; }

        /// <summary>
        /// 卡片主题分组
        /// </summary>
        [Display(Name = "卡片主题分组")]
        public virtual ICollection<CardThemeGroup> CardThemeGroupList { get; set; }
    }
}
