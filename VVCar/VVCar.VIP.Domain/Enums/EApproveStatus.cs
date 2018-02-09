using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVCar.VIP.Domain.Enums
{
    /// <summary>
    /// 审核状态
    /// </summary>
    public enum EApproveStatus
    {
        /// <summary>
        /// 未审核
        /// </summary>
        [Description("未审核")]
        Pedding = 0,

        /// <summary>
        /// 已审核待投放
        /// </summary>
        [Description("已审核待投放")]
        Approved = 1,

        /// <summary>
        /// 已审核已投放
        /// </summary>
        [Description("已审核已投放")]
        Delivered = 2,

        /// <summary>
        /// 已拒绝
        /// </summary>
        [Description("已拒绝")]
        Rejected = -1,
    }
}
