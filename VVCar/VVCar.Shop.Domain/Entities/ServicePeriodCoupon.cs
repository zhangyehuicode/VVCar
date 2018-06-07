using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEF.Core.Data;
using System.ComponentModel.DataAnnotations;

namespace VVCar.Shop.Domain.Entities
{
    /// <summary>
    /// 服务周期 到期推送券
    /// </summary>
    public class ServicePeriodCoupon : EntityBase
    {
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
