using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVCar.VIP.Domain.Enums
{
    /// <summary>
    /// 人员分类
    /// </summary>
    public enum EPeopleSort
    {
        /// <summary>
        /// 会员
        /// </summary>
        [Description("会员")]
        Member = 0,

        /// <summary>
        /// 粉丝
        /// </summary>
        [Description("粉丝")]
        Fans = 1,
    }
}
