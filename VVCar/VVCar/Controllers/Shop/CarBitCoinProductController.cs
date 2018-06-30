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
    /// 车比特产品
    /// </summary>
    [RoutePrefix("api/CarBitCoinProduct")]
    public class CarBitCoinProductController : BaseApiController
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="carBitCoinProductService"></param>
        public CarBitCoinProductController(ICarBitCoinProductService carBitCoinProductService)
        {
            CarBitCoinProductService = carBitCoinProductService;
        }

        ICarBitCoinProductService CarBitCoinProductService { get; set; }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonActionResult<CarBitCoinProduct> Add(CarBitCoinProduct entity)
        {
            return SafeExecute(() =>
            {
                return CarBitCoinProductService.Add(entity);
            });
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPut]
        public JsonActionResult<bool> Update(CarBitCoinProduct entity)
        {
            return SafeExecute(() =>
            {
                return CarBitCoinProductService.Update(entity);
            });
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public JsonActionResult<bool> Delete(Guid id)
        {
            return SafeExecute(() =>
            {
                return CarBitCoinProductService.Delete(id);
            });
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        public PagedActionResult<CarBitCoinProduct> Search([FromUri]CarBitCoinProductFilter filter)
        {
            return SafeGetPagedData<CarBitCoinProduct>((result) =>
            {
                var totalCount = 0;
                var data = CarBitCoinProductService.Search(filter, out totalCount);
                result.Data = data;
                result.TotalCount = totalCount;
            });
        }

        /// <summary>
        /// 调整索引
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpGet, Route("AdjustIndex")]
        public JsonActionResult<bool> AdjustIndex([FromUri]AdjustIndexParam param)
        {
            return SafeExecute(() =>
            {
                return CarBitCoinProductService.AdjustIndex(param);
            });
        }

        /// <summary>
        /// 更改发布状态
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet, Route("ChangePublishStatus")]
        public JsonActionResult<bool> ChangePublishStatus(Guid id)
        {
            return SafeExecute(() =>
            {
                return CarBitCoinProductService.ChangePublishStatus(id);
            });
        }

        /// <summary>
        /// 获取产品列表
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("CarBitCoinProductList")]
        public JsonActionResult<IEnumerable<CarBitCoinProductCategoryLiteDto>> GetProductList()
        {
            return SafeExecute<IEnumerable<CarBitCoinProductCategoryLiteDto>>(() =>
            {
                return CarBitCoinProductService.GetCarBitCoinProductLiteData();
            });
        }

        /// <summary>
        /// 获取推荐产品
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetRecommendProduct"), AllowAnonymous]
        public PagedActionResult<CarBitCoinProduct> GetRecommendProduct()
        {
            return SafeGetPagedData<CarBitCoinProduct>((result) =>
            {
                var data = CarBitCoinProductService.GetRecommendCarBitCoinProduct();
                result.Data = data;
            });
        }

        /// <summary>
        /// 获取可上架产品
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetProduct"), AllowAnonymous]
        public PagedActionResult<CarBitCoinProductDto> GetProduct()
        {
            return SafeGetPagedData<CarBitCoinProductDto>((result) =>
            {
                var data = CarBitCoinProductService.GetCarBitCoinProduct();
                result.Data = data;
            });
        }

        /// <summary>
        /// 获取引擎商品
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetEngineProduct"), AllowAnonymous]
        public PagedActionResult<CarBitCoinProduct> GetEngineProduct()
        {
            return SafeGetPagedData<CarBitCoinProduct>((result) =>
            {
                var data = CarBitCoinProductService.GetEngineProduct();
                result.Data = data;
            });
        }
    }
}