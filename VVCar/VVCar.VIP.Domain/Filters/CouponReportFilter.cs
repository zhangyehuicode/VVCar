using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEF.Core.Dtos;

namespace VVCar.VIP.Domain.Filters
{
    /// <summary>
    /// 卡券报表过滤条件
    /// </summary>
    public class CouponReportFilter : BasePageFilter
    {
        /// <summary>
        ///  类型
        /// </summary>
        [Display(Name = "类型")]
        public int CouponType { get; set; }

        /// <summary>
        ///  优惠券模板编码
        /// </summary>
        [Display(Name = "优惠券模板编码")]
        public string TemplateCode { get; set; }

        /// <summary>
        ///  优惠券模板标题
        /// </summary>
        [Display(Name = "优惠券模板标题")]
        public string TemplateTitle { get; set; }

        /// <summary>
        ///  起始时间
        /// </summary>
        [Display(Name = "起始时间")]
        public DateTime StartTime { get; set; }

        /// <summary>
        ///  结束时间
        /// </summary>
        [Display(Name = "结束时间")]
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 是否限制时间段
        /// </summary>
        [Display(Name = "是否限制时间段")]
        public bool IsLimited { get; set; }

        /// <summary>
        /// 时间是否有效
        /// </summary>
        [Display(Name = "时间是否有效")]
        public bool TimeAvailable { get; set; }
    }
}
