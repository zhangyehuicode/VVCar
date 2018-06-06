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
    /// 库存记录
    /// </summary>
    [RoutePrefix("api/StockRecord")]
    public class StockRecordController : BaseApiController
    {
        public StockRecordController(IStockRecordService stockRecordService)
        {
            StockRecordService = stockRecordService;
        }

        IStockRecordService StockRecordService { get; set; }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet, AllowAnonymous]
        public PagedActionResult<StockRecordDto> Search([FromUri]StockRecordFilter filter)
        {
            return SafeGetPagedData<StockRecordDto>((result) =>
            {
                var totalCount = 0;
                var data = StockRecordService.Search(filter, ref totalCount);
                result.Data = data;
                result.TotalCount = totalCount;
            });
        }
    }
}
