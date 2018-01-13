using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.WebPages.Html;
using Microsoft.Ajax.Utilities;
using VVCar.BaseData.Domain.Dtos;
using VVCar.BaseData.Domain.Entities;
using VVCar.BaseData.Domain.Enums;
using VVCar.BaseData.Domain.Filters;
using VVCar.BaseData.Domain.Services;
using YEF.Core;
using YEF.Core.Dtos;

namespace VVCar.Controllers.Api
{
    /// <summary>
    /// 权限
    /// </summary>
    [RoutePrefix("api/Permission")]
    public class PermissionController : BaseApiController
    {
        /// <summary>
        /// 权限
        /// </summary>
        /// <param name="permissionsFuncService"></param>
        public PermissionController(IPermissionFuncService permissionsFuncService)
        {
            PermissionFuncService = permissionsFuncService;
        }

        #region properties

        /// <summary>
        /// 功能权限领域服务
        /// </summary>
        IPermissionFuncService PermissionFuncService { get; set; }

        #endregion

        /// <summary>
        /// 新增功能权限
        /// </summary>
        /// <param name="newPermission">功能权限</param>
        /// <returns></returns>
        [HttpPost]
        public JsonActionResult<PermissionFunc> AddDepartment(PermissionFunc newPermission)
        {
            return SafeExecute(() =>
            {
                return this.PermissionFuncService.Add(newPermission);
            });
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="permission"></param>
        /// <returns></returns>
        [HttpPut]
        public JsonActionResult<bool> Update(PermissionFunc permission)
        {
            return SafeExecute(() => PermissionFuncService.Update(permission));
        }

        /// <summary>
        /// 获取权限列表
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetRolePermissionList/{roleCode}")]
        public JsonActionResult<IEnumerable<OwnerPermissionDto>> GetRolePermissionList(string roleCode)
        {
            return SafeExecute(() =>
            {
                return PermissionFuncService.GetOwnerPermissionList(roleCode);
            });
        }

        /// <summary>
        /// 权限配置
        /// </summary>
        /// <param name="assignModel"></param>
        /// <returns></returns>
        [HttpPost, Route("AssignPermission")]
        public JsonActionResult<bool> AssignPermission(AssignPermissionDto assignModel)
        {
            return SafeExecute(() => PermissionFuncService.AssignPermission(assignModel));
        }

        /// <summary>
        /// 拉取权限
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("SyncPermission")]
        public JsonActionResult<bool> SyncPermission()
        {
            return SafeExecute(() => PermissionFuncService.SyncPermission());
        }

        /// <summary>
        /// 查询权限
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        public PagedActionResult<PermissionFunc> SearchFuncPermission([FromUri]PermissionFilter filter)
        {
            return SafeGetPagedData<PermissionFunc>((result) =>
            {
                int totalCount = 0;
                var temp = PermissionFuncService.SearchFuncPermission(filter, out totalCount);
                result.TotalCount = totalCount;
                result.Data = temp;
            });
        }
    }
}
