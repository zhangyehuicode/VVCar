using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVCar.Shop.Domain.Enums
{
    /// <summary>
    /// 交易订单类型
    /// </summary>
    public enum EShopTradeOrderType
    {
        /// <summary>
        /// 其他
        /// </summary>
        [Description("其他")]
        Other = 0,

        /// <summary>
        /// 商城订单
        /// </summary>
        [Description("商城订单")]
        Order = 1,

        /// <summary>
        /// 接车单
        /// </summary>
        [Description("接车单")]
        PickupOrder = 2,
    }
}
