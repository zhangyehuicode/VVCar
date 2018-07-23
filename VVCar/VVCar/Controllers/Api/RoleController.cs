using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VVCar.BaseData.Domain.Entities;
using VVCar.BaseData.Domain.Filters;
using VVCar.BaseData.Domain.Services;
using YEF.Core.Dtos;

namespace VVCar.Controllers.Api
{
    /// <summary>
    /// 角色
    /// </summary>
    [RoutePrefix("api/Role")]
    public class RoleController : BaseApiController
    {
        #region ctor.

        /// <summary>
        /// 角色
        /// </summary>
        /// <param name="roleService"></param>
        public RoleController(IRoleService roleService)
        {
            this.RoleService = roleService;
        }

        #endregion

        #region properties
        IRoleService RoleService { get; set; }
        #endregion

        /// <summary>
        /// 新增角色
        /// </summary>
        /// <param name="newRole">角色</param>
        /// <returns></returns>
        [HttpPost]
        public JsonActionResult<Role> AddRole(Role newRole)
        {
            return SafeExecute(() =>
            {
                return this.RoleService.Add(newRole);
            });
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="id">角色ID</param>
        /// <returns></returns>
        [HttpDelete]
        public JsonActionResult<bool> DeleteRole(Guid id)
        {
            return SafeExecute(() =>
            {
                return this.RoleService.Delete(id);
            });
        }

        /// <summary>
        /// 更新角色
        /// </summary>
        /// <param name="role">角色</param>
        /// <returns></returns>
        [HttpPut]
        public JsonActionResult<bool> UpdateRole(Role role)
        {
            return SafeExecute(() =>
            {
                return this.RoleService.Update(role);
            });
        }

        /// <summary>
        /// 获取角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonActionResult<Role> GetRole(Guid id)
        {
            return SafeExecute(() =>
            {
                return this.RoleService.Get(id);
            });
        }

        /// <summary>
        /// 查询角色
        /// </summary>
        /// <returns></returns>
        [HttpGet, AllowAnonymous]
        public JsonActionResult<IEnumerable<Role>> Search([FromUri]RoleFilter filter)
        {
            return SafeExecute(() =>
            {
                if (!ModelState.IsValid)
                    return null;
                return this.RoleService.Query(filter);
            });
        }
    }
}
