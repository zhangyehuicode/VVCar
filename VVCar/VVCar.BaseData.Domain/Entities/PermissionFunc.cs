using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEF.Core.Data;
using System.ComponentModel.DataAnnotations;
using VVCar.BaseData.Domain.Enums;

namespace VVCar.BaseData.Domain.Entities
{
    /// <summary>
    /// 权限
    /// </summary>
    public class PermissionFunc : EntityBase
    {
        /// <summary>
        ///权限代码
        /// </summary>
        [Display(Name = "权限代码")]
        public string Code { get; set; }

        /// <summary>
        /// 权限名称
        /// </summary>
        [Display(Name = "权限名称")]
        public string Name { get; set; }

        /// <summary>
        /// 权限类型
        /// </summary>
        [Display(Name = "权限类型")]
        public EPermissionType PermissionType { get; set; }

        /// <summary>
        /// 是否手动配置
        /// </summary>
        [Display(Name = "是否手动配置")]
        public bool IsManual { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        [Display(Name = "是否启用")]
        public bool IsAvailable { get; set; }
    }
}
