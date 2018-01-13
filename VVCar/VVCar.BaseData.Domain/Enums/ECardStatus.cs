using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace VVCar.BaseData.Domain.Enums
{
    /// <summary>
    /// 卡片状态
    /// </summary>
    public enum ECardStatus
    {
        /// <summary>
        /// 未激活
        /// </summary>
        [Description("未激活")]
        UnActivate = 0,

        /// <summary>
        /// 已激活
        /// </summary>
        [Description("已激活")]
        Activated = 10,

        /// <summary>
        /// 挂失
        /// </summary>
        [Description("挂失")]
        Lost = -1,
    }
}
