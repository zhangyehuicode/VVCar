using System;
using System.Collections.Generic;
using YEF.Core.Data;
using YEF.Core.Domain;
using VVCar.BaseData.Domain.Entities;
using VVCar.BaseData.Domain.Filters;
using VVCar.BaseData.Domain.Dtos;

namespace VVCar.BaseData.Domain.Services
{
    /// <summary>
    /// 功能权限领域服务
    /// </summary>
    public partial interface IPermissionFuncService : IDomainService<IRepository<PermissionFunc>, PermissionFunc, Guid>
    {
        /// <summary>
        /// 获取可用的权限列表
        /// </summary>
        /// <returns></returns>
        IEnumerable<PermissionFunc> GetFuncPermissionList();

        /// <summary>
        ///权限列表--分页及查询条件
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        IEnumerable<PermissionFunc> SearchFuncPermission(PermissionFilter filter, out int totalCount);

        /// <summary>
        /// 分配权限
        /// </summary>
        /// <param name="assignModel"></param>
        /// <returns></returns>
        bool AssignPermission(AssignPermissionDto assignModel);

        /// <summary>
        /// 获取角色拥有的功能权限
        /// </summary>
        /// <param name="roleCode"></param>
        /// <returns></returns>
        IEnumerable<OwnerPermissionDto> GetOwnerPermissionList(string roleCode);

        /// <summary>
        /// 获取角色关联的功能权限代码
        /// </summary>
        /// <param name="roleCodes"></param>
        /// <returns></returns>
        IEnumerable<AssignPermissionDto> GetRolePermissionList(IList<string> roleCodes);

        /// <summary>
        /// 同步权限列表
        /// </summary>
        /// <returns></returns>
        bool SyncPermission();
    }
}
