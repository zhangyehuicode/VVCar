using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVCar.VIP.Domain.Enums
{
    /// <summary>
    /// 投放方式
    /// </summary>
    public enum EDeliveryMode
    {
        /// <summary>
        /// 直接群发
        /// </summary>
        [Description("直接群发")]
        Mass = 0,

        /// <summary>
        /// 定向发送
        /// </summary>
        [Description("定向发送")]
        Directional = 1,

        /// <summary>
        /// 下载二维码
        /// </summary>
        [Description("下载二维码")]
        LoadQRCode = 2,
    }
}
