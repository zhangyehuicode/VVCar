﻿using System;
using System.Collections.Generic;
using System.Linq;
using YEF.Core;
using YEF.Core.Data;
using YEF.Core.Domain;
using VVCar.BaseData.Domain.Entities;
using VVCar.BaseData.Domain.Services;

namespace VVCar.BaseData.Services.DomainServices
{
    public partial class RoleService : DomainServiceBase<IRepository<Role>, Role, Guid>, IRoleService
    {
        public RoleService()
        {
        }

        #region methods

        public override Role Add(Role entity)
        {
            if (entity == null)
                return null;
            entity.ID = Util.NewID();
            entity.CreatedUserID = AppContext.CurrentSession.UserID;
            entity.CreatedUser = AppContext.CurrentSession.UserName;
            entity.CreatedDate = DateTime.Now;
            return base.Add(entity);
        }

        public override bool Delete(Guid key)
        {
            var role = this.Repository.GetByKey(key);
            if (role == null)
                throw new DomainException("删除失败，数据不存在");

            if (role.ID == Guid.Parse("00000000-0000-0000-0000-000000000001"))
                throw new DomainException("不允许删除超级管理员角色");
            else if (role.ID == Guid.Parse("00000000-0000-0000-0000-000000000002"))
                throw new DomainException("不允许删除店长角色");
            else if (role.ID == Guid.Parse("00000000-0000-0000-0000-000000000003"))
                throw new DomainException("不允许删除店员角色");

            role.IsDeleted = true;
            return this.Repository.Update(role) > 0;
        }

        public override bool Update(Role entity)
        {
            if (entity == null)
                return false;
            var role = this.Repository.GetByKey(entity.ID);
            if (role == null)
                return false;

            if (role.ID == Guid.Parse("00000000-0000-0000-0000-000000000001"))
                throw new DomainException("不允许修改超级管理员角色");
            else if (role.ID == Guid.Parse("00000000-0000-0000-0000-000000000002"))
                throw new DomainException("不允许修改店长角色");
            else if (role.ID == Guid.Parse("00000000-0000-0000-0000-000000000003"))
                throw new DomainException("不允许修改店员角色");

            role.Code = entity.Code;
            role.Name = entity.Name;
            role.RoleType = entity.RoleType;
            role.LastUpdateUserID = AppContext.CurrentSession.UserID;
            role.LastUpdateUser = AppContext.CurrentSession.UserName;
            role.LastUpdateDate = DateTime.Now;
            return base.Update(role);
        }

        protected override bool DoValidate(Role entity)
        {
            bool exists = this.Repository.Exists(t => t.Code == entity.Code && t.ID != entity.ID);
            if (exists)
                throw new DomainException(String.Format("代码 {0} 已使用，不能重复添加。", entity.Code));
            exists = this.Repository.Exists(t => t.Name == entity.Name && t.ID != entity.ID);
            if (exists)
                throw new DomainException(String.Format("名称 {0} 已使用，不能重复添加。", entity.Name));
            return true;
        }

        #endregion

        #region IRoleService 成员

        public IEnumerable<Role> Query(Domain.Filters.RoleFilter filter)
        {
            var commendid = Guid.Parse("00000000-0000-0000-0000-000000000000");
            var queryable = this.Repository.GetQueryable(false).Where(t => !t.IsDeleted).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID || t.MerchantID == commendid);
            if (filter != null)
            {
                if (!string.IsNullOrEmpty(filter.Code))
                    queryable = queryable.Where(t => t.Code.Contains(filter.Code));
                if (!string.IsNullOrEmpty(filter.Name))
                    queryable = queryable.Where(t => t.Name.Contains(filter.Name));
                if (!string.IsNullOrEmpty(filter.RoleType))
                    queryable = queryable.Where(t => t.RoleType == filter.RoleType);
            }
            return queryable.ToArray();
        }

        #endregion
    }
}
