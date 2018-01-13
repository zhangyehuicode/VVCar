using System;
using System.Collections.Generic;
using YEF.Core.Data;
using YEF.Core.Domain;
using VVCar.BaseData.Domain.Entities;
using VVCar.BaseData.Domain.Filters;
using YEF.Core.Dtos;

namespace VVCar.BaseData.Domain.Services
{
    public partial interface IUserRoleService : IDomainService<IRepository<UserRole>, UserRole, Guid>
    {
        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="userRoles">人员角色</param>
        /// <returns></returns>
        bool BatchAdd(IEnumerable<UserRole> userRoles);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="userRoleIDs">人员角色主键ID</param>
        /// <returns></returns>
        bool BatchDelete(IEnumerable<Guid> userRoleIDs);

        /// <summary>
        /// 过滤数据
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        PagedResultDto<UserRole> QueryData(UserRoleFilter filter);
    }
}
