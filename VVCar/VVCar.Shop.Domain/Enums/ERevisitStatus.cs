using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace VVCar.Shop.Domain.Enums
{
    /// <summary>
    /// 回访状态
    /// </summary>
    public enum ERevisitStatus
    {
        /// <summary>
        /// 未回访
        /// </summary>
        [Description("未回访")]
        UnRevisit = 0,

        /// <summary>
        /// 已回访
        /// </summary>
        [Description("已回访")]
        Revisited = 1,
    }
}
