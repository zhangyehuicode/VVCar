using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVCar.VIP.Domain.Enums
{
    /// <summary>
    /// 推送状态
    /// </summary>
    public enum ECouponPushStatus
    {
        /// <summary>
        /// 未推送
        /// </summary>
        [Description("未推送")]
        NotPush = 0,

        /// <summary>
        /// 已推送
        /// </summary>
        [Description("已推送")]
        Pushed = 1,

        /// <summary>
        /// 终止推送
        /// </summary>
        [Description("终止推送")]
        Cancel = -1,
    }
}
