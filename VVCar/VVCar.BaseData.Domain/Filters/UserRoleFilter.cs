using System;
using YEF.Core.Dtos;

namespace VVCar.BaseData.Domain.Filters
{
    /// <summary>
    /// 用户角色过滤条件
    /// </summary>
    public class UserRoleFilter : BasePageFilter
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        public Guid? RoleID { get; set; }
    }
}
