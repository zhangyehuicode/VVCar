﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YEF.Core;

namespace VVCar.Areas.Mobile.Controllers
{
    /// <summary>
    /// 车比特
    /// </summary>
    public class CarBitcoinController : Controller
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

        // GET: Mobile/CarBitcoin
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
        /// 我的信息
        /// </summary>
        /// <returns></returns>
        public ActionResult MyInfo()
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
        /// 注册
        /// </summary>
        /// <returns></returns>
        public ActionResult Register()
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
        /// 商城
        /// </summary>
        /// <returns></returns>
        public ActionResult Shop()
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
        /// 商品详情
        /// </summary>
        /// <returns></returns>
        public ActionResult ProductDetails()
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
        /// 确认订单
        /// </summary>
        /// <returns></returns>
        public ActionResult ConfirmOrder()
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
        /// 支付结果
        /// </summary>
        /// <returns></returns>
        public ActionResult PayResult()
        {
            return View();
        }

        /// <summary>
        /// 我的订单
        /// </summary>
        /// <returns></returns>
        public ActionResult MyOrder()
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
        /// 订单详情
        /// </summary>
        /// <returns></returns>
        public ActionResult OrderDetails()
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
        /// 我的引擎
        /// </summary>
        /// <returns></returns>
        public ActionResult MyEngine()
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
        /// 我的资产
        /// </summary>
        /// <returns></returns>
        public ActionResult MyProperty()
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