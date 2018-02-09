using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVCar.VIP.Domain.Enums
{
    /// <summary>
    /// 卡券模板可用时段类别
    /// </summary>
    public enum EUseTimeType
    {
        /// <summary>
        /// 使用优惠券可用时段
        /// </summary>
        [Description("使用优惠券可用时段")]
        Use = 0,

        /// <summary>
        /// 投放可用时段
        /// </summary>
        [Description("投放可用时段")]
        PutIn = 1,
    }
}
