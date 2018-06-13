using System;
using System.Collections.Generic;
using System.Linq;
using YEF.Core;
using YEF.Core.Data;
using YEF.Core.Domain;
using VVCar.BaseData.Domain.Entities;
using VVCar.BaseData.Domain.Filters;
using VVCar.BaseData.Domain.Services;
using VVCar.BaseData.Domain.Dtos;
using VVCar.BaseData.Domain.Enums;

namespace VVCar.BaseData.Services.DomainServices
{
    public partial class SysMenuService : DomainServiceBase<IRepository<SysMenu>, SysMenu, Guid>, ISysMenuService
    {
        public SysMenuService()
        {
        }

        #region properties
        IUserService UserService
        {
            get
            {
                return ServiceLocator.Instance.GetService<IUserService>();
            }
        }
        #endregion

        #region methods
        public override SysMenu Add(SysMenu entity)
        {
            entity.ID = Util.NewID();
            if (entity.ParentID == Guid.Empty)
                entity.ParentID = null;

            return base.Add(entity);
        }

        public override bool Update(SysMenu entity)
        {
            entity.Children = null;
            if (entity.ParentID == Guid.Empty)
                entity.ParentID = null;

            return base.Update(entity);
        }

        public IEnumerable<SysMenu> Query(SysMenuFilter filter, out int totalCount)
        {
            if (filter.ParentID == Guid.Empty)
                filter.ParentID = null;

            var queryable = Repository.GetInclude(p => p.Children).Where(t => !t.IsDeleted);
            if (filter.All)
            {
                var result = queryable.OrderBy(p => p.Index).ToArray();
                totalCount = result.Count();
                return result;
            }

            queryable = queryable.Where(p => p.ParentID == filter.ParentID);

            totalCount = queryable.Count();
            return queryable.OrderBy(p => p.Index).ToArray();
        }

        public override bool Delete(Guid key)
        {
            var entity = Repository.Get(p => p.ID == key);
            if (entity == null)
                throw new DomainException("数据不存或已删除");
            entity.IsDeleted = true;
            return base.Update(entity);
        }

        /// <summary>
        /// 获取管理后台导航菜单
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SysNavMenuDto> GetNavMenu()
        {
            var sysMenus = this.Repository.GetQueryable(false)
                .Where(t => t.IsAvailable == true)
                .OrderBy(p => p.Index).ToList();
            var isAdminUser = UserService.IsAdmin(AppContext.CurrentSession.UserID);
            if (!isAdminUser)//判断是不是管理员
            {
                var userFuncPermissions = UserService.GetUserFuncPermissionList(AppContext.CurrentSession.UserID);
                var sysMenuPermissions = new List<string>();
                if (userFuncPermissions != null && userFuncPermissions.Count() > 0)
                {
                    sysMenuPermissions = userFuncPermissions.Where(p => p.PermissionType == EPermissionType.PortalMenu)
                        .Select(t => t.PermissionCode).ToList();
                }
                sysMenus = sysMenus.Where(t => t.IsLeaf == false || sysMenuPermissions.Contains(t.Component + "." + t.SysMenuUrl)).ToList();
            }
            var sysNavMenus = sysMenus.MapTo<IList<SysNavMenuDto>>();
            var navMenus = BuildTree(sysNavMenus, null);
            navMenus = navMenus.Where(nav => nav.Children.Count() > 0).ToList();
            navMenus.ForEach(t =>
            {
                t.expanded = false;
            });
            return navMenus;
        }

        /// <summary>
        /// 生成树
        /// </summary>
        /// <param name="sources"></param>
        /// <param name="parentID"></param>
        /// <returns></returns>
        IEnumerable<SysNavMenuDto> BuildTree(IList<SysNavMenuDto> sources, Guid? parentID)
        {
            var children = sources.Where(t => t.ParentID == parentID).OrderBy(t => t.Index).ToList();
            foreach (var child in children)
            {
                child.Children = BuildTree(sources, child.ID);
            }
            return children;
        }

        #endregion
    }
}
