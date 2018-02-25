using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VVCar.Models;
using YEF.Core;
using YEF.Core.Dtos;

namespace VVCar.Controllers.Api
{
    /// <summary>
    /// 通用功能接口
    /// </summary>
    [ApiAuthorize(NeedLogin = false)]
    [RoutePrefix("api/Common")]
    public class CommonController : BaseApiController
    {
        /// <summary>
        /// 通用功能接口
        /// </summary>
        public CommonController()
        {
        }

        /// <summary>
        /// 获取配置信息
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("ConfigInfo")]
        public JsonActionResult<ConfigInfoModel> GetConfigInfo()
        {
            return SafeExecute(() =>
            {
                return new ConfigInfoModel
                {
                    SiteDomain = AppContext.Settings.SiteDomain,
                    CompanyCode = AppContext.CurrentSession.CompanyCode
                };
            });
        }
    }
}
