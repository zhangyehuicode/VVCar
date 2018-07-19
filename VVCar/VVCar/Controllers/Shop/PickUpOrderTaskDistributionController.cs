using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VVCar.Shop.Domain.Entities;
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
    }
}
