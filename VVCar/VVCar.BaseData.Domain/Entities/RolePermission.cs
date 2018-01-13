using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.BaseData.Domain.Enums;
using YEF.Core.Data;

namespace VVCar.BaseData.Domain.Entities
{
    /// <summary>
    /// 角色权限关联表
    /// </summary>
    public partial class RolePermission : EntityBase
    {
        /// <summary>
        /// 角色代码
        /// </summary>
        [Display(Name = "角色代码")]
        public String RoleCode { get; set; }

        /// <summary>
        /// 权限代码
        /// </summary>
        [Display(Name = "权限代码")]
        public string PermissionCode { get; set; }

        /// <summary>
        /// 权限类型
        /// </summary>
        [Display(Name = "权限类型")]
        public EPermissionType PermissionType { get; set; }

    }
}
