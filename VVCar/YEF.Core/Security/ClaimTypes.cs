using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YEF.Core.Security
{
    /// <summary>
    /// 定义显示声明类型的常量
    /// </summary>
    public class ClaimTypes
    {
        /// <summary>
        /// 用户Code
        /// </summary>
        public const string UserCode = "http://schemas.VVCar.com/identity/claims/userCode";

        /// <summary>
        /// 所属部门ID
        /// </summary>
        public const string DepartmentId = "http://schemas.VVCar.com/identity/claims/departmentId";

        /// <summary>
        /// 所属部门名称
        /// </summary>
        public const string DepartmentName = "http://schemas.VVCar.com/identity/claims/departmentName";

        /// <summary>
        /// 商户号
        /// </summary>
        public const string CompanyCode = "http://schemas.VVCar.com/identity/claims/companyCode";
    }
}
