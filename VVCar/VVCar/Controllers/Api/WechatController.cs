using Senparc.Weixin.MP;
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
    public class WechatController : System.Web.Mvc.Controller
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
        public System.Web.Mvc.ActionResult Index([FromUri]WechatFilter filter)
        {
            filter.token = "YBCYZ2018";
            if (CheckSignature.Check(filter.signature, filter.timestamp, filter.nonce, filter.token))
            {
                return Content(filter.echostr);
            }
            else
            {
                return Content("failed:" + filter.signature + "," + CheckSignature.GetSignature(filter.timestamp, filter.nonce, filter.token));
            }
        }
    }
}
