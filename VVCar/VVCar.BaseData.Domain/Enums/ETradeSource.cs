using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace VVCar.BaseData.Domain.Enums
{
    /// <summary>
    /// 交易来源
    /// </summary>
    public enum ETradeSource
    {
        /// <summary>
        /// 微信
        /// </summary>
        [Description("微信")]
        WeChat = 0,

        /// <summary>
        /// 支付宝
        /// </summary>
        [Description("支付宝")]
        Alipay = 1,

        /// <summary>
        /// 管理后台
        /// </summary>
        [Description("管理后台")]
        Portal = 2,

        /// <summary>
        /// 官网自充
        /// </summary>
        [Description("官网自充")]
        WebSite = 3,
    }
}
