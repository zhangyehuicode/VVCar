using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.VIP.Domain.Enums;
using YEF.Core.Data;

namespace VVCar.VIP.Domain.Entities
{
    /// <summary>
    /// 卡券模板可用时段
    /// </summary>
    public class CouponTemplateUseTime : NormalEntityBase
    {
        /// <summary>
        /// 优惠券模板ID
        /// </summary>
        [Display(Name = "优惠券模板ID")]
        public Guid TemplateID { get; set; }

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

        /// <summary>
        /// 时段类别
        /// </summary>
        [Display(Name = "时段类别")]
        public EUseTimeType Type { get; set; }
    }
}
