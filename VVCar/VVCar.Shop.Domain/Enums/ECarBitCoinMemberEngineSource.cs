using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace VVCar.Shop.Domain.Enums
{
    /// <summary>
    /// 车比特会员引擎来源
    /// </summary>
    public enum ECarBitCoinMemberEngineSource
    {
        /// <summary>
        /// 其他
        /// </summary>
        [Description("其他")]
        Other = 0,

        /// <summary>
        /// 购买
        /// </summary>
        [Description("购买")]
        Buy = 1,

        /// <summary>
        /// 赠送
        /// </summary>
        [Description("赠送")]
        Give = 2,
    }
}
