using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace VVCar.Shop.Domain.Enums
{
    /// <summary>
    /// 支付类型
    /// </summary>
    public enum EPayType
    {
        /// <summary>
        /// 其他
        /// </summary>
        [Description("其他")]
        Other = 0,

        /// <summary>
        /// 微信
        /// </summary>
        [Description("微信")]
        WeChat = 1,

        /// <summary>
        /// 现金
        /// </summary>
        [Description("现金")]
        Cash = 2,

        /// <summary>
        /// 会员卡
        /// </summary>
        [Description("会员卡")]
        MemberCard = 3,

        /// <summary>
        /// 优惠券
        /// </summary>
        [Description("优惠券")]
        Coupon = 4,

        /// <summary>
        /// 车比特
        /// </summary>
        [Description("车比特")]
        CarBitCoin = 5,
    }
}
