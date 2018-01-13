using System;
using System.Collections.Generic;
using System.Linq;
using YEF.Core;
using YEF.Core.Data;
using YEF.Core.Domain;
using VVCar.BaseData.Domain.Entities;
using VVCar.BaseData.Domain.Services;

namespace VVCar.BaseData.Services.DomainServices
{
    public partial class RolePermissionService : DomainServiceBase<IRepository<RolePermission>, RolePermission, Guid>, IRolePermissionService
    {
        public RolePermissionService()
        {
        }
    }
}
