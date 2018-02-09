using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVCar.VIP.Domain.Dtos
{
    /// <summary>
    /// 卡片主题分组 Dto
    /// </summary>
    public class CardThemeGroupDto
    {
        /// <summary>
        /// ID
        /// </summary>
        [Display(Name = "ID")]
        public Guid ID { get; set; }

        /// <summary>
        /// 推荐类别ID
        /// </summary>
        [Display(Name = "推荐类别ID")]
        public Guid CardThemeCategoryID { get; set; }

        /// <summary>
        /// 类别等级
        /// </summary>
        [Display(Name = "类别等级")]
        public int CategoryGrade { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Display(Name = "名称")]
        public string Name { get; set; }

        /// <summary>
        /// 类别名称
        /// </summary>
        [Display(Name = "类别名称")]
        public string CategoryName { get; set; }

        /// <summary>
        /// 编号
        /// </summary>
        [Display(Name = "编号")]
        public string Code { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [Display(Name = "排序")]
        public int Index { get; set; }

        /// <summary>
        /// 主题Url
        /// </summary>
        [Display(Name = "主题Url")]
        public string ThemeUrl { get; set; }


        /// <summary>
        /// 开始日期
        /// </summary>
        [Display(Name = "开始日期")]
        public DateTime GiftCardStartTime { get; set; }

        /// <summary>
        /// 截止日期
        /// </summary>
        [Display(Name = "截止日期")]
        public DateTime GiftCardEndTime { get; set; }

        /// <summary>
        /// 时间段(是否全部)
        /// </summary>
        [Display(Name = "是否全部")]
        public bool TimeSlot { get; set; }


        /// <summary>
        /// 门店编号
        /// </summary>
        [Display(Name = "编号")]
        public string DepartmentCode { get; set; }


        /// <summary>
        /// 关联分组
        /// </summary>
        [Display(Name = "关联分组")]
        public Guid? RecommendGroupID { get; set; }


        /// <summary>
        /// 规则说明
        /// </summary>
        [Display(Name = "规则说明")]
        public string RuleDescription { get; set; }

        /// <summary>
        /// 星期
        /// </summary>
        [Display(Name = "星期")]
        public string week { get; set; }

        /// <summary>
        /// 后台展示图片
        /// </summary>
        [Display(Name = "主题Url")]
        public string ImgUrlDto { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        [Display(Name = "是否启用")]
        public bool IsAvailable { get; set; }

        /// <summary>
        /// 是否启用说明
        /// </summary>
        [Display(Name = "是否启用")]
        public string IsAvailableShow { get; set; }

        /// <summary>
        /// 是否逻辑删除
        /// </summary>
        [Display(Name = "是否逻辑删除")]
        public bool IsDeleted { get; set; }

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
    }
}
