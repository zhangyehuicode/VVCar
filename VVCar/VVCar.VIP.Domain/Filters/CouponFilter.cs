using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVCar.VIP.Domain.Filters
{
    /// <summary>
    /// 卡券过滤条件
    /// </summary>
    public class CouponFilter
    {
        /// <summary>
        ///  类型
        /// </summary>
        [Display(Name = "类型")]
        public int CouponType { get; set; }

        /// <summary>
        /// 模板编号
        /// </summary>
        [Display(Name = "模板编号")]
        public string TemplateCode { get; set; }

        /// <summary>
        /// 优惠券模板标题
        /// </summary>
        [Display(Name = "优惠券模板标题")]
        public string TemplateTitle { get; set; }

        /// <summary>
        ///  优惠券编码
        /// </summary>
        [Display(Name = "优惠券编码")]
        public string CouponCode { get; set; }

        /// <summary>
        /// 门店编码
        /// </summary>
        [Display(Name = "门店编码")]
        public string DepartmentCode { get; set; }

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
        /// 分页开始
        /// </summary>
        [Display(Name = "分页开始")]
        public int? Start { get; set; }

        /// <summary>
        /// 分页限制
        /// </summary>
        [Display(Name = "分页限制")]
        public int? Limit { get; set; }

        /// <summary>
        /// 性质
        /// </summary>
        [Display(Name = "性质")]
        public int Nature { get; set; }
    }
}
