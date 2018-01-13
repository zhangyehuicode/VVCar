using System;
using System.Collections.Generic;
using YEF.Core.Data;
using YEF.Core.Domain;
using VVCar.BaseData.Domain.Entities;

namespace VVCar.BaseData.Domain.Services
{
    public partial interface IRolePermissionService : IDomainService<IRepository<RolePermission>, RolePermission, Guid>
    {
    }
}
