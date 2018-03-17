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
    /// 订单
    /// </summary>
    [RoutePrefix("api/Order")]
    public class OrderController : BaseApiController
    {
        /// <summary>
        /// 订单
        /// </summary>
        /// <param name="orderService"></param>
        public OrderController(IOrderService orderService)
        {
            OrderService = orderService;
        }

        IOrderService OrderService { get; set; }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        [HttpPost]
        public JsonActionResult<Order> Add(Order entity)
        {
            return SafeExecute(() =>
            {
                return OrderService.Add(entity);
            });
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        [HttpPut]
        public JsonActionResult<bool> Update(Order entity)
        {
            return SafeExecute(() =>
            {
                return OrderService.Update(entity);
            });
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpDelete]
        public JsonActionResult<bool> Delete(Guid id)
        {
            return SafeExecute(() =>
            {
                return OrderService.Delete(id);
            });
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        [HttpGet]
        public PagedActionResult<Order> Search([FromUri]OrderFilter filter)
        {
            return SafeGetPagedData<Order>((result) =>
            {
                var totalCount = 0;
                var data = OrderService.Search(filter, out totalCount);
                result.Data = data;
                result.TotalCount = totalCount;
            });
        }
    }
}
