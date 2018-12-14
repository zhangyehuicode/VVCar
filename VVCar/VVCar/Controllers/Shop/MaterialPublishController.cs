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
    /// 广告发布
    /// </summary>
    [RoutePrefix("api/MaterialPublish")]
    public class MaterialPublishController : BaseApiController
    {
        public MaterialPublishController(IMaterialPublishService materialPublishService)
        {
            MaterialPublishService = materialPublishService;
        }

        IMaterialPublishService MaterialPublishService { get; set; }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="materialPublish"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonActionResult<MaterialPublish> Add(MaterialPublish materialPublish)
        {
            return SafeExecute(() =>
            {
                return MaterialPublishService.Add(materialPublish);
            });
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        [HttpDelete, Route("BatchDelete")]
        public JsonActionResult<bool> BatchDelete(BatchOperationDto parameter)
        {
            return SafeExecute(() =>
            {
                return MaterialPublishService.BatchDelete(parameter.IdList.ToArray());
            });
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPut]
        public JsonActionResult<bool> Update(MaterialPublish entity)
        {
            return SafeExecute(() =>
            {
                return MaterialPublishService.Update(entity);
            });
        }

        /// <summary>
        /// 批量发布
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        [HttpPost, Route("BatchHandMaterialPublish")]
        public JsonActionResult<bool> BatchHandMaterialPublish(BatchOperationDto parameter)
        {
            return SafeExecute(() =>
            {
                return MaterialPublishService.BatchHandMaterialPublish(parameter.IdList.ToArray());
            });
        }

        /// <summary>
        /// 批量取消发布
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        [HttpPost, Route("BatchHandCancelMaterialPublish")]
        public JsonActionResult<bool> BatchHandCancelMaterialPublish(BatchOperationDto parameter)
        {
            return SafeExecute(() =>
            {
                return MaterialPublishService.BatchHandCancelMaterialPublish(parameter.IdList.ToArray());
            });
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        public PagedActionResult<MaterialPublish> Search([FromUri]MaterialPublishFilter filter)
        {
            return SafeGetPagedData<MaterialPublish>((result) =>
            {
                var totalCount = 0;
                var data = MaterialPublishService.Search(filter, out totalCount);
                result.Data = data;
                result.TotalCount = totalCount;
            });
        }
    }
}
