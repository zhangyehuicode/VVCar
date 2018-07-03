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
    /// 车比特会员引擎
    /// </summary>
    [RoutePrefix("api/CarBitCoinMemberEngine")]
    public class CarBitCoinMemberEngineController : BaseApiController
    {
        public CarBitCoinMemberEngineController(ICarBitCoinMemberEngineService carBitCoinMemberEngineService)
        {
            CarBitCoinMemberEngineService = carBitCoinMemberEngineService;
        }

        ICarBitCoinMemberEngineService CarBitCoinMemberEngineService { get; set; }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet, AllowAnonymous]
        public PagedActionResult<CarBitCoinMemberEngine> Search([FromUri]CarBitCoinMemberEngineFilter filter)
        {
            return SafeGetPagedData<CarBitCoinMemberEngine>((result) =>
            {
                var totalCount = 0;
                result.Data = CarBitCoinMemberEngineService.Search(filter, out totalCount);
                result.TotalCount = totalCount;
            });
        }
    }
}
