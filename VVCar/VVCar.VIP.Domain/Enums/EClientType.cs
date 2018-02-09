using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVCar.VIP.Domain.Enums
{
    /// <summary>
    /// 访问客户端类型
    /// </summary>
    public enum EClientType
    {
        /// <summary>
        /// 未知
        /// </summary>
        [Description("未知")]
        None = 0,

        /// <summary>
        /// 管理后台
        /// </summary>
        [Description("管理后台")]
        Portal = 1,

        /// <summary>
        /// 微信
        /// </summary>
        [Description("微信")]
        WeChat = 2,

        /// <summary>
        /// 礼品卡
        /// </summary>
        [Description("礼品卡")]
        GiftCard = 3,
    }
}
