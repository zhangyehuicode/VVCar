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
    /// 车比特订单
    /// </summary>
    [RoutePrefix("api/CarBitCoinOrder")]
    public class CarBitCoinOrderController : BaseApiController
    {
        public CarBitCoinOrderController(ICarBitCoinOrderService carBitCoinOrderService)
        {
            CarBitCoinOrderService = carBitCoinOrderService;
        }

        ICarBitCoinOrderService CarBitCoinOrderService { get; set; }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        [HttpPost, AllowAnonymous]
        public JsonActionResult<CarBitCoinOrder> Add(CarBitCoinOrder entity)
        {
            return SafeExecute(() =>
            {
                return CarBitCoinOrderService.Add(entity);
            });
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        [HttpPut]
        public JsonActionResult<bool> Update(CarBitCoinOrder entity)
        {
            return SafeExecute(() =>
            {
                return CarBitCoinOrderService.Update(entity);
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
                return CarBitCoinOrderService.Delete(id);
            });
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        [HttpGet, AllowAnonymous]
        public PagedActionResult<CarBitCoinOrder> Search([FromUri]OrderFilter filter)
        {
            return SafeGetPagedData<CarBitCoinOrder>((result) =>
            {
                var totalCount = 0;
                var data = CarBitCoinOrderService.Search(filter, out totalCount);
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
                return CarBitCoinOrderService.GetTradeNo();
            });
        }
    }
}
