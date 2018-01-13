using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace VVCar.BaseData.Domain.Enums
{
    /// <summary>
    /// 支付方式
    /// </summary>
    public enum EPaymentType
    {
        /// <summary>
        /// 现金
        /// </summary>
        [Description("现金")]
        Cash = 0,

        /// <summary>
        /// 支付宝
        /// </summary>
        [Description("支付宝")]
        Alipay = 1,

        /// <summary>
        /// 微信
        /// </summary>
        [Description("微信")]
        WeChat = 2,

        /// <summary>
        /// 银行卡
        /// </summary>
        [Description("银行卡")]
        BankCard = 3,

        /// <summary>
        /// 初始余额储值
        /// </summary>
        [Description("初始余额储值")]
        Initial = 4,

        /// <summary>
        /// 百度糯米
        /// </summary>
        [Description("百度糯米")]
        Nuomi = 5,

        /// <summary>
        /// 营销活动
        /// </summary>
        [Description("营销活动")]
        MarketingActivity = 6,

        /// <summary>
        /// 余额调整
        /// </summary>
        [Description("余额调整")]
        AdjustBalance = 7,

        /// <summary>
        /// 买赠
        /// </summary>
        [Description("买赠")]
        GiveAway = 8,
    }
}
