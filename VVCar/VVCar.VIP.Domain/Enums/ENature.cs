using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace VVCar.VIP.Domain.Enums
{
    /// <summary>
    /// 性质
    /// </summary>
    public enum ENature
    {
        /// <summary>
        /// 券
        /// </summary>
        [Description("券")]
        Coupon = 0,

        /// <summary>
        /// 卡
        /// </summary>
        [Description("卡")]
        Card = 1,
    }
}
