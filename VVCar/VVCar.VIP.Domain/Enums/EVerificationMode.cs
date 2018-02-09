using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVCar.VIP.Domain.Enums
{
    /// <summary>
    /// 核销方式
    /// </summary>
    public enum EVerificationMode
    {
        /// <summary>
        /// 验证码 
        /// </summary>
        [Description("验证码")]
        VerifyCode = 0,

        /// <summary>
        /// 扫码核销
        /// </summary>
        [Description("扫码核销")]
        ScanCode = 1,
    }
}
