using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVCar.BaseData.Domain.Enums
{
    /// <summary>
    /// 审核状态
    /// </summary>
    public enum EAgentDepartmentApproveStatus
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

        /// <summary>
        /// 已导入
        /// </summary>
        [Description("已导入")]
        Imported = 2,
    }
}
