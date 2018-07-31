using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VVCar.Shop.Domain.Dtos;
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
        [HttpPost, AllowAnonymous]
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
        [HttpGet, AllowAnonymous]
        public PagedActionResult<OrderDto> Search([FromUri]OrderFilter filter)
        {
            return SafeGetPagedData<OrderDto>((result) =>
            {
                var totalCount = 0;
                var data = OrderService.Search(filter, out totalCount);
                result.Data = data;
                result.TotalCount = totalCount;
            });
        }

        /// <summary>
        /// 生成订单号
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetTradeNo"), AllowAnonymous]
        public JsonActionResult<string> GetTradeNo()
        {
            return SafeExecute(() =>
            {
                return OrderService.GetTradeNo();
            });
        }

        /// <summary>
        /// 发货
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        [HttpPost, Route("Delivery")]
        public JsonActionResult<bool> Delivery(Order order)
        {
            return SafeExecute(() =>
            {
                return OrderService.Delivery(order);
            });
        }

        /// <summary>
        /// 微信端发货
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        [HttpPost, Route("DeliveryByWeChat"), AllowAnonymous]
        public JsonActionResult<bool> DeliveryByWeChat(Order order)
        {
            return SafeExecute(() =>
            {
                return OrderService.Delivery(order);
            });
        }

        /// <summary>
        /// 取消发货
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet, Route("AntiDelivery")]
        public JsonActionResult<bool> AntiDelivery(Guid id)
        {
            return SafeExecute(() =>
            {
                return OrderService.AntiDelivery(id);
            });
        }

        /// <summary>
        /// 取消发货
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet, Route("AntiDeliveryByWeChat"), AllowAnonymous]
        public JsonActionResult<bool> AntiDeliveryByWeChat(Guid id)
        {
            return SafeExecute(() =>
            {
                return OrderService.AntiDelivery(id);
            });
        }
    }
}
