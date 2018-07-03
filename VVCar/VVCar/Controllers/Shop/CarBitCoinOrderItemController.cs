using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VVCar.Shop.Domain.Entities;
using VVCar.Shop.Domain.Filters;
using VVCar.Shop.Domain.Services;
using YEF.Core.Dtos;

namespace VVCar.Controllers.Shop
{
    /// <summary>
    /// 车比特订单子项
    /// </summary>
    [RoutePrefix("api/CarBitCoinOrderItem")]
    public class CarBitCoinOrderItemController : BaseApiController
    {
        public CarBitCoinOrderItemController(ICarBitCoinOrderItemService carBitCoinOrderItemService)
        {
            CarBitCoinOrderItemService = carBitCoinOrderItemService;
        }

        ICarBitCoinOrderItemService CarBitCoinOrderItemService { get; set; }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        public PagedActionResult<CarBitCoinOrderItem> Search([FromUri]OrderItemFilter filter)
        {
            return SafeGetPagedData<CarBitCoinOrderItem>((result) =>
            {
                var totalCount = 0;
                var data = CarBitCoinOrderItemService.Search(filter, ref totalCount);
                result.Data = data;
                result.TotalCount = totalCount;
            });
        }
    }
}
