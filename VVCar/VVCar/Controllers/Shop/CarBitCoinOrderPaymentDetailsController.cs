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
    /// 车比特订单支付明细
    /// </summary>
    [RoutePrefix("api/CarBitCoinOrderPaymentDetails")]
    public class CarBitCoinOrderPaymentDetailsController : BaseApiController
    {
        public CarBitCoinOrderPaymentDetailsController(ICarBitCoinOrderPaymentDetailsService carBitCoinOrderPaymentDetailsService)
        {
            CarBitCoinOrderPaymentDetailsService = carBitCoinOrderPaymentDetailsService;
        }

        ICarBitCoinOrderPaymentDetailsService CarBitCoinOrderPaymentDetailsService { get; set; }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost, AllowAnonymous]
        public JsonActionResult<CarBitCoinOrderPaymentDetails> Add(CarBitCoinOrderPaymentDetails entity)
        {
            return SafeExecute(() =>
            {
                return CarBitCoinOrderPaymentDetailsService.Add(entity);
            });
        }
    }
}
