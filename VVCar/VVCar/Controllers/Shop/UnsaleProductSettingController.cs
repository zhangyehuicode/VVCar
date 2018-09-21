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
    /// 滞销产品参数设置
    /// </summary>
    [RoutePrefix("api/UnsaleProductSetting")]
    public class UnsaleProductSettingController : BaseApiController
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="unsaleProductSettingService"></param>
        public UnsaleProductSettingController(IUnsaleProductSettingService unsaleProductSettingService)
        {
            UnsaleProductSettingService = unsaleProductSettingService;
        }

        IUnsaleProductSettingService UnsaleProductSettingService { get; set; }

        /// <summary>
        /// 新增
        /// </summary>
        [HttpPost]
        public JsonActionResult<UnsaleProductSetting> Add(UnsaleProductSetting entity)
        {
            return SafeExecute(() =>
            {
                return UnsaleProductSettingService.Add(entity);
            });
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <returns></returns>
        [HttpDelete, Route("BatchDelete")]
        public JsonActionResult<bool> BatchDelete(BatchOperationDto parameter)
        {
            return SafeExecute(() =>
            {
                return UnsaleProductSettingService.BatchDelete(parameter.IdList.ToArray());
            });
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPut]
        public JsonActionResult<bool> Update(UnsaleProductSetting entity)
        {
            return SafeExecute(() =>
            {
                return UnsaleProductSettingService.Update(entity);
            });
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        public PagedActionResult<UnsaleProductSettingDto> Search([FromUri]UnsaleProductSettingFilter filter)
        {
            return SafeGetPagedData<UnsaleProductSettingDto>((result) =>
            {
                if (!ModelState.IsValid)
                {
                    throw new DomainException("查询参数错误");
                }
                int totalCount = 0;
                var pageData = UnsaleProductSettingService.Search(filter, out totalCount);
                result.Data = pageData;
                result.TotalCount = totalCount;
            });
        }
    }
}
