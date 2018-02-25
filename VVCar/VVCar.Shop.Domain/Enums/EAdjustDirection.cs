using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVCar.Shop.Domain.Enums
{
    /// <summary>
    /// 调整方向
    /// </summary>
    public enum EAdjustDirection
    {
        /// <summary>
        /// 上调
        /// </summary>
        [Description("上调")]
        Up = 1,

        /// <summary>
        /// 下调
        /// </summary>
        [Description("下调")]
        Down = 2,
    }
}
