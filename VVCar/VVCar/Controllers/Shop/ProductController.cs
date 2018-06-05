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
    /// 产品
    /// </summary>
    [RoutePrefix("api/Product")]
    public class ProductController : BaseApiController
    {
        /// <summary>
        /// 产品
        /// </summary>
        /// <param name="productService"></param>
        public ProductController(IProductService productService)
        {
            ProductService = productService;
        }

        IProductService ProductService { get; set; }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        [HttpPost]
        public JsonActionResult<Product> Add(Product entity)
        {
            return SafeExecute(() =>
            {
                return ProductService.Add(entity);
            });
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        [HttpPut]
        public JsonActionResult<bool> Update(Product entity)
        {
            return SafeExecute(() =>
            {
                return ProductService.Update(entity);
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
                return ProductService.Delete(id);
            });
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        [HttpGet, AllowAnonymous]
        public PagedActionResult<Product> Search([FromUri]ProductFilter filter)
        {
            return SafeGetPagedData<Product>((result) =>
            {
                var totalCount = 0;
                var data = ProductService.Search(filter, out totalCount);
                result.Data = data;
                result.TotalCount = totalCount;
            });
        }

        /// <summary>
        /// 调整索引
        /// </summary>
        /// <param name="param">The parameter.</param>
        /// <returns></returns>
        [HttpGet, Route("AdjustIndex")]
        public JsonActionResult<bool> AdjustIndex([FromUri]AdjustIndexParam param)
        {
            return SafeExecute(() =>
            {
                return ProductService.AdjustIndex(param);
            });
        }

        /// <summary>
        /// 更改发布状态
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet, Route("ChangePublishStatus"), AllowAnonymous]
        public JsonActionResult<bool> ChangePublishStatus(Guid id)
        {
            return SafeExecute(() =>
            {
                return ProductService.ChangePublishStatus(id);
            });
        }

        /// <summary>
        /// 获取产品列表
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("ProductList")]
        public JsonActionResult<IEnumerable<ProductCategoryLiteDto>> GetProductList()
        {
            return SafeExecute<IEnumerable<ProductCategoryLiteDto>>(() =>
            {
                return ProductService.GetProductLiteData();
            });
        }

        /// <summary>
        /// 获取推荐产品
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetRecommendProduct"), AllowAnonymous]
        public PagedActionResult<Product> GetRecommendProduct()
        {
            return SafeGetPagedData<Product>((result) =>
            {
                var data = ProductService.GetRecommendProduct();
                result.Data = data;
            });
        }

        /// <summary>
        /// 获取可上架产品
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetProduct"), AllowAnonymous]
        public PagedActionResult<ProductDto> GetProduct()
        {
            return SafeGetPagedData<ProductDto>((result) =>
            {
                var data = ProductService.GetProduct();
                result.Data = data;
            });
        }

        /// <summary>
        /// 获取服务
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetAppointmentProduct"), AllowAnonymous]
        public PagedActionResult<Product> GetAppointmentProduct()
        {
            return SafeGetPagedData<Product>((result) =>
            {
                var data = ProductService.GetAppointmentProduct();
                result.Data = data;
            });
        }

        /// <summary>
        /// 接车单历史数据分析
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetHistoryAnalysisData"), AllowAnonymous]
        public PagedActionResult<HistoryDataAnalysisDto> GetHistoryAnalysisData()
        {
            return SafeGetPagedData<HistoryDataAnalysisDto>((result) =>
            {
                result.Data = ProductService.GetHistoryAnalysisData();
            });
        }
    }
}
