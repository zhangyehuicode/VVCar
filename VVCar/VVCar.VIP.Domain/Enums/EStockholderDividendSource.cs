using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace VVCar.VIP.Domain.Enums
{
    /// <summary>
    /// 股东分红来源
    /// </summary>
    public enum EStockholderDividendSource
    {
        /// <summary>
        /// 其他
        /// </summary>
        [Description("其他")]
        Other = 0,

        /// <summary>
        /// 下级会员消费
        /// </summary>
        [Description("下级会员消费")]
        MemberConsume = 1,
    }
}
