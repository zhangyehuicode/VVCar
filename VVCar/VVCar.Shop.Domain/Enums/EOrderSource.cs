using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVCar.Shop.Domain.Enums
{
    /// <summary>
    /// 订单来源
    /// </summary>
    public enum EOrderSource
    {
        /// <summary>
        /// 商城下单
        /// </summary>
        Shop = 0,

        /// <summary>
        /// 手动添加
        /// </summary>
        Manual = 1,
    }
}
