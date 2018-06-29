using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace VVCar.Shop.Domain.Enums
{
    /// <summary>
    /// 车比特分配状态
    /// </summary>
    public enum ECarBitCoinDistributionStatus
    {
        /// <summary>
        /// 未转换
        /// </summary>
        [Description("未转换")]
        UnTransform = 0,

        /// <summary>
        /// 已转换
        /// </summary>
        [Description("已转换")]
        Transformed = 1,
    }
}
