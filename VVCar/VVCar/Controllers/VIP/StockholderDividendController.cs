using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VVCar.VIP.Domain.Dtos;
using VVCar.VIP.Domain.Entities;
using VVCar.VIP.Domain.Filters;
using VVCar.VIP.Domain.Services;
using YEF.Core.Dtos;

namespace VVCar.Controllers.VIP
{
    /// <summary>
    /// 股东分红
    /// </summary>
    [RoutePrefix("api/StockholderDividend")]
    public class StockholderDividendController : BaseApiController
    {
        public StockholderDividendController(IStockholderDividendService stockholderDividendService)
        {
            StockholderDividendService = stockholderDividendService;
        }

        IStockholderDividendService StockholderDividendService { get; set; }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonActionResult<StockholderDividend> Add(StockholderDividend entity)
        {
            return SafeExecute(() =>
            {
                return StockholderDividendService.Add(entity);
            });
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet, AllowAnonymous]
        public PagedActionResult<StockholderDividendDto> Search([FromUri]StockholderDividendFilter filter)
        {
            return SafeGetPagedData<StockholderDividendDto>((result) =>
            {
                var totalCount = 0;
                result.Data = StockholderDividendService.Search(filter, out totalCount);
                result.TotalCount = totalCount;
            });
        }
    }
}
