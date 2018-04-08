﻿using System;
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
        [HttpGet, Route("ChangePublishStatus")]
        public JsonActionResult<bool> ChangePublishStatus(Guid id)
        {
            return SafeExecute(() =>
            {
                return ProductService.ChangePublishStatus(id);
            });
        }
    }
}