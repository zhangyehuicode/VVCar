using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVCar.VIP.Domain.Enums
{
    /// <summary>
    /// 会员查找方式
    /// </summary>
    public enum EMemberFindType
    {
        /// <summary>
        /// 会员卡号或者手机号
        /// </summary>
        [Description("会员卡号或者手机号")]
        Number = 0,

        /// <summary>
        /// 微信OpenID
        /// </summary>
        [Description("微信OpenID")]
        WeChatOpenID = 1,
    }
}
