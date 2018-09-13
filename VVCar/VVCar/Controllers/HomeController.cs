using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YEF.Core;

namespace VVCar.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var host = HttpContext.Request.Url.Host;
            ViewBag.IsMrTarot = false;
            ViewBag.HQName = "车因子";
            if (host.Contains("mrtarot"))
            {
                ViewBag.Title = "塔罗先生智慧管理系统";
                ViewBag.IsMrTarot = true;
                ViewBag.HQName = "塔罗先生";
            }
            else
            {
                ViewBag.Title = AppContext.Settings.SystemTitle;
            }
            return View();
        }
    }
}