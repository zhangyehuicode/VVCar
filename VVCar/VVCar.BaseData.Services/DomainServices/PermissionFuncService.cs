using System;
using System.Collections.Generic;
using System.Linq;
using YEF.Core;
using YEF.Core.Data;
using YEF.Core.Domain;
using VVCar.BaseData.Domain.Entities;
using VVCar.BaseData.Domain.Services;
using VVCar.BaseData.Domain.Dtos;
using VVCar.BaseData.Domain.Filters;
using VVCar.BaseData.Domain.Enums;

namespace VVCar.BaseData.Services.DomainServices
{
    /// <summary>
    /// 功能权限
    /// </summary>
    public partial class PermissionFuncService : DomainServiceBase<IRepository<PermissionFunc>, PermissionFunc, Guid>, IPermissionFuncService
    {
        public PermissionFuncService()
        {
        }

        #region properties

        private IRepository<RolePermission> _rolePermissionRepo;

        /// <summary>
        ///　角色权限关联Repo
        /// </summary>
        public IRepository<RolePermission> RolePermissionRepo
        {
            get
            {
                if (_rolePermissionRepo == null)
                {
                    _rolePermissionRepo = this.UnitOfWork.GetRepository<IRepository<RolePermission>>();
                }
                return _rolePermissionRepo;
            }
        }

        #endregion

        #region methods
        protected override bool DoValidate(PermissionFunc entity)
        {
            bool exists = this.Repository.Exists(t => t.Code == entity.Code && t.ID != entity.ID);
            if (exists)
                throw new DomainException(String.Format("代码 {0} 已使用，不能重复添加。", entity.Code));
            return true;
        }

        public override PermissionFunc Add(PermissionFunc entity)
        {
            if (entity == null)
                return null;
            entity.ID = Util.NewID();
            entity.IsManual = true;
            return base.Add(entity);
        }

        public override bool Update(PermissionFunc entity)
        {
            if (entity == null)
                return false;
            var permission = this.Repository.GetByKey(entity.ID);
            if (permission == null)
                throw new DomainException("数据不存在");
            permission.Name = entity.Name;
            permission.PermissionType = entity.PermissionType;
            permission.IsAvailable = entity.IsAvailable;
            return base.Update(permission);
        }

        public override bool Delete(Guid key)
        {
            var entity = this.Repository.GetByKey(key);
            if (entity == null)
                throw new DomainException("删除失败，数据不存在");
            entity.IsDeleted = true;
            return this.Repository.Update(entity) > 0;
        }

        /// <summary>
        /// 获取可用的权限列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<PermissionFunc> GetFuncPermissionList()
        {
            return this.Repository.GetQueryable(false)
                .Where(p => !p.IsAvailable)
                .ToArray();
        }

        /// <summary>
        /// 权限列表--分页及查询条件
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public IEnumerable<PermissionFunc> SearchFuncPermission(PermissionFilter filter, out int totalCount)
        {
            totalCount = 0;
            IEnumerable<PermissionFunc> dataList = null;
            var queryable = this.Repository.GetQueryable(false);
            if (filter != null)
            {
                if (!string.IsNullOrEmpty(filter.Code))
                    queryable = queryable.Where(t => t.Code.Contains(filter.Code));
                if (!string.IsNullOrEmpty(filter.Name))
                    queryable = queryable.Where(t => t.Name.Contains(filter.Name));
            }
            if (filter != null && filter.Start.HasValue && filter.Limit.HasValue)
            {
                totalCount = queryable.Count();
                dataList = queryable.OrderBy(t => t.ID).Skip(filter.Start.Value).Take(filter.Limit.Value).ToArray();
            }
            else
            {
                dataList = queryable.ToArray();
                totalCount = dataList.Count();
            }
            return dataList;
        }

        /// <summary>
        /// 分配权限
        /// </summary>
        /// <param name="assignModel"></param>
        /// <returns></returns>
        public bool AssignPermission(AssignPermissionDto assignModel)
        {
            if (assignModel.IsChecked)
            {
                var exist = RolePermissionRepo.Exists(t => t.PermissionCode == assignModel.PermissionCode && t.RoleCode == assignModel.RoleCode
                && t.PermissionType == assignModel.PermissionType);
                if (!exist)//如果不存在则新增
                {
                    var rolePermission = new RolePermission
                    {
                        ID = Util.NewID(),
                        PermissionCode = assignModel.PermissionCode,
                        PermissionType = assignModel.PermissionType,
                        RoleCode = assignModel.RoleCode,
                    };
                    this.RolePermissionRepo.Add(rolePermission);
                }
            }
            else
            {
                var rolePermission = RolePermissionRepo.Get(t => t.PermissionCode == assignModel.PermissionCode && t.RoleCode == assignModel.RoleCode
                && t.PermissionType == assignModel.PermissionType);
                if (rolePermission != null)
                {
                    rolePermission.IsDeleted = true;
                    RolePermissionRepo.Update(rolePermission);
                }
            }
            return true;
        }

        /// <summary>
        /// 获取角色拥有的功能权限
        /// </summary>
        /// <param name="roleCode"></param>
        /// <returns></returns>
        public IEnumerable<OwnerPermissionDto> GetOwnerPermissionList(string roleCode)
        {
            var rolePermissionList = RolePermissionRepo.GetQueryable(false)
                .Where(d => d.RoleCode == roleCode)
                .Select(t => t.PermissionCode)
                .ToList();

            var allPermissoin = this.Repository.GetQueryable(false).ToList();
            var ownerPermissionList = allPermissoin.GroupBy(p => p.PermissionType)
                .Select(group => new OwnerPermissionDto
                {
                    PermissionType = group.Key.GetDescription(),
                    ItemList = group.Select(item => new OwnerPermissionItemDto()
                    {
                        PermissionCode = item.Code,
                        PermissionName = item.Name,
                        PermissionType = item.PermissionType,
                        IsChecked = rolePermissionList.Contains(item.Code)
                    })
                }).ToList();
            return ownerPermissionList;
        }

        public IEnumerable<AssignPermissionDto> GetRolePermissionList(IList<string> roleCodes)
        {
            var rolePermissionList = RolePermissionRepo.GetQueryable(false)
                .Where(d => roleCodes.Contains(d.RoleCode))
                .Select(t => new AssignPermissionDto()
                {
                    RoleCode = t.RoleCode,
                    PermissionCode = t.PermissionCode,
                    PermissionType = t.PermissionType,
                    IsChecked = true
                })
                .ToList();
            return rolePermissionList;
        }

        /// <summary>
        /// 同步权限列表
        /// </summary>
        /// <returns></returns>
        public bool SyncPermission()
        {
            var funcPermissionData = this.Query(null).ToList();
            var tempList = new List<string>();

            #region 同步管理后台菜单数据
            var sysMenuRepo = this.UnitOfWork.GetRepository<IRepository<SysMenu>>();
            var sysMenuList = sysMenuRepo.GetQueryable(false)
                .Where(t => t.IsAvailable == true && t.IsLeaf == true)
                .Select(t => new { Code = t.Component + "." + t.SysMenuUrl, Name = t.Name }).ToArray();
            foreach (var sysMenu in sysMenuList)
            {
                AddOrUpdateSyncPermission(funcPermissionData, sysMenu.Code, sysMenu.Name, EPermissionType.PortalMenu);
                tempList.Add(sysMenu.Code);
            }
            #endregion

            var notUse = funcPermissionData.Where(d => !tempList.Contains(d.Code) && d.IsManual == false).ToList();
            foreach (var nu in notUse)
            {
                this.RolePermissionRepo.Delete(d => d.PermissionCode == nu.Code);
                this.Repository.Delete(nu);
            }
            return true;
        }

        /// <summary>
        /// 新增或更新同步的权限项
        /// </summary>
        /// <param name="funcPermissionData"></param>
        /// <param name="code">权限编码</param>
        /// <param name="name">权限名称</param>
        /// <param name="type">权限类型</param>
        void AddOrUpdateSyncPermission(List<PermissionFunc> funcPermissionData, string code, string name, EPermissionType type)
        {
            var tempPremission = funcPermissionData.FirstOrDefault(d => d.PermissionType == type && d.Code == code);
            if (tempPremission != null)
            {
                if (tempPremission.Name.Equals(name))
                    return;
                tempPremission.Name = name;
                this.Repository.Update(tempPremission);
            }
            else
            {
                tempPremission = new PermissionFunc();
                tempPremission.ID = Util.NewID();
                tempPremission.Code = code;
                tempPremission.Name = name;
                tempPremission.PermissionType = type;
                tempPremission.IsAvailable = true;
                this.Repository.Add(tempPremission);
            }
        }

        #endregion
    }
}
