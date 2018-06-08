using System;
using System.ComponentModel.DataAnnotations;

namespace VVCar.VIP.Domain.Dtos
{
    /// <summary>
    /// 卡券推送子项 Dto
    /// </summary>
    public class CouponPushItemDto
    {
        /// <summary>
        /// 卡券推送子项ID
        /// </summary>
        [Display(Name = "卡券推送子项ID")]
        public Guid ID { get; set; }

        /// <summary>
        /// 卡券模板ID
        /// </summary>
        [Display(Name = "卡券模板ID")]
        public Guid CouponTemplateID { get; set; }

        /// <summary>
        /// 优惠券模板编号
        /// </summary>
        [Display(Name = "优惠券模板编号")]
        public string TemplateCode { get; set; }

        /// <summary>
        /// 优惠券模板标题
        /// </summary>
        [Display(Name = "优惠券模板标题")]
        public string CouponTemplateTitle { get; set; }
    }
}
