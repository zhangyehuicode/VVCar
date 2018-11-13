using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VVCar.BaseData.Domain.Filters;
using VVCar.BaseData.Domain.Services;
using YEF.Core.Dtos;

namespace VVCar.Controllers.Api
{
    /// <summary>
    /// 微信入口
    /// </summary>
    [ApiAuthorize(NeedLogin = false)]
    [RoutePrefix("api/Wechat")]
    public class WechatController : BaseApiController
    {
        public WechatController(IWechatService wechatService)
        {
            WechatService = wechatService;
        }

        IWechatService WechatService { get; set; }

        /// <summary>
        /// 入口
        /// </summary>
        /// <returns></returns>
        [HttpGet, AllowAnonymous]
        public JsonActionResult<string> Index([FromUri]WechatFilter filter)
        {
            return SafeExecute(() =>
            {
                return WechatService.Index(filter);
            });
        }
    }
}
