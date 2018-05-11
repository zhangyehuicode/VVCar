using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace VVCar.Shop.Domain.Enums
{
    /// <summary>
    /// 产品类型
    /// </summary>
    public enum EProductType
    {
        /// <summary>
        /// 服务
        /// </summary>
        [Description("服务")]
        Service = 0,

        /// <summary>
        /// 商品
        /// </summary>
        [Description("商品")]
        Goods = 1,

        /// <summary>
        /// 会员卡
        /// </summary>
        [Description("会员卡")]
        MemberCard = 2,
    }
}
