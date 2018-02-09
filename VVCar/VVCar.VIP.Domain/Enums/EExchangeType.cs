using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVCar.VIP.Domain.Enums
{
    /// <summary>
    /// 兑换类型
    /// </summary>
    public enum EExchangeType
    {
        /// <summary>
        /// 免费
        /// </summary>
        [Description("免费")]
        Free,

        /// <summary>
        /// 积分
        /// </summary>
        [Description("积分")]
        Point
    }
}
