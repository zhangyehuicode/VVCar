using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace VVCar.BaseData.Domain.Enums
{
    /// <summary>
    /// 菜单类型
    /// </summary>
    public enum ESysMenuType
    {
        /// <summary>
        /// 组件
        /// </summary>
        [Description("组件")]
        Component = 1,

        /// <summary>
        /// 路径
        /// </summary>
        [Description("路径")]
        Path = 2
    }
}
