using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVCar.Shop.Domain.Enums
{
    /// <summary>
    /// 商城订单状态
    /// </summary>
    public enum EOrderStatus
    {
        /// <summary>
        /// 未付款
        /// </summary>
        [Description("未付款")]
        UnPay = -1,

        /// <summary>
        /// 已付款未发货
        /// </summary>
        [Description("已付款未发货")]
        PayUnshipped = 0,

        /// <summary>
        /// 已发货
        /// </summary>
        [Description("已发货")]
        Delivered = 1,

        /// <summary>
        /// 已完成
        /// </summary>
        [Description("已完成")]
        Finish = 2,
    }
}
