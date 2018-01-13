using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.BaseData.Domain.Enums;

namespace VVCar.BaseData.Domain.Dtos
{
    /// <summary>
    /// 角色拥有权限DTO
    /// </summary>
    public class OwnerPermissionDto
    {
        /// <summary>
        /// 权限类型
        /// </summary>
        public string PermissionType { get; set; }

        /// <summary>
        /// 权限列表
        /// </summary>
        public IEnumerable<OwnerPermissionItemDto> ItemList { get; set; }
    }

    public class OwnerPermissionItemDto
    {
        /// <summary>
        ///权限代码
        /// </summary>
        public string PermissionCode { get; set; }

        /// <summary>
        /// 权限名称
        /// </summary>
        public string PermissionName { get; set; }

        /// <summary>
        /// 权限类型
        /// </summary>
        public EPermissionType PermissionType { get; set; }

        /// <summary>
        /// 是否有权限
        /// </summary>
        public bool IsChecked { get; set; }
    }
}
