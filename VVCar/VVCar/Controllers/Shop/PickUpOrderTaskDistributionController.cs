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
    /// 接车单任务分配
    /// </summary>
    [RoutePrefix("api/PickUpOrderTaskDistribution")]
    public class PickUpOrderTaskDistributionController : BaseApiController
    {
        public PickUpOrderTaskDistributionController(IPickUpOrderTaskDistributionService pickUpOrderTaskDistributionService)
        {
            PickUpOrderTaskDistributionService = pickUpOrderTaskDistributionService;
        }

        IPickUpOrderTaskDistributionService PickUpOrderTaskDistributionService { get; set; }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost, AllowAnonymous]
        public JsonActionResult<PickUpOrderTaskDistribution> Add(PickUpOrderTaskDistribution entity)
        {
            return SafeExecute(() =>
            {
                return PickUpOrderTaskDistributionService.Add(entity);
            });
        }

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="pickUpOrderTaskDistributions"></param>
        /// <returns></returns>
        [HttpPost, Route("BatchAdd")]
        public JsonActionResult<bool> BatchAdd(IEnumerable<PickUpOrderTaskDistribution> pickUpOrderTaskDistributions)
        {
            return SafeExecute(() =>
            {
                return PickUpOrderTaskDistributionService.BatchAdd(pickUpOrderTaskDistributions);
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
                return this.PickUpOrderTaskDistributionService.BatchDelete(parameter.IdList.ToArray());
            });
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet, Route("Delete"), AllowAnonymous]
        public JsonActionResult<bool> Delete(Guid id)
        {
            return SafeExecute(() =>
            {
                return PickUpOrderTaskDistributionService.Delete(id);
            });
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        public PagedActionResult<PickUpOrderTaskDistributionDto> Search([FromUri]PickUpOrderTaskDistributionFilter filter)
        {
            return SafeGetPagedData<PickUpOrderTaskDistributionDto>((result) =>
            {
                var totalCount = 0;
                var data = PickUpOrderTaskDistributionService.Search(filter, out totalCount);
                result.Data = data;
                result.TotalCount = totalCount;
            });
        }
    }
}
