using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YEF.Core;

namespace VVCar.Areas.Mobile.Controllers
{
    public class AgentController : Controller
    {
        /// <summary>
        /// Auth
        /// </summary>
        /// <returns></returns>
        public ActionResult Auth()
        {
            var openId = Request["openid"];
            var nickname = Request["nickname"];
            var headimgurl = Request["headimgurl"];
            var companyCode = Request["mch"];
            var redirectTo = Request["redirectTo"];
            if (string.IsNullOrEmpty(companyCode) || string.IsNullOrEmpty(redirectTo))
                return Content("参数错误");
            if (string.IsNullOrEmpty(openId))//|| string.IsNullOrEmpty(nickname) || string.IsNullOrEmpty(headimgurl)
            {
                var serviceUrl = string.Empty;
                var useragent = Request.UserAgent;
                if (useragent.ToLower().Contains("micromessenger"))
                {
                    serviceUrl = AppContext.Settings.WeChatIntegrationService;
                }
                else
                {
                    serviceUrl = AppContext.Settings.AlipayIntegrationService;
                }
                var redirectUrl = Server.UrlEncode(Request.Url.AbsoluteUri);
                var authUrl = $"{serviceUrl}/Auth?companyCode={companyCode}&UserInfo=true&scope=snsapi_userinfo&redirectUrl={redirectUrl}";
                return Redirect(authUrl);
            }
            var userOpenId = openId.Split(',')[0];
            TempData["openid"] = userOpenId;
            TempData["nickname"] = nickname;
            TempData["headimgurl"] = headimgurl;
            return Redirect(redirectTo);
        }

        /// <summary>
        /// BaseAuth
        /// </summary>
        /// <returns></returns>
        public ActionResult BaseAuth()
        {
            var openId = Request["openid"];
            var companyCode = Request["mch"];
            var redirectTo = Request["redirectTo"];
            if (string.IsNullOrEmpty(redirectTo))
                return Content("参数错误");
            if (string.IsNullOrEmpty(openId))
            {
                var serviceUrl = string.Empty;
                var useragent = Request.UserAgent;
                if (useragent.ToLower().Contains("micromessenger"))
                {
                    serviceUrl = AppContext.Settings.WeChatIntegrationService;
                }
                else
                {
                    serviceUrl = AppContext.Settings.AlipayIntegrationService;
                }
                var redirectUrl = Server.UrlEncode(Request.Url.AbsoluteUri);
                var authUrl = $"{serviceUrl}/Auth?companyCode={companyCode}&scope=snsapi_base&redirectUrl={redirectUrl}";
                return Redirect(authUrl);
            }
            var userOpenId = openId.Split(',')[0];
            TempData["openid"] = userOpenId;
            return Redirect(redirectTo);
        }

        // GET: Mobile/Agent

        /// <summary>
        /// 代理商登录
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var companyCode = Request["mch"];
            if (string.IsNullOrEmpty(companyCode))
            {
                return Content("参数错误");
            }
            var openId = TempData["openid"] as string;
#if DEBUG
            openId = "oI4ee0sGQu_E2tkp7OUdU2ADzR0U";
#endif
            if (string.IsNullOrEmpty(openId))
            {
                return RedirectToAction("Auth", new { redirectTo = Server.UrlEncode(Request.Url.AbsoluteUri), mch = companyCode });
            }
            ViewBag.OpenId = openId;
            ViewBag.NickName = TempData["nickname"] as string;
            ViewBag.HeadImgUrl = TempData["headimgurl"] as string;
            ViewBag.ClientType = Request.UserAgent.ToLower().Contains("micromessenger") ? 2 : 3;
            return View();
        }

        /// <summary>
        /// 销售经理登录
        /// </summary>
        /// <returns></returns>
        public ActionResult SalesManagerIndex()
        {
            var companyCode = Request["mch"];
            if (string.IsNullOrEmpty(companyCode))
            {
                return Content("参数错误");
            }
            var openId = TempData["openid"] as string;
#if DEBUG
            openId = "oI4ee0sGQu_E2tkp7OUdU2ADzR0U";
#endif
            if (string.IsNullOrEmpty(openId))
            {
                return RedirectToAction("Auth", new { redirectTo = Server.UrlEncode(Request.Url.AbsoluteUri), mch = companyCode });
            }
            ViewBag.OpenId = openId;
            ViewBag.NickName = TempData["nickname"] as string;
            ViewBag.HeadImgUrl = TempData["headimgurl"] as string;
            ViewBag.ClientType = Request.UserAgent.ToLower().Contains("micromessenger") ? 2 : 3;
            return View();
        }

        /// <summary>
        /// 代理商首页
        /// </summary>
        /// <returns></returns>
        public ActionResult GeneralManagerHome()
        {
            var companyCode = Request["mch"];
            if (string.IsNullOrEmpty(companyCode))
            {
                return Content("参数错误");
            }
            var openId = TempData["openid"] as string;
#if DEBUG
            openId = "oI4ee0sGQu_E2tkp7OUdU2ADzR0U";
#endif
            if (string.IsNullOrEmpty(openId))
            {
                return RedirectToAction("Auth", new { redirectTo = Server.UrlEncode(Request.Url.AbsoluteUri), mch = companyCode });
            }
            ViewBag.OpenId = openId;
            ViewBag.NickName = TempData["nickname"] as string;
            ViewBag.HeadImgUrl = TempData["headimgurl"] as string;
            ViewBag.ClientType = Request.UserAgent.ToLower().Contains("micromessenger") ? 2 : 3;
            return View();
        }

        /// <summary>
        /// 快捷开户
        /// </summary>
        /// <returns></returns>
        public ActionResult QuickOpenAccount()
        {
            var companyCode = Request["mch"];
            if (string.IsNullOrEmpty(companyCode))
            {
                return Content("参数错误");
            }
            var openId = TempData["openid"] as string;
#if DEBUG
            openId = "oI4ee0sGQu_E2tkp7OUdU2ADzR0U";
#endif
            if (string.IsNullOrEmpty(openId))
            {
                return RedirectToAction("Auth", new { redirectTo = Server.UrlEncode(Request.Url.AbsoluteUri), mch = companyCode });
            }
            ViewBag.OpenId = openId;
            ViewBag.NickName = TempData["nickname"] as string;
            ViewBag.HeadImgUrl = TempData["headimgurl"] as string;
            ViewBag.ClientType = Request.UserAgent.ToLower().Contains("micromessenger") ? 2 : 3;
            return View();
        }

        /// <summary>
        /// 库存管理
        /// </summary>
        /// <returns></returns>
        public ActionResult StockManager()
        {
            var companyCode = Request["mch"];
            if (string.IsNullOrEmpty(companyCode))
            {
                return Content("参数错误");
            }
            var openId = TempData["openid"] as string;
#if DEBUG
            openId = "oI4ee0sGQu_E2tkp7OUdU2ADzR0U";
#endif
            if (string.IsNullOrEmpty(openId))
            {
                return RedirectToAction("Auth", new { redirectTo = Server.UrlEncode(Request.Url.AbsoluteUri), mch = companyCode });
            }
            ViewBag.OpenId = openId;
            ViewBag.NickName = TempData["nickname"] as string;
            ViewBag.HeadImgUrl = TempData["headimgurl"] as string;
            ViewBag.ClientType = Request.UserAgent.ToLower().Contains("micromessenger") ? 2 : 3;
            return View();
        }

        /// <summary>
        /// 今日开户
        /// </summary>
        /// <returns></returns>
        public ActionResult TodayOpenAccount()
        {
            var companyCode = Request["mch"];
            if (string.IsNullOrEmpty(companyCode))
            {
                return Content("参数错误");
            }
            var openId = TempData["openid"] as string;
#if DEBUG
            openId = "oI4ee0sGQu_E2tkp7OUdU2ADzR0U";
#endif
            if (string.IsNullOrEmpty(openId))
            {
                return RedirectToAction("Auth", new { redirectTo = Server.UrlEncode(Request.Url.AbsoluteUri), mch = companyCode });
            }
            ViewBag.OpenId = openId;
            ViewBag.NickName = TempData["nickname"] as string;
            ViewBag.HeadImgUrl = TempData["headimgurl"] as string;
            ViewBag.ClientType = Request.UserAgent.ToLower().Contains("micromessenger") ? 2 : 3;
            return View();
        }
    }
}