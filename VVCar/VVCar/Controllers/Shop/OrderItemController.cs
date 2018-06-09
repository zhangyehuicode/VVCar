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
    /// 订单子项
    /// </summary>
    [RoutePrefix("api/OrderItem")]
    public class OrderItemController : BaseApiController
    {
        public OrderItemController(IOrderItemService orderItemService)
        {
            OrderItemService = orderItemService;
        }

        IOrderItemService OrderItemService { get; set; }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        public PagedActionResult<OrderItem> Search([FromUri]OrderItemFilter filter)
        {
            return SafeGetPagedData<OrderItem>((result) =>
            {
                var totalCount = 0;
                var data = OrderItemService.Search(filter, ref totalCount);
                result.Data = data;
                result.TotalCount = totalCount;
            });
        }
    }
}
