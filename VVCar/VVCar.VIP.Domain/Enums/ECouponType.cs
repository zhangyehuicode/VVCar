using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVCar.VIP.Domain.Enums
{
    /// <summary>
    /// 优惠券类型
    /// </summary>
    public enum ECouponType
    {
        /// <summary>
        /// 代金券, #e59e04
        /// </summary>
        [Description("代金券")]
        CashCoupon = 0,

        /// <summary>
        /// 抵用券, #d0459d
        /// </summary>
        [Description("抵用券")]
        Voucher = 1,

        /// <summary>
        /// 兑换券, #2a8bc2
        /// </summary>
        [Description("兑换券")]
        Exchange = 2,

        /// <summary>
        /// 折扣券, #1cac2a
        /// </summary>
        [Description("折扣券")]
        Discount = 3,
    }
}
