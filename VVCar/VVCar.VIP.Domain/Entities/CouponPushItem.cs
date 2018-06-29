using System;
using System.ComponentModel.DataAnnotations;
using YEF.Core.Data;

namespace VVCar.VIP.Domain.Entities
{
    /// <summary>
    /// 卡券推送子表
    /// </summary>
    public class CouponPushItem : EntityBase
    {
        /// <summary>
        /// 卡券推送ID
        /// </summary>
        [Display(Name ="卡券推送ID")]
        public Guid CouponPushID { get; set; }

        /// <summary>
        /// 卡券模板ID
        /// </summary>
        [Display(Name = "卡券模板ID")]
        public Guid CouponTemplateID { get; set; }

        /// <summary>
        /// 优惠券模板标题
        /// </summary>
        [Display(Name = "优惠券模板标题")]
        public string CouponTemplateTitle { get; set; }

        /// <summary>
        /// 卡券推送
        /// </summary>
        [Display(Name = "卡券")]
        public virtual CouponTemplate CouponTemplate { get; set; }

        /// <summary>
        /// 创建人ID
        /// </summary>
        [Display(Name = "创建人ID")]
        public Guid CreatedUserID { get; set; }

        /// <summary>
        /// 创建人名称
        /// </summary>
        [Display(Name = "创建人名称")]
        public string CreatedUser { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        [Display(Name = "创建日期")]
        public DateTime CreatedDate { get; set; }

        ///// <summary>
        ///// 被推送ID(人员ID/分组ID)
        ///// </summary>
        //[Display(Name = "被推送ID(人员ID/分组ID)")]
        //public Guid? PushedID { get; set; }

        ///// <summary>
        ///// 类型(0:人员,1:分组)
        ///// </summary>
        //[Display(Name = "类型")]
        //public ECouponPushItemType Type { get; set; }
    }
}
