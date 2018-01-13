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
    /// 卡片主题分组
    /// </summary>
    public class CardThemeGroup : EntityBase
    {
        /// <summary>
        /// 卡片主题分组
        /// </summary>
        public CardThemeGroup()
        {
            UseTimeList = new List<CardThemeGroupUseTime>();
        }

        /// <summary>
        /// 推荐类别ID
        /// </summary>
        [Display(Name = "推荐类别ID")]
        public Guid? CardThemeCategoryID { get; set; }

        /// <summary>
        /// 卡片主题类别
        /// </summary>
        [Display(Name = "卡片主题类别")]
        public virtual CardThemeCategory CardThemeCategory { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Display(Name = "名称")]
        public string Name { get; set; }

        /// <summary>
        /// 编号
        /// </summary>
        [Display(Name = "编号")]
        public string Code { get; set; }

        /// <summary>
        /// 门店编号
        /// </summary>
        [Display(Name = "门店编号")]
        public string DepartmentCode { get; set; }

        /// <summary>
        /// 关联分组
        /// </summary>
        [Display(Name = "关联分组")]
        public Guid? RecommendGroupID { get; set; }

        /// <summary>
        /// 关联分组
        /// </summary>
        public virtual CardThemeGroup RecommendCardThemeGroup { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [Display(Name = "排序")]
        public int Index { get; set; }

        /// <summary>
        /// 开始日期
        /// </summary>
        [Display(Name = "开始日期")]
        public DateTime? GiftCardStartTime { get; set; }

        /// <summary>
        /// 截止日期
        /// </summary>
        [Display(Name = "截止日期")]
        public DateTime? GiftCardEndTime { get; set; }

        /// <summary>
        /// 购买后几天生效
        /// </summary>
        [Display(Name = "购买后几天生效")]
        public int EffectiveDaysOfAfterBuy { get; set; }

        /// <summary>
        /// 有效天数
        /// </summary>
        [Display(Name = "有效天数")]
        public int EffectiveDays { get; set; }

        /// <summary>
        /// 是否为自定义日期
        /// </summary>
        [Display(Name = "是否为自定义日期")]
        public bool IsNotFixationDate { get; set; }

        /// <summary>
        /// 是否全部时段
        /// </summary>
        [Display(Name = "是否全部时段")]
        public bool TimeSlot { get; set; }

        /// <summary>
        /// 规则说明
        /// </summary>
        [Display(Name = "规则说明")]
        public string RuleDescription { get; set; }

        /// <summary>
        /// 星期
        /// </summary>
        [Display(Name = "星期")]
        public string Week { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        [Display(Name = "是否启用")]
        public bool IsAvailable { get; set; }

        /// <summary>
        /// 卡片主题列表
        /// </summary>
        [Display(Name = "卡片主题列表")]
        public virtual ICollection<MemberCardTheme> MemberCardThemeList { get; set; }

        /// <summary>
        /// 时间列表
        /// </summary>
        [Display(Name = "卡片主题列表")]
        public virtual ICollection<CardThemeGroupUseTime> UseTimeList { get; set; }
    }
}
