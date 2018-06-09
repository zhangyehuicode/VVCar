using System;
using System.ComponentModel.DataAnnotations;
using YEF.Core.Dtos;

namespace VVCar.Shop.Domain.Filters
{
    /// <summary>
    /// 服务周期卡券过滤条件
    /// </summary>
    public class ServicePeriodCouponFilter : BasePageFilter
    {
        /// <summary>
        /// 服务周期设置ID
        /// </summary>
        [Display(Name = "服务周期设置ID")]
        public Guid? ServicePeriodSettingID { get; set; }
    }
}
