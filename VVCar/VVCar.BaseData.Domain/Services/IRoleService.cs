using System;
using System.Collections.Generic;
using YEF.Core.Data;
using YEF.Core.Domain;
using VVCar.BaseData.Domain.Entities;
using VVCar.BaseData.Domain.Filters;

namespace VVCar.BaseData.Domain.Services
{
    public partial interface IRoleService : IDomainService<IRepository<Role>, Role, Guid>
    {
        /// <summary>
        /// 人员角色查询
        /// </summary>
        /// <param name="filter">查询条件</param>
        /// <returns></returns>
        IEnumerable<Role> Query(RoleFilter filter);
    }
}
