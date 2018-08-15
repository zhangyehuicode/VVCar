using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YEF.Core;

namespace VVCar.Areas.Coupon.Controllers
{
    /// <summary>
    /// 优惠券
    /// </summary>
    public class CouponController : Controller
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
            var mch = Request["mch"];
            if (string.IsNullOrEmpty(mch))
            {
                throw new Exception("参数错误");
            }
            if (string.IsNullOrEmpty(openId))//|| string.IsNullOrEmpty(nickname) || string.IsNullOrEmpty(headimgurl)
            {
                var redirectUrl = Server.UrlEncode(Request.Url.AbsoluteUri);
                var authUrl = string.Format("{0}/Auth?companyCode={1}&UserInfo=true&scope=snsapi_userinfo&RedirectUrl={2}",
                    AppContext.Settings.WeChatIntegrationService,
                    mch,
                    redirectUrl);
                return Redirect(authUrl);
            }
            Session["openid"] = openId.Split(',')[0];
            Session["nickname"] = nickname;
            Session["headimgurl"] = headimgurl;
            Session["subscribe"] = Request["subscribe"];
            var redirectTo = Request["redirectTo"];
            if (string.IsNullOrEmpty(redirectTo))
                return RedirectToAction("MyCoupon", new { mch = mch });
            else
                return Redirect(redirectTo);
        }

        /// <summary>
        /// 我的优惠券
        /// </summary>
        /// <returns></returns>
        public ActionResult MyCoupon()
        {
            var openId = Session["openid"] as string;
            var nickname = Session["nickname"] as string;
            var headimgurl = Session["headimgurl"] as string;
#if DEBUG
            openId = "oI4ee0sGQu_E2tkp7OUdU2ADzR0U";
            Session["subscribe"] = "1";
#endif
            var mch = Request["mch"];
            if (string.IsNullOrEmpty(openId) || string.IsNullOrEmpty(nickname) || string.IsNullOrEmpty(headimgurl))
            {
                return RedirectToAction("Auth", new { redirectTo = Server.UrlEncode(Request.Url.AbsoluteUri), mch = mch });
            }
            ViewBag.openid = openId;
            ViewBag.nickname = Session["nickname"];
            ViewBag.headimgurl = Session["headimgurl"];
            ViewBag.subscribe = Session["subscribe"];
            return View();
        }

        /// <summary>
        /// 优惠券详情
        /// </summary>
        /// <param name="ctid">优惠券模板Id</param>
        /// <returns></returns>
        public ActionResult CouponInfo(string ctid)
        {
            var couponTemplateID = Guid.Empty;
            if (!Guid.TryParse(ctid, out couponTemplateID))
            {
                ViewBag.ErrorMessage = "访问参数错误";
                return View();
            }
            var openId = Session["openid"] as string;
            var nickname = Session["nickname"] as string;
            var headimgurl = Session["headimgurl"] as string;
#if DEBUG
            openId = "oI4ee0sGQu_E2tkp7OUdU2ADzR0U";
            Session["subscribe"] = "1";
#endif
            var mch = Request["mch"];
            if (string.IsNullOrEmpty(openId))//|| string.IsNullOrEmpty(nickname) || string.IsNullOrEmpty(headimgurl)
            {
                return RedirectToAction("Auth", new { redirectTo = Server.UrlEncode(Request.Url.AbsoluteUri), mch = mch });
            }
            try
            {
                ViewBag.openid = openId;
                ViewBag.nickname = Session["nickname"];
                ViewBag.headimgurl = Session["headimgurl"];
                ViewBag.subscribe = Session["subscribe"];
                ViewBag.FromMycoupon = Request["frommycoupon"] != null ? Request["frommycoupon"] : "false";
                if ("false".Equals(ViewBag.FromMycoupon))
                {
                    Session["openid"] = string.Empty;
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
            }
            return View();
        }

        /// <summary>
        /// 门店列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult CouponStore(string id)
        {
            return View();
        }

        /// <summary>
        /// 适用商品列表
        /// </summary>
        /// <returns></returns>
        public ActionResult CouponProduct()
        {
            return View();
        }

        /// <summary>
        /// 使用优惠券
        /// </summary>
        /// <param name="couponid"></param>
        /// <returns></returns>
        public ActionResult UseCoupon(string couponid)
        {
            var couponID = Guid.Empty;
            if (!Guid.TryParse(couponid, out couponID))
            {
                ViewBag.ErrorMessage = "参数错误";
                return View();
            }
            return View();
        }

        /// <summary>
        /// 领券中心
        /// </summary>
        /// <returns></returns>
        public ActionResult CouponCenter()
        {
            var openId = Session["openid"] as string;
            var nickname = Session["nickname"] as string;
            var headimgurl = Session["headimgurl"] as string;

#if DEBUG
            openId = "oI4ee0sGQu_E2tkp7OUdU2ADzR0U";
            Session["subscribe"] = "1";
#endif

            var mch = Request["mch"];
            if (string.IsNullOrEmpty(openId))//|| string.IsNullOrEmpty(nickname) || string.IsNullOrEmpty(headimgurl)
            {
                return RedirectToAction("Auth", new { redirectTo = Server.UrlEncode(Request.Url.AbsoluteUri), mch = mch });
            }
            ViewBag.openid = openId;
            ViewBag.nickname = Session["nickname"];
            ViewBag.headimgurl = Session["headimgurl"];
            ViewBag.subscribe = Session["subscribe"];
            return View();
        }

        /// <summary>
        /// 详情介绍
        /// </summary>
        /// <param name="couponID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("IntroDetail")]
        public ActionResult IntroDetail(string couponID)
        {
            var couponTemplateID = Guid.Empty;
            if (!Guid.TryParse(couponID, out couponTemplateID))
            {
                ViewBag.ErrorMessage = "参数错误";
                return View();
            }
            var ownerOpenID = Session["openid"] as string;
#if DEBUG
            ownerOpenID = "oI4ee0sGQu_E2tkp7OUdU2ADzR0U";
            Session["subscribe"] = "1";
#endif
            return View();
        }

        /// <summary>
        /// 分享
        /// </summary>
        /// <returns></returns>
        public ActionResult Share()
        {
            return View();
        }
    }
}