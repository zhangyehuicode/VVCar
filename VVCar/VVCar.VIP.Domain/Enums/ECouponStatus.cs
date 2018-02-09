using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVCar.VIP.Domain.Enums
{
    /// <summary>
    /// 优惠券使用状态
    /// </summary>
    public enum ECouponStatus
    {
        /// <summary>
        /// 未过期
        /// </summary>
        [Description("未使用")]
        Default = 0,

        /// <summary>
        /// 已使用
        /// </summary>
        [Description("已使用")]
        Used = 1,

        /// <summary>
        /// 已作废
        /// </summary>
        [Description("已作废")]
        Blocked = 3,

        /// <summary>
        /// 已过期
        /// </summary>
        [Description("已过期")]
        Expired = -1,
    }
}
