using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YEF.Core;

namespace VVCar.Areas.Mobile.Controllers
{
    /// <summary>
    /// 商家端
    /// </summary>
    public class MerchantController : Controller
    {
        // GET: Mobile/Merchant

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

        /// <summary>
        /// 店长登录注册
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var companyCode = Request["mch"];
            //var departmentCode = Request["dept"];
            if (string.IsNullOrEmpty(companyCode))//|| string.IsNullOrEmpty(departmentCode) || string.IsNullOrEmpty(boardCode)
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
        /// 员工登录注册
        /// </summary>
        /// <returns></returns>
        public ActionResult StaffIndex()
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
        /// 店长主页
        /// </summary>
        /// <returns></returns>
        public ActionResult MasterHome()
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
        /// 员工主页
        /// </summary>
        /// <returns></returns>
        public ActionResult StaffHome()
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
        /// 快捷开单
        /// </summary>
        /// <returns></returns>
        public ActionResult QuickOrder()
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
        /// 开单（接车单）
        /// </summary>
        /// <returns></returns>
        public ActionResult OpenOrder()
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
        /// 订单
        /// </summary>
        /// <returns></returns>
        public ActionResult Order()
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
        /// 接车单详情
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
        /// 支付方式
        /// </summary>
        /// <returns></returns>
        public ActionResult PayType()
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
        /// 扫码收款
        /// </summary>
        /// <returns></returns>
        public ActionResult QRCodePay()
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
        /// 现金收款
        /// </summary>
        /// <returns></returns>
        public ActionResult CashPay()
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

        public ActionResult MyAppointment()
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
        /// 寻客侠
        /// </summary>
        /// <returns></returns>
        public ActionResult Advisement()
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
        /// 寻客侠详情页
        /// </summary>
        /// <returns></returns>
        public ActionResult AdvisementDetail()
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
        /// 数据分析导航
        /// </summary>
        /// <returns></returns>
        public ActionResult AnalyseIndex()
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
            ViewBag.OpenId = openId;
            ViewBag.NickName = TempData["nickname"] as string;
            ViewBag.HeadImgUrl = TempData["headimgurl"] as string;
            ViewBag.ClientType = Request.UserAgent.ToLower().Contains("micromessenger") ? 2 : 3;
            return View();
        }

        /// <summary>
        /// 数据分析
        /// </summary>
        /// <returns></returns>
        public ActionResult Analyse()
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
            ViewBag.OpenId = openId;
            ViewBag.NickName = TempData["nickname"] as string;
            ViewBag.HeadImgUrl = TempData["headimgurl"] as string;
            ViewBag.ClientType = Request.UserAgent.ToLower().Contains("micromessenger") ? 2 : 3;
            return View();
        }

        /// <summary>
        /// 数据分析子项
        /// </summary>
        /// <returns></returns>
        public ActionResult AnalyseDetail()
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
            ViewBag.OpenId = openId;
            ViewBag.NickName = TempData["nickname"] as string;
            ViewBag.HeadImgUrl = TempData["headimgurl"] as string;
            ViewBag.ClientType = Request.UserAgent.ToLower().Contains("micromessenger") ? 2 : 3;
            return View();

        }

        /// <summary>
        /// 扫码优惠
        /// </summary>
        /// <returns></returns>
        public ActionResult ScanCodeDiscounts()
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
        /// 会员开卡
        /// </summary>
        /// <returns></returns>
        public ActionResult MemberShop()
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
        /// 手机绑定
        /// </summary>
        /// <returns></returns>
        public ActionResult BindingMobilePhone()
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
        /// 员工管理
        /// </summary>
        /// <returns></returns>
        public ActionResult StaffManager()
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
        /// 会员管理
        /// </summary>
        /// <returns></returns>
        public ActionResult MemberManager()
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
        /// 会员卡管理
        /// </summary>
        /// <returns></returns>
        public ActionResult MemberCardManager()
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
        /// 营业报表
        /// </summary>
        /// <returns></returns>
        public ActionResult OperatingStatement()
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
        /// 特权订单
        /// </summary>
        /// <returns></returns>
        public ActionResult PrivilegeOrder()
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
        /// 卡券核销
        /// </summary>
        /// <returns></returns>
        public ActionResult CouponVerification()
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
        /// 出/入库
        /// </summary>
        /// <returns></returns>
        public ActionResult StockOutIn()
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
        /// 库存记录
        /// </summary>
        /// <returns></returns>
        public ActionResult StockRecord()
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
        /// 会员详情
        /// </summary>
        /// <returns></returns>
        public ActionResult MemberDetails()
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
        /// 生成商户外部订单号
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GenerateOutTradeNo")]
        public string GenerateOutTradeNo()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmssfff");
        }

        /// <summary>
        /// 工具/耗材
        /// </summary>
        /// <returns></returns>
        public ActionResult ToolsManagement()
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
        /// 门店位置设置
        /// </summary>
        /// <returns></returns>
        public ActionResult DepartmentLocation()
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
        /// 我的分红
        /// </summary>
        /// <returns></returns>
        public ActionResult MyCommission()
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
        /// 我的业绩
        /// </summary>
        /// <returns></returns>
        public ActionResult MyPerformance()
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