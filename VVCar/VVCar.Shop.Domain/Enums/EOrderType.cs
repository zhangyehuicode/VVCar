using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVCar.Shop.Domain.Enums
{
    /// <summary>
    /// 订单类型
    /// </summary>
    public enum EOrderType
    {
        /// <summary>
        /// 商品
        /// </summary>
        [Description("商品")]
        Goods = 0,

        /// <summary>
        /// 会员卡
        /// </summary>
        [Description("会员卡")]
        MemberCard = 1,
    }
}
