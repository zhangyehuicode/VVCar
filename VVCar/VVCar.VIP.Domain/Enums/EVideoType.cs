using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVCar.VIP.Domain.Enums
{
    /// <summary>
    /// 视频类型
    /// </summary>
    public enum EVideoType
    {
        /// <summary>
        /// 总部视频
        /// </summary>
        [Description("总部视频")]
        HQ = 0,

        /// <summary>
        /// 代理商视频
        /// </summary>
        [Description("代理商视频")]
        Agent = 1,
    }
}
