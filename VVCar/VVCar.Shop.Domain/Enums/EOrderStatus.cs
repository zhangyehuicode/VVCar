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
        /// 挂账
        /// </summary>
        [Description("挂账")]
        OnCredit = -2,

        /// <summary>
        /// 付款不足
        /// </summary>
        [Description("付款不足")]
        UnEnough = -1,

        /// <summary>
        /// 未付款
        /// </summary>
        [Description("未付款")]
        UnPay = 0,

        /// <summary>
        /// 已付款未发货
        /// </summary>
        [Description("已付款未发货")]
        PayUnshipped = 1,

        /// <summary>
        /// 已发货
        /// </summary>
        [Description("已发货")]
        Delivered = 2,

        /// <summary>
        /// 已完成
        /// </summary>
        [Description("已完成")]
        Finish = 3,
    }
}
