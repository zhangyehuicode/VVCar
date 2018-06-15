using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using VVCar.BaseData.Domain.Dtos;
using VVCar.BaseData.Domain.Entities;
using VVCar.BaseData.Domain.Filters;
using VVCar.BaseData.Domain.Services;
using YEF.Core.Dtos;

namespace VVCar.Controllers.Api
{
    [RoutePrefix("api/SysMenu")]
    public class SysMenuController : BaseApiController
    {
        public SysMenuController(ISysMenuService menuService)
        {
            MenuService = menuService;
        }
        ISysMenuService MenuService { get; set; }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public JsonActionResult<SysMenu> Get(Guid id)
        {
            return SafeExecute(() => { return MenuService.Get(id); });
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public JsonActionResult<SysMenu> Add(SysMenu entity)
        {
            return SafeExecute(() => { return MenuService.Add(entity); });
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public JsonActionResult<IEnumerable<SysMenu>> Query([FromUri]SysMenuFilter filter)
        {
            return SafeExecute(() =>
            {
                int totalCount;
                return MenuService.Query(filter, out totalCount);
            });
        }

        /// <summary>
        /// 根据父级ID获取子节点
        /// </summary>
        /// <param name="parentID"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("GetByParentID/{parentID}")]
        public JsonActionResult<IEnumerable<SysMenu>> GetByParentID(Guid? parentID)
        {
            var filter = new SysMenuFilter();
            filter.ParentID = parentID;
            return Query(filter);
        }

        /// <summary>
        /// 根据父级ID获取导航菜单
        /// </summary>
        /// <param name="parentID"></param>
        /// <returns></returns>
        [HttpGet, Route("NavMenu/{parentID}")]
        public TreeActionResult<SysNavMenuDto> GetNavMenuByParentID(Guid? parentID)
        {
            return SafeGetTreeData(() =>
            {
                if (parentID.HasValue && parentID != Guid.Empty)
                    return null;
                var navMenus = MenuService.GetNavMenu();
                navMenus = navMenus.Where(nav => nav.Children.Count() > 0).ToList();
                return navMenus;
            });
        }

        /// <summary>
        /// 根据父级ID获取导航菜单（菜单管理）
        /// </summary>
        /// <param name="parentID"></param>
        /// <returns></returns>
        [HttpGet, Route("NavManageMenu/{parentID}")]
        public TreeActionResult<SysNavMenuDto> GetNavManageMenu(Guid? parentID)
        {
            return SafeGetTreeData(() =>
            {
                if (parentID.HasValue && parentID != Guid.Empty)
                    return null;
                var item = new SysNavMenuDto
                {
                    ID = Guid.Parse("00000000-0000-0000-0000-000000000000"),
                    text = "菜单目录",
                    Children = MenuService.GetNavMenu(),
                };
                return new List<SysNavMenuDto>() { item };
            });
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPut]
        public JsonActionResult<bool> Update(SysMenu entity)
        {
            return SafeExecute<bool>(() => { return MenuService.Update(entity); });
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [System.Web.Http.HttpDelete]
        public JsonActionResult<bool> Delete(Guid id)
        {
            return SafeExecute<bool>(() => { return MenuService.Delete(id); });
        }

    }
}
