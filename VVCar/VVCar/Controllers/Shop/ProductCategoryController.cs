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
using YEF.Core;
using YEF.Core.Dtos;

namespace VVCar.Controllers.Shop
{
    [RoutePrefix("api/ProductCategory")]
    public class ProductCategoryController : BaseApiController
    {
        /// <summary>
        /// API 初始化
        /// </summary>
        /// <param name="productCategoryService"></param>
        public ProductCategoryController(IProductCategoryService productCategoryService)
        {
            this.ProductCategoryService = productCategoryService;
        }

        IProductCategoryService ProductCategoryService { get; set; }

        /// <summary>
        /// 添加产品分类
        /// </summary>
        /// <param name="entity">产品实体对象</param>
        /// <returns></returns>
        [HttpPost]
        public JsonActionResult<ProductCategory> Add(ProductCategory entity)
        {
            return SafeExecute(() => this.ProductCategoryService.Add(entity));
        }

        /// <summary>
        /// 删除产品分类
        /// </summary>
        /// <param name="id">产品主键</param>
        /// <returns>是否删除成功</returns>
        [HttpDelete]
        public JsonActionResult<bool> Delete(Guid id)
        {
            return SafeExecute(() =>
            {
                return ProductCategoryService.Delete(id);
            });
        }

        /// <summary>
        /// 更新产品分类
        /// </summary>
        /// <param name="productCategory">产品分类实体</param>
        /// <returns>更新完成状态</returns>
        [HttpPut]
        public JsonActionResult<bool> Update(ProductCategory productCategory)
        {
            return SafeExecute(() =>
            {
                return ProductCategoryService.Update(productCategory);
            });
        }

        /// <summary>
        /// 获取树型结构数据
        /// </summary>
        /// <param name="parentID"></param>
        /// <returns></returns>
        [HttpGet, Route("GetTree")]
        public TreeActionResult<ProductCategoryTreeDto> GetTree(Guid? parentID)
        {
            return SafeGetTreeData(() =>
            {
                var productCategories = this.ProductCategoryService.GetTreeData(parentID);
                return productCategories;
            });
        }

        /// <summary>
        /// 获取产品分类列表，用于产品分类选择下拉框
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("LiteData")]
        public JsonActionResult<IList<IDCodeNameDto>> GetLiteData()
        {
            return SafeExecute(() =>
            {
                var categories = ProductCategoryService.GetLiteData();
                //categories.Insert(0, new IDCodeNameDto { ID = Guid.Empty, Code = string.Empty, Name = "所有分类" });
                return categories;
            });
        }

        /// <summary>
        /// 根据条件查询产品分类
        /// </summary>
        /// <param name="filter">keywords(分类名称，)</param>
        /// <returns>产品分类数据集</returns>
        [HttpGet]
        public PagedActionResult<ProductCategory> Search([FromUri]ProductCategoryFilter filter)
        {
            return SafeGetPagedData<ProductCategory>((result) =>
            {
                int totalCount = 0;
                var temp = ProductCategoryService.Search(filter, out totalCount);
                result.TotalCount = totalCount;
                result.Data = temp;
            });
        }
    }
}
