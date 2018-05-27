using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace VVCar.Shop.Domain.Enums
{
    /// <summary>
    /// 接车单状态
    /// </summary>
    public enum EPickUpOrderStatus
    {
        /// <summary>
        /// 未付款
        /// </summary>
        [Description("未付款")]
        UnPay = 0,

        /// <summary>
        /// 已付款
        /// </summary>
        [Description("已付款")]
        Payed = 1,

        /// <summary>
        /// 收款不足
        /// </summary>
        [Description("收款不足")]
        UnEnough = 2,
    }
}
