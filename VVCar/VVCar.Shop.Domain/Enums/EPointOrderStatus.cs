using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVCar.Shop.Domain.Enums
{
    /// <summary>
    /// 积分商城订单状态
    /// </summary>
    public enum EPointOrderStatus
    {
        /// <summary>
        /// 未发货
        /// </summary>
        [Description("未发货")]
        Unshipped = 0,

        /// <summary>
        /// 已发货
        /// </summary>
        [Description("已发货")]
        Delivered = 1,
    }
}
