using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace VVCar.BaseData.Domain.Enums
{
    /// <summary>
    /// 权限类型
    /// </summary>
    public enum EPermissionType
    {
        /// <summary>
        /// 管理后台菜单权限
        /// </summary>
        [Description("管理后台菜单")]
        PortalMenu = 0,

        /// <summary>
        /// 管理后台功能权限
        /// </summary>
        [Description("管理后台功能")]
        PortalAction = 1,
    }
}
