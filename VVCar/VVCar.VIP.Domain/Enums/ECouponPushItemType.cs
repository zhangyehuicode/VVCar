using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVCar.VIP.Domain.Enums
{
    /// <summary>
    /// 卡券推送子项类型
    /// </summary>
    public enum ECouponPushItemType
    {
        /// <summary>
        /// 人员
        /// </summary>
        [Description("人员")]
        People = 0,

        /// <summary>
        /// 分组
        /// </summary>
        [Description("分组")]
        Group = 1
    }
}
