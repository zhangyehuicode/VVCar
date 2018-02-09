using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVCar.VIP.Domain.Enums
{
    /// <summary>
    /// 会员等级权益类型
    /// </summary>
    public enum EGradeRightType
    {
        /// <summary>
        /// 会员折扣
        /// </summary>
        [Description("会员折扣")]
        Discount = 0,

        /// <summary>
        /// 会员产品
        /// </summary>
        [Description("会员产品")]
        Product = 1,
    }
}
