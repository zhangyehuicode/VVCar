using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VVCar.Shop.Domain.Dtos;
using VVCar.Shop.Domain.Services;
using YEF.Core.Dtos;

namespace VVCar.Controllers.Shop
{
    /// <summary>
    /// 报表
    /// </summary>
    [RoutePrefix("api/Reporting")]
    public class ReportingController : BaseApiController
    {
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="reportingService"></param>
        public ReportingController(IReportingService reportingService)
        {
            ReportingService = reportingService;
        }

        IReportingService ReportingService { get; set; }

        /// <summary>
        /// 营业报表
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetTurnoverReporting"), AllowAnonymous]
        public JsonActionResult<TurnoverReportingDto> GetTurnoverReporting()
        {
            return SafeExecute(() =>
            {
                return ReportingService.GetTurnoverReporting();
            });
        }

        /// <summary>
        /// 月营业额
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [HttpGet, Route("GetMonthTurnover"), AllowAnonymous]
        public JsonActionResult<MonthTurnoverDto> GetMonthTurnover(DateTime date)
        {
            return SafeExecute(() =>
            {
                return ReportingService.GetMonthTurnover(date);
            });
        }

        /// <summary>
        /// 获取员工绩效
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        [HttpGet, Route("GetStaffPerformance"), AllowAnonymous]
        public JsonActionResult<StaffPerformance> GetStaffPerformance(Guid userId, DateTime? date)
        {
            return SafeExecute(() =>
            {
                return ReportingService.GetStaffPerformance(userId, date);
            });
        }
    }
}
