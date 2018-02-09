using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVCar.VIP.Domain.Filters
{
    /// <summary>
    /// 赠送卡券过滤条件
    /// </summary>
    public class GivenCouponFilter
    {
        /// <summary>
        /// 卡券ID
        /// </summary>
        [Display(Name = "卡券ID")]
        public Guid? CouponID { get; set; }
    }
}
