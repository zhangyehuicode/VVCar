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
    /// <summary>
    /// 车比特商城
    /// </summary>
    [RoutePrefix("api/CarBitCoinProductCategory")]
    public class CarBitCoinProductCategoryController : BaseApiController
    {
        /// <summary>
        /// API 初始化
        /// </summary>
        /// <param name="carBitCoinProductCategoryService"></param>
        public CarBitCoinProductCategoryController(ICarBitCoinProductCategoryService carBitCoinProductCategoryService)
        {
            this.CarBitCoinProductCategoryService = carBitCoinProductCategoryService;
        }

        ICarBitCoinProductCategoryService CarBitCoinProductCategoryService { get; set; }

        /// <summary>
        /// 添加产品分类
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public JsonActionResult<CarBitCoinProductCategory> Add(CarBitCoinProductCategory entity)
        {
            return SafeExecute(() => this.CarBitCoinProductCategoryService.Add(entity));
        }

        /// <summary>
        /// 删除产品分类
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public JsonActionResult<bool> Delete(Guid id)
        {
            return SafeExecute(() =>
            {
                return CarBitCoinProductCategoryService.Delete(id);
            });
        }

        /// <summary>
        /// 更新产品分类
        /// </summary>
        /// <param name="carBitCoinProductCategory"></param>
        /// <returns></returns>
        [HttpPut]
        public JsonActionResult<bool> Update(CarBitCoinProductCategory carBitCoinProductCategory)
        {
            return SafeExecute(() =>
            {
                return this.CarBitCoinProductCategoryService.Update(carBitCoinProductCategory);
            });
        }

        /// <summary>
        /// 获取树形结构数据
        /// </summary>
        /// <param name="parentID"></param>
        /// <returns></returns>
        [HttpGet, Route("GetTree")]
        public TreeActionResult<CarBitCoinProductCategoryTreeDto> GetTree(Guid? parentID)
        {
            return SafeGetTreeData(() =>
            {
                var carBitCoinProductCategories = this.CarBitCoinProductCategoryService.GetTreeData(parentID);
                return carBitCoinProductCategories;
            });
        }

        /// <summary>
        /// 获取产品分类列表， 用于产品分类选择下拉框
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("LiteData")]
        public JsonActionResult<IList<IDCodeNameDto>> GetLiteData()
        {
            return SafeExecute(() =>
            {
                var categories = this.CarBitCoinProductCategoryService.GetLiteData();
                return categories;
            });
        }

        /// <summary>
        /// 根据条件查询产品分类
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        public PagedActionResult<CarBitCoinProductCategory> Search([FromUri]CarBitCoinProductCategoryFilter filter)
        {
            return SafeGetPagedData<CarBitCoinProductCategory>((result) =>
            {
                int totalCount = 0;
                var temp = this.CarBitCoinProductCategoryService.Search(filter, out totalCount);
                result.TotalCount = totalCount;
                result.Data = temp;
            });
        }
    }
}
