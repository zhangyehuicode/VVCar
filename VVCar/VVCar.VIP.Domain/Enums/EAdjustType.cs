using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVCar.VIP.Domain.Enums
{
    /// <summary>
    /// 调整方式
    /// </summary>
    [Description("调整方式")]
    public enum EAdjustType
    {
        /// <summary>
        /// 无效操作
        /// </summary>
        [Description("无效操作")]
        None = 0,

        /// <summary>
        /// 增加余额
        /// </summary>
        [Description("增加余额")]
        Increase = 1,

        /// <summary>
        /// 减少余额
        /// </summary>
        [Description("减少余额")]
        Decrease = 2,
    }
}
