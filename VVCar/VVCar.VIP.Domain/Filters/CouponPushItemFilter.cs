using System;
using System.ComponentModel.DataAnnotations;
using YEF.Core.Dtos;

namespace VVCar.VIP.Domain.Filters
{
    /// <summary>
    /// 卡券推送子项过滤条件
    /// </summary>
    public class CouponPushItemFilter : BasePageFilter
    {
        /// <summary>
        /// 卡券推送ID
        /// </summary>
        [Display(Name = "卡券推送ID")]
        public Guid? CouponPushID { get; set; }

        /// <summary>
        /// 卡券模板ID
        /// </summary>
        [Display(Name = "卡券模板ID")]
        public Guid CouponTemplateID { get; set; }
    }
}
