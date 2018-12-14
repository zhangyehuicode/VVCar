using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VVCar.Controllers
{
    public class ReportingController : Controller
    {
        // GET: Reporting
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AnalyseIndex()
        {
            return View();
        }

        public ActionResult AnalysePieChart()
        {
            return View();
        }

        public ActionResult AnalyseLineChart()
        {
            return View();
        }

        public ActionResult ProductRetailStatisticsChart()
        {
            return View();
        }
    }
}