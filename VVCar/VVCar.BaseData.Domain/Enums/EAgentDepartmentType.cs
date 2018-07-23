using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVCar.BaseData.Domain.Enums
{
    /// <summary>
    /// 客户类型
    /// </summary>
    public enum EAgentDepartmentType
    {
        /// <summary>
        /// 开发客户
        /// </summary>
        [Description("开发客户")]
        Development = 0,

        /// <summary>
        /// 意向客户
        /// </summary>
        [Description("意向客户")]
        Intention = 1
    }
}
