using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEF.Core.Data;

namespace VVCar.BaseData.Domain.Entities
{
    /// <summary>
    /// 用户角色关联表
    /// </summary>
    public class UserRole : NormalEntityBase
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        [Display(Name = "用户ID")]
        public Guid UserID { get; set; }

        /// <summary>
        /// 用户
        /// </summary>
        [Display(Name = "用户")]
        public virtual User User { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        [Display(Name = "角色ID")]
        public Guid RoleID { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        [Display(Name = "角色")]
        public virtual Role Role { get; set; }

        /// <summary>
        /// 创建人ID
        /// </summary>
        [Display(Name = "创建人ID")]
        public Guid CreatedUserID { get; set; }

        /// <summary>
        /// 创建人名称
        /// </summary>
        [Display(Name = "创建人名称")]
        public string CreatedUser { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        [Display(Name = "创建日期")]
        public DateTime CreatedDate { get; set; }
    }
}
