using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVCar.VIP.Domain.Enums
{
    /// <summary>
    /// 优惠类型
    /// </summary>
    public enum ECouponType
    {
        /// <summary>
        /// 代金
        /// </summary>
        [Description("代金")]
        CashCoupon = 0,

        /// <summary>
        /// 抵用
        /// </summary>
        [Description("抵用")]
        Voucher = 1,

        /// <summary>
        /// 兑换
        /// </summary>
        [Description("兑换")]
        Exchange = 2,

        /// <summary>
        /// 折扣
        /// </summary>
        [Description("折扣")]
        Discount = 3,
    }
}
