using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEF.Core.Data;

namespace VVCar.VIP.Domain.Entities
{
    /// <summary>
    /// 访问记录统计
    /// </summary>
    public class VisitRecord : NormalEntityBase
    {
        /// <summary>
        /// 标识ID
        /// </summary>
        [Display(Name = "标识ID")]
        public Guid IdentifyID { get; set; }

        /// <summary>
        /// CouponTemplate
        /// </summary>
        public virtual CouponTemplate CouponTemplate { get; set; }

        /// <summary>
        /// 访问日期
        /// </summary>
        [Display(Name = "访问日期")]
        public DateTime VisitDate { get; set; }

        /// <summary>
        /// 访问量
        /// </summary>
        [Display(Name = "访问量")]
        public int PV { get; set; }
    }
}
