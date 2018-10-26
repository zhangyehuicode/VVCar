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
    /// 接车单支付明细
    /// </summary>
    [RoutePrefix("api/PickUpOrderPaymentDetails")]
    public class PickUpOrderPaymentDetailsController : BaseApiController
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="pickUpOrderPaymentDetailsService"></param>
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

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        public PagedActionResult<PickUpOrderPaymentDetails> Search([FromUri]PickUpOrderPaymentDetailsFilter filter)
        {
            return SafeGetPagedData<PickUpOrderPaymentDetails>((result) =>
            {
                var totalCount = 0;
                var data = PickUpOrderPaymentDetailsService.Search(filter, out totalCount);
                result.Data = data;
                result.TotalCount = totalCount;
            });
        }
    }
}
