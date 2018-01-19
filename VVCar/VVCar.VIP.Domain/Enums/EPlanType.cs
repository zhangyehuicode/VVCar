using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace VVCar.VIP.Domain.Enums
{
    /// <summary>
    /// 优惠类型
    /// </summary>
    public enum EPlanType
    {
        /// <summary>
        /// 活动
        /// </summary>
        [Description("活动")]
        Activity = 1,

        /// <summary>
        /// 满送
        /// </summary>
        [Description("满送")]
        FullGive = 2,
    }
}
