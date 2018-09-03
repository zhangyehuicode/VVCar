using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VVCar.BaseData.Domain.Dtos;
using VVCar.BaseData.Domain.Entities;
using VVCar.BaseData.Domain.Filters;
using VVCar.BaseData.Domain.Services;
using YEF.Core.Dtos;

namespace VVCar.Controllers.Api
{
    /// <summary>
    /// 系统设置
    /// </summary>
    [RoutePrefix("api/SystemSetting")]
    public class SystemSettingController : BaseApiController
    {
        #region ctor.

        /// <summary>
        /// 系统设置
        /// </summary>
        /// <param name="sysSettingService"></param>
        public SystemSettingController(ISystemSettingService sysSettingService)
        {
            this.SysSettingService = sysSettingService;
        }

        #endregion

        #region properties
        ISystemSettingService SysSettingService { get; set; }
        #endregion

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonActionResult<SystemSetting> Add(SystemSetting entity)
        {
            return SafeExecute(() =>
            {
                return SysSettingService.Add(entity);
            });
        }

        /// <summary>
        /// 修改设置值
        /// </summary>
        /// <param name="setting">系统设置</param>
        /// <returns></returns>
        [HttpPut]
        public JsonActionResult<bool> Update(SystemSetting setting)
        {
            return SafeExecute(() =>
            {

                return SysSettingService.UpdateSetting(setting.ID, setting.Name, setting.SettingValue);
            });
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public JsonActionResult<bool> Delete(Guid id)
        {
            return SafeExecute(() =>
            {
                return SysSettingService.Delete(id);
            });
        }

        /// <summary>
        /// 获取系统设置
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public PagedActionResult<SystemSettingDto> GetSystemSettings([FromUri]SystemSettingFilter filter)
        {
            return SafeGetPagedData<SystemSettingDto>((result) =>
            {
                var totalCount = 0;
                var data = SysSettingService.Search(filter, out totalCount);
                result.Data = data;
                result.TotalCount = totalCount;
            });
        }

        /// <summary>
        /// 获取系统信息
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetSystemInfo")]
        [AllowAnonymous]
        public JsonActionResult<SystemInfoDto> GetSystemInfo()
        {
            return SafeExecute(() =>
            {
                return new SystemInfoDto
                {
                    DepartmentCode = YEF.Core.AppContext.DepartmentCode,
                    DepartmentName = YEF.Core.AppContext.DepartmentName,
                    CompanyCode = YEF.Core.AppContext.CurrentSession.CompanyCode,
                    SiteDomain = YEF.Core.AppContext.Settings.SiteDomain
                };
            });
        }
    }
}
