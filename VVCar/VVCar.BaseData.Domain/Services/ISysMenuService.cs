using System;
using System.Collections.Generic;
using YEF.Core.Data;
using YEF.Core.Domain;
using VVCar.BaseData.Domain.Entities;
using VVCar.BaseData.Domain.Filters;
using VVCar.BaseData.Domain.Dtos;

namespace VVCar.BaseData.Domain.Services
{
    public partial interface ISysMenuService : IDomainService<IRepository<SysMenu>, SysMenu, Guid>
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        IEnumerable<SysMenu> Query(SysMenuFilter filter, out int totalCount);

        /// <summary>
        /// 获取管理后台导航菜单
        /// </summary>
        /// <returns></returns>
        IEnumerable<SysNavMenuDto> GetNavMenu();
    }
}
