using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVCar.BaseData.Domain.Enums
{
    /// <summary>
    /// 代理商门店数据来源
    /// </summary>
    public enum EAgentDepartmentSource
    {
        /// <summary>
        /// 微信
        /// </summary>
        [Description("微信")]
        Wechat = 0,

        /// <summary>
        /// 管理后台
        /// </summary>
        [Description("后台")]
        Portal = 1,
    }
}
