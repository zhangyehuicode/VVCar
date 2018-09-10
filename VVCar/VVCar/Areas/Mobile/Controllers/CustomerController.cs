using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YEF.Core;

namespace VVCar.Areas.Mobile.Controllers
{
    /// <summary>
    /// 客户端
    /// </summary>
    public class CustomerController : Controller
    {
        // GET: Mobile/Customer

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
        /// 登录注册
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
        /// 登录注册(代理商)
        /// </summary>
        /// <returns></returns>
        public ActionResult AgentIndex()
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
        /// 首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Home()
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
        /// 精品商城
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
        /// 个人中心
        /// </summary>
        /// <returns></returns>
        public ActionResult PerCenter()
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
        /// 会员卡包
        /// </summary>
        /// <returns></returns>
        public ActionResult MemberCard()
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
        /// 服务详情
        /// </summary>
        /// <returns></returns>
        public ActionResult ServiceDetails()
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
        /// 购物车
        /// </summary>
        /// <returns></returns>
        public ActionResult ShoppingCart()
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
        /// 领券中心
        /// </summary>
        /// <returns></returns>
        public ActionResult CouponCenter()
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
        /// 会员卡购买
        /// </summary>
        /// <returns></returns>
        public ActionResult MemberCardBuy()
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
        /// 会员服务
        /// </summary>
        /// <returns></returns>
        public ActionResult MemberServices()
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
        /// 我的积分
        /// </summary>
        /// <returns></returns>
        public ActionResult MyPoints()
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
        /// 我的车牌
        /// </summary>
        /// <returns></returns>
        public ActionResult MyPlate()
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
        /// 核销密码设置
        /// </summary>
        /// <returns></returns>
        public ActionResult VerificationCodeSet()
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
        /// 游戏
        /// </summary>
        /// <returns></returns>
        public ActionResult Game()
        {
            var companyCode = Request["mch"];
            if (string.IsNullOrEmpty(companyCode))
            {
                return Content("参数错误");
            }
            var gameType = Request["GameType"];
            if (string.IsNullOrEmpty(gameType))
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
            ViewBag.GameType = gameType.Equals("0") ? 0 : 1;
            ViewBag.NickName = TempData["nickname"] as string;
            ViewBag.HeadImgUrl = TempData["headimgurl"] as string;
            ViewBag.CompanyCode = companyCode;
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
        /// 获取时间戳
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetTimeSpan")]
        public string GetTimeSpan()
        {
            return DateTime.Now.Ticks.ToString();
        }

        /// <summary>
        /// 拓客
        /// </summary>
        /// <returns></returns>
        public ActionResult TooKeen()
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
        /// 我的会员
        /// </summary>
        /// <returns></returns>
        public ActionResult MyMember()
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
        public ActionResult MyDividend()
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