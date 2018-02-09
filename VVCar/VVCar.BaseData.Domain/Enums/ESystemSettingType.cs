using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace VVCar.BaseData.Domain.Enums
{
    /// <summary>
    /// 系统设置类型
    /// </summary>
    public enum ESystemSettingType
    {
        /// <summary>
        /// 过滤字段
        /// </summary>
        [Description("过滤字段")]
        FilterField = 0,

        /// <summary>
        /// 过滤SQL
        /// </summary>
        [Description("过滤SQL")]
        FilterSQL = 1,

        /// <summary>
        /// 临时字段
        /// </summary>
        [Description("临时字段")]
        TempField = 2,

        /// <summary>
        /// 参数变量
        /// </summary>
        [Description("参数变量")]
        Parameter = 9,
    }
}
