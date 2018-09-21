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
    /// 滞销产品参数设置子项
    /// </summary>
    [RoutePrefix("api/UnsaleProductSettingItem")]
    public class UnsaleProductSettingItemController : BaseApiController
    {
        /// <summary>
        /// ctor
        /// </summary>
        public UnsaleProductSettingItemController(IUnsaleProductSettingItemService unsaleProductSettingItemService)
        {
            UnsaleProductSettingItemService = unsaleProductSettingItemService;
        }

        IUnsaleProductSettingItemService UnsaleProductSettingItemService { get; set; }

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="unsaleProductSettingItems"></param>
        /// <returns></returns>
        [HttpPost, Route("BatchAdd")]
        public JsonActionResult<bool> BatchAdd(IEnumerable<UnsaleProductSettingItem> unsaleProductSettingItems)
        {
            return SafeExecute(() =>
            {
                if (unsaleProductSettingItems == null)
                    throw new DomainException("参数错误");
                return UnsaleProductSettingItemService.BatchAdd(unsaleProductSettingItems);
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
                return UnsaleProductSettingItemService.BatchDelete(parameter.IdList.ToArray());
            });
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        public PagedActionResult<UnsaleProductSettingItemDto> Search([FromUri]UnsaleProductSettingItemFilter filter)
        {
            return SafeGetPagedData<UnsaleProductSettingItemDto>((result) =>
            {
                if (!ModelState.IsValid)
                {
                    throw new DomainException("查询参数错误");
                }
                int totalCount = 0;
                var pageData = UnsaleProductSettingItemService.Search(filter, out totalCount);
                result.Data = pageData;
                result.TotalCount = totalCount;
            });
        }
    }
}
