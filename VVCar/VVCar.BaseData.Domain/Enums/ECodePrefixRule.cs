using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace VVCar.BaseData.Domain.Enums
{
    /// <summary>
    /// 编号前缀规则
    /// </summary>
    public enum ECodePrefixRule
    {
        /// <summary>
        /// 无
        /// </summary>
        [Description("无")]
        None = 0,

        /// <summary>
        /// 固定
        /// </summary>
        [Description("固定")]
        Fixed = 1,

        /// <summary>
        /// 部门
        /// </summary>
        [Description("部门")]
        Department = 3,

        /// <summary>
        /// 日期
        /// </summary>
        [Description("日期")]
        Date = 4,
    }
}
