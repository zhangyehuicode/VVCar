using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YEF.Core;

namespace VVCar.Areas.Coupon.Controllers
{
    public class AdminController : Controller
    {
        // GET: Coupon/Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CouponTemplate()
        {
            return View();
        }

        public ActionResult Report()
        {
            return View();
        }

        public ActionResult TotalReport()
        {
            return View();
        }

        public ActionResult VerificationCode()
        {
            return View();
        }

        public ActionResult Verification()
        {
            return View();
        }

        public ActionResult CouponDetails()
        {
            return View();
        }

        [HttpGet]
        [Route("GetSiteDomain")]
        public string GetSiteDomain()
        {
            return AppContext.Settings.SiteDomain;
        }

        [HttpGet]
        [Route("DownloadFile")]
        public void DownloadFile(string url)
        {
            var fileName = Path.Combine("Pictures", "CouponTemplateDeliveryQrCode", Path.GetFileName(url));
            HttpContext.Response.ContentType = "application/ms-download";
            string path = HttpContext.Server.MapPath("~/") + fileName;
            System.IO.FileInfo file = new System.IO.FileInfo(path);
            HttpContext.Response.Clear();
            HttpContext.Response.AddHeader("Content-Type", "application/octet-stream");
            HttpContext.Response.Charset = "utf-8";
            HttpContext.Response.AddHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode(file.Name, System.Text.Encoding.UTF8));
            HttpContext.Response.AddHeader("Content-Length", file.Length.ToString());
            HttpContext.Response.WriteFile(file.FullName);
            HttpContext.Response.Flush();
            HttpContext.Response.Clear();
            HttpContext.Response.End();
        }

        /// <summary>
        /// 领取记录
        /// </summary>
        /// <returns></returns>
        public ActionResult ReceivedCouponRecord()
        {
            return View();
        }

        /// <summary>
        /// 核销记录
        /// </summary>
        /// <returns></returns>
        public ActionResult VerificationRecord()
        {
            return View();
        }

        /// <summary>
        /// 卡券推送
        /// </summary>
        /// <returns></returns>
        public ActionResult CouponPush()
        {
            return View();
        }

        /// <summary>
        /// 卡券立即推送
        /// </summary>
        /// <returns></returns>
        public ActionResult ImmediatePush()
        {
            return View();
        }
    }
}