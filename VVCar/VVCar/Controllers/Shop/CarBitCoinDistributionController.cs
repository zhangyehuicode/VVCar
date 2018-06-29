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
    /// 车比特分配
    /// </summary>
    [RoutePrefix("api/CarBitCoinDistribution")]
    public class CarBitCoinDistributionController : BaseApiController
    {
        public CarBitCoinDistributionController(ICarBitCoinDistributionService carBitCoinDistributionService)
        {
            CarBitCoinDistributionService = carBitCoinDistributionService;
        }

        ICarBitCoinDistributionService CarBitCoinDistributionService { get; set; }

        /// <summary>
        /// 转换车比特到会员
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet, Route("CarBitCoinTransform"), AllowAnonymous]
        public JsonActionResult<bool> CarBitCoinTransform(Guid id)
        {
            return SafeExecute(() =>
            {
                return CarBitCoinDistributionService.CarBitCoinTransform(id);
            });
        }

        /// <summary>
        /// 分配车比特
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("DistributionCarBitCoin"), AllowAnonymous]
        public JsonActionResult<bool> DistributionCarBitCoin()
        {
            return SafeExecute(() =>
            {
                return CarBitCoinDistributionService.DistributionCarBitCoin(null);
            });
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet, AllowAnonymous]
        public PagedActionResult<CarBitCoinDistribution> Search([FromUri]CarBitCoinDistributionFilter filter)
        {
            return SafeGetPagedData<CarBitCoinDistribution>((result) =>
            {
                var totalCount = 0;
                var data = CarBitCoinDistributionService.Search(filter, out totalCount);
                result.Data = data;
                result.TotalCount = totalCount;
            });
        }
    }
}
