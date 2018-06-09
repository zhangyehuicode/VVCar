using System;
using System.ComponentModel.DataAnnotations;

namespace VVCar.Shop.Domain.Dtos
{
    /// <summary>
    /// 服务周期卡券Dto
    /// </summary>
    public class ServicePeriodCouponDto
    {
        /// <summary>
        /// 服务周期卡券ID
        /// </summary>
        [Display(Name = "服务周期卡券ID")]
        public Guid ID { get; set; }

        /// <summary>
        /// 服务周期设置ID
        /// </summary>
        [Display(Name = "服务周期设置ID")]
        public Guid ServicePeriodSettingID { get; set; }

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
    }
}
