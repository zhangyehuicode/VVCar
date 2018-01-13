using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVCar.BaseData.Domain.Dtos
{
    /// <summary>
    /// 用户信息
    /// </summary>
    public class UserInfoDto
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// 用户编号
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 用户姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 所属门店ID
        /// </summary>
        public Guid DepartmentID { get; set; }

        /// <summary>
        /// 所属门店编号
        /// </summary>
        public string DepartmentCode { get; set; }

        /// <summary>
        /// 所属门店名称
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// 是否可用
        /// </summary>
        public bool IsAvailable { get; set; }

        /// <summary>
        /// 是否可以登录管理后台
        /// </summary>
        public bool CanLoginAdminPortal { get; set; }
    }
}
