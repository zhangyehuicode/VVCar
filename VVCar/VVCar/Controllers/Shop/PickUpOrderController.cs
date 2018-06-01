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
    /// 接车单
    /// </summary>
    [RoutePrefix("api/PickUpOrder")]
    public class PickUpOrderController : BaseApiController
    {
        public PickUpOrderController(IPickUpOrderService pickUpOrderService)
        {
            PickUpOrderService = pickUpOrderService;
        }

        IPickUpOrderService PickUpOrderService { get; set; }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost, AllowAnonymous]
        public JsonActionResult<PickUpOrder> Add(PickUpOrder entity)
        {
            return SafeExecute(() =>
            {
                return PickUpOrderService.Add(entity);
            });
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet, AllowAnonymous]
        public PagedActionResult<PickUpOrder> Search([FromUri]PickUpOrderFilter filter)
        {
            return SafeGetPagedData<PickUpOrder>((result) =>
            {
                var totalCount = 0;
                var data = PickUpOrderService.Search(filter, ref totalCount);
                result.Data = data;
                result.TotalCount = totalCount;
            });
        }

        /// <summary>
        /// 结账
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet, Route("CheckOut"), AllowAnonymous]
        public JsonActionResult<bool> CheckOut(string code)
        {
            return SafeExecute(() =>
            {
                return PickUpOrderService.CheckOut(code);
            });
        }

        /// <summary>
        /// 获取接车单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet, Route("GetOrder"), AllowAnonymous]
        public JsonActionResult<PickUpOrder> GetOrder(Guid id)
        {
            return SafeExecute(() =>
            {
                return PickUpOrderService.GetOrder(id);
            });
        }

        /// <summary>
        /// 核销
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost, Route("Verification"), AllowAnonymous]
        public JsonActionResult<bool> Verification(VerificationParam param)
        {
            return SafeExecute(() =>
            {
                return PickUpOrderService.Verification(param);
            });
        }
    }
}
