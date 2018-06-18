using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VVCar.Shop.Domain.Entities;
using VVCar.Shop.Domain.Services;
using YEF.Core.Dtos;

namespace VVCar.Controllers.Shop
{
    /// <summary>
    /// 商城订单支付明细
    /// </summary>
    [RoutePrefix("api/OrderPaymentDetails")]
    public class OrderPaymentDetailsController : BaseApiController
    {
        public OrderPaymentDetailsController(IOrderPaymentDetailsService orderPaymentDetailsService)
        {
            OrderPaymentDetailsService = orderPaymentDetailsService;
        }

        IOrderPaymentDetailsService OrderPaymentDetailsService { get; set; }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost, AllowAnonymous]
        public JsonActionResult<OrderPaymentDetails> Add(OrderPaymentDetails entity)
        {
            return SafeExecute(() =>
            {
                return OrderPaymentDetailsService.Add(entity);
            });
        }
    }
}
