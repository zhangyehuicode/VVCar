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
    /// 接车单支付明细
    /// </summary>
    [RoutePrefix("api/PickUpOrderPaymentDetails")]
    public class PickUpOrderPaymentDetailsController : BaseApiController
    {
        public PickUpOrderPaymentDetailsController(IPickUpOrderPaymentDetailsService pickUpOrderPaymentDetailsService)
        {
            PickUpOrderPaymentDetailsService = pickUpOrderPaymentDetailsService;
        }

        IPickUpOrderPaymentDetailsService PickUpOrderPaymentDetailsService { get; set; }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost, AllowAnonymous]
        public JsonActionResult<PickUpOrderPaymentDetails> Add(PickUpOrderPaymentDetails entity)
        {
            return SafeExecute(() =>
            {
                return PickUpOrderPaymentDetailsService.Add(entity);
            });
        }
    }
}
