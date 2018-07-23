using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VVCar.BaseData.Domain.Entities;
using VVCar.BaseData.Domain.Filters;
using VVCar.BaseData.Domain.Services;
using YEF.Core;
using YEF.Core.Dtos;

namespace VVCar.Controllers.Api
{
    /// <summary>
    /// 用户角色关联
    /// </summary>
    [RoutePrefix("api/UserRole")]
    public class UserRoleController : BaseApiController
    {
        #region ctor.

        /// <summary>
        /// 用户角色
        /// </summary>
        /// <param name="userRoleService"></param>
        public UserRoleController(IUserRoleService userRoleService)
        {
            this.UserRoleService = userRoleService;
        }

        #endregion

        #region properties
        IUserRoleService UserRoleService { get; set; }
        #endregion

        /// <summary>
        /// 新增用户角色关联
        /// </summary>
        /// <param name="userRoles">批量操作对象</param>
        /// <returns></returns>
        [HttpPost, Route("BatchAdd")]
        public JsonActionResult<bool> BatchAdd(IEnumerable<UserRole> userRoles)
        {
            return SafeExecute(() =>
            {
                if (userRoles == null)
                {
                    throw new DomainException("参数错误");
                }
                return UserRoleService.BatchAdd(userRoles);
            });
        }

        /// <summary>
        /// 删除用户角色关联
        /// </summary>
        /// <param name="userRoleIds">批量操作对象</param>
        /// <returns></returns>
        [HttpDelete, Route("BatchDelete")]
        public JsonActionResult<bool> BatchDelete(IEnumerable<Guid> userRoleIds)
        {
            return SafeExecute(() =>
            {
                if (userRoleIds == null)
                {
                    throw new DomainException("参数错误");
                }
                return UserRoleService.BatchDelete(userRoleIds);
            });
        }

        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="filter">过滤条件</param>
        /// <returns></returns>
        [HttpGet, AllowAnonymous]
        public PagedActionResult<UserRole> GetDataByRoleID([FromUri]UserRoleFilter filter)
        {
            return SafeGetPagedData<UserRole>((result) =>
            {
                if (!ModelState.IsValid)
                {
                    throw new DomainException("参数错误");
                }
                var pagedData = this.UserRoleService.QueryData(filter);
                result.Data = pagedData.Items;
                result.TotalCount = pagedData.TotalCount;
            });
        }
    }
}
