using System;
using System.Collections.Generic;
using System.Linq;
using YEF.Core;
using YEF.Core.Data;
using YEF.Core.Domain;
using VVCar.BaseData.Domain.Entities;
using VVCar.BaseData.Domain.Filters;
using VVCar.BaseData.Domain.Services;
using YEF.Core.Dtos;

namespace VVCar.BaseData.Services.DomainServices
{
    public partial class UserRoleService : DomainServiceBase<IRepository<UserRole>, UserRole, Guid>, IUserRoleService
    {
        public UserRoleService()
        {
        }

        #region methods
        public override UserRole Add(UserRole entity)
        {
            if (entity == null)
                return null;
            entity.ID = Util.NewID();
            entity.CreatedUserID = AppContext.CurrentSession.UserID;
            entity.CreatedUser = AppContext.CurrentSession.UserName;
            entity.CreatedDate = DateTime.Now;
            return base.Add(entity);
        }
        #endregion

        #region IUserRoleService 成员

        public bool BatchAdd(IEnumerable<UserRole> userRoles)
        {
            if (userRoles == null || userRoles.Count() < 1)
                throw new DomainException("新增失败，没有数据");
            var userRoleList = userRoles.ToList();
            var roleId = userRoleList.First().RoleID;
            var userIds = userRoleList.Select(t => t.UserID).Distinct();
            var existData = this.Repository.GetQueryable(false)
                .Where(t => t.RoleID == roleId && userIds.Contains(t.UserID))
                .Select(t => t.UserID).ToList();
            if (existData.Count > 0)
            {
                userRoleList.RemoveAll(t => existData.Contains(t.UserID));
            }
            if (userRoleList.Count < 1)
                return true;
            foreach (var userRole in userRoleList)
            {
                userRole.ID = Util.NewID();
                userRole.CreatedUserID = AppContext.CurrentSession.UserID;
                userRole.CreatedUser = AppContext.CurrentSession.UserName;
                userRole.CreatedDate = DateTime.Now;
            }
            this.Repository.Add(userRoleList);
            return true;
        }

        public bool BatchDelete(IEnumerable<Guid> userRoleIDs)
        {
            var userRoles = new List<UserRole>();
            foreach (var userRoleID in userRoleIDs)
            {
                userRoles.Add(new UserRole()
                {
                    ID = userRoleID,
                });
            }
            return this.Repository.Delete(userRoles) > 0;
        }

        public PagedResultDto<UserRole> QueryData(UserRoleFilter filter)
        {
            if (filter == null || !filter.RoleID.HasValue)
                throw new DomainException("查询参数错误");
            var result = new PagedResultDto<UserRole>();
            var queryable = this.Repository.GetInclude(t => t.User).Where(t => t.RoleID == filter.RoleID);
            if (filter.Start.HasValue && filter.Limit.HasValue)
            {
                result.TotalCount = queryable.Count();
                result.Items = queryable.OrderBy(t => t.ID).Skip(filter.Start.Value).Take(filter.Limit.Value);
            }
            else
            {
                result.Items = queryable.ToArray();
                result.TotalCount = result.Items.Count();
            }
            return result;
        }

        #endregion
    }
}
