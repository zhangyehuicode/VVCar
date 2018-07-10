using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVCar.VIP.Domain.Enums
{
    /// <summary>
    /// 业务报销审核状态
    /// </summary>
    public enum EReimbursementApproveStatus
    {
        /// <summary>
        /// 未审核
        /// </summary>
        [Description("未审核")]
        Pedding = 0,

        /// <summary>
        /// 已审核
        /// </summary>
        [Description("已审核")]
        Approved = 1,
    }
}
