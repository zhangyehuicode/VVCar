using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
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
        public WechatController()
        {
        }

        /// <summary>
        /// 入口
        /// </summary>
        /// <returns></returns>
        public JsonActionResult<string> Index()
        {
            return SafeExecute(() =>
            {
                return "hello";
            });
        }
    }
}
