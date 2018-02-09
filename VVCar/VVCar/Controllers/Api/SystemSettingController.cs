using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VVCar.BaseData.Domain.Dtos;
using VVCar.BaseData.Domain.Entities;
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
        /// 修改设置值
        /// </summary>
        /// <param name="setting">系统设置</param>
        /// <returns></returns>
        [HttpPut]
        public JsonActionResult<bool> Update(SystemSetting setting)
        {
            return SafeExecute(() =>
            {

                return SysSettingService.UpdateSetting(setting.ID, setting.SettingValue);
            });
        }

        /// <summary>
        /// 获取系统设置
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonActionResult<IEnumerable<SystemSetting>> GetSystemSettings()
        {
            return SafeExecute(() =>
            {
                return SysSettingService.Search(null);
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
