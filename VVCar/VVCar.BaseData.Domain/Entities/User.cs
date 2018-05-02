using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.BaseData.Domain.Enums;
using YEF.Core.Data;
using YEF.Core.Enums;

namespace VVCar.BaseData.Domain.Entities
{
    /// <summary>
    /// 用户档案
    /// </summary>
    public partial class User : EntityBase, IDepartmentEntity
    {
        /// <summary>
        /// 用户档案
        /// </summary>
        public User()
        {
            this.UserRoles = new List<UserRole>();
        }

        /// <summary>
        /// 用户编号
        /// </summary>
        [Display(Name = "用户编号")]
        public string Code { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        [Display(Name = "用户名称")]
        public string Name { get; set; }

        /// <summary>
        /// 门店ID
        /// </summary>
        [Display(Name = "门店ID")]
        public Guid DepartmentID { get; set; }

        /// <summary>
        /// 所属部门
        /// </summary>
        [Display(Name = "所属部门")]
        public virtual Department Department { get; set; }

        /// <summary>
        /// 登录密码
        /// </summary>
        [Display(Name = "登录密码")]
        public string Password { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        [Display(Name = "性别")]
        public ESex Sex { get; set; }

        /// <summary>
        /// 电话号码
        /// </summary>
        [Display(Name = "电话号码")]
        public string PhoneNo { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        [Display(Name = "手机号码")]
        public string MobilePhoneNo { get; set; }

        /// <summary>
        /// 电子邮件
        /// </summary>
        [Display(Name = "电子邮件")]
        public string EmailAddress { get; set; }

        /// <summary>
        /// 权限卡
        /// </summary>
        [Display(Name = "权限卡")]
        public string AuthorityCard { get; set; }

        /// <summary>
        /// 是否可用
        /// </summary>
        [Display(Name = "是否可用")]
        public bool IsAvailable { get; set; }

        /// <summary>
        /// 是否可以登录管理后台
        /// </summary>
        [Display(Name = "是否可以登录管理后台")]
        public bool CanLoginAdminPortal { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Display(Name = "备注")]
        public string Remark { get; set; }

        /// <summary>
        /// OpenID
        /// </summary>
        [Display(Name = "OpenID")]
        public string OpenID { get; set; }

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

        /// <summary>
        /// 最后修改人ID
        /// </summary>
        [Display(Name = "最后修改人ID")]
        public Guid? LastUpdateUserID { get; set; }

        /// <summary>
        /// 最后修改人名称
        /// </summary>
        [Display(Name = "最后修改人名称")]
        public string LastUpdateUser { get; set; }

        /// <summary>
        /// 最后修改日期
        /// </summary>
        [Display(Name = "最后修改日期")]
        public DateTime? LastUpdateDate { get; set; }

        /// <summary>
        /// 数据来源
        /// </summary>
        [Display(Name = "数据来源")]
        public EDataSource DataSource { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        [Display(Name = "角色")]
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
