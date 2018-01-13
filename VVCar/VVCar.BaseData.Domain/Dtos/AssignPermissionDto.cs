using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VVCar.BaseData.Domain.Enums;

namespace VVCar.BaseData.Domain.Dtos
{
    /// <summary>
    /// 分配权限模型
    /// </summary>
    public class AssignPermissionDto
    {
        /// <summary>
        /// 角色代码
        /// </summary>
        public string RoleCode { get; set; }

        /// <summary>
        /// 权限代码
        /// </summary>
        public string PermissionCode { get; set; }

        /// <summary>
        /// 权限类型
        /// </summary>
        public EPermissionType PermissionType { get; set; }

        /// <summary>
        /// 是否有权限 true 为有，flase 为没有
        /// </summary>
        public bool IsChecked { get; set; }
    }
}
