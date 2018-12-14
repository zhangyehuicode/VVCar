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
    /// 信息发布子项
    /// </summary>
    [RoutePrefix("api/MaterialPublishItem")]
    public class MaterialPublishItemController : BaseApiController
    {
        public MaterialPublishItemController(IMaterialPublishItemService materialPublishItemService)
        {
            MaterialPublishItemService = materialPublishItemService;
        }

        IMaterialPublishItemService MaterialPublishItemService { get; set; }

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="materialPublishItems"></param>
        /// <returns></returns>
        [HttpPost, Route("BatchAdd")]
        public JsonActionResult<bool> BatchAdd(IEnumerable<MaterialPublishItem> materialPublishItems)
        {
            return SafeExecute(() =>
            {
                if (materialPublishItems == null)
                    throw new DomainException("参数错误");
                return MaterialPublishItemService.BatchAdd(materialPublishItems);
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
                return MaterialPublishItemService.BatchDelete(parameter.IdList.ToArray());
            });
        }

        /// <summary>
        /// 批量查询
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        public PagedActionResult<MaterialPublishItemDto> Search([FromUri]MaterialPublishItemFilter filter)
        {
            return SafeGetPagedData<MaterialPublishItemDto>((result) =>
            {
                var totalCount = 0;
                var data = MaterialPublishItemService.Search(filter, out totalCount);
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
                return MaterialPublishItemService.AdjustIndex(param);
            });
        }
    }
}
