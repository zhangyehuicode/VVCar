using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VVCar.Shop.Domain.Dtos;
using VVCar.Shop.Domain.Filters;
using VVCar.Shop.Domain.Services;
using YEF.Core.Dtos;

namespace VVCar.Controllers.Shop
{
    /// <summary>
    /// 物流
    /// </summary>
    [RoutePrefix("api/Logistics")]
    public class LogisticsController : BaseApiController
    {
        public LogisticsController(ILogisticsService logisticsService)
        {
            LogisticsService = logisticsService;
        }

        ILogisticsService LogisticsService { get; set; }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet, AllowAnonymous]
        public PagedActionResult<LogisticsDto> Search(LogisticsFilter filter)
        {
            return SafeGetPagedData<LogisticsDto>((result) =>
            {
                var totalCount = 0;
                var data = LogisticsService.Search(filter, out totalCount);
                result.Data = data;
                result.TotalCount = totalCount;
            });
        }

        /// <summary>
        /// 发货
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet, Route("Delivery")]
        public JsonActionResult<bool> Delivery(Guid id)
        {
            return SafeExecute(() =>
            {
                return LogisticsService.Delivery(id);
            });
        }
    }
}
