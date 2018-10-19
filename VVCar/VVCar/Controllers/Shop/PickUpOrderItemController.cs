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
    /// 接车单子项
    /// </summary>
    [RoutePrefix("api/PickUpOrderItem")]
    public class PickUpOrderItemController : BaseApiController
    {
        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="pickUpOrderItemService"></param>
        public PickUpOrderItemController(IPickUpOrderItemService pickUpOrderItemService)
        {
            PickUpOrderItemService = pickUpOrderItemService;
        }

        IPickUpOrderItemService PickUpOrderItemService { get; set; }

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="pickUpOrderItems"></param>
        /// <returns></returns>
        [HttpPost, Route("BatchAdd")]
        public JsonActionResult<PickUpOrder> BatchAdd(IEnumerable<PickUpOrderItem> pickUpOrderItems)
        {
            return SafeExecute(() =>
            {
                return PickUpOrderItemService.BatchAdd(pickUpOrderItems);
            });
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        [HttpDelete, Route("BatchDelete")]
        public JsonActionResult<PickUpOrder> BatchDelete(BatchOperationDto parameter)
        {
            return SafeExecute(() =>
            {
                return this.PickUpOrderItemService.BatchDelete(parameter.IdList.ToArray());
            });
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPut]
        public JsonActionResult<bool> Update(PickUpOrderItem entity)
        {
            return SafeExecute(() =>
            {
                return PickUpOrderItemService.Update(entity);
            });
        }

        /// <summary>
        /// 更新返回订单实体用于显示
        /// </summary>
        /// <param name="pickUpOrderItem"></param>
        /// <returns></returns>
        [HttpPut, Route("UpdatePickUpOrder")]
        public JsonActionResult<PickUpOrder> UpdatePickUpOrder(PickUpOrderItem pickUpOrderItem)
        {
            return SafeExecute(() =>
            {
                return PickUpOrderItemService.UpdatePickUpOrder(pickUpOrderItem);
            });
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        public PagedActionResult<PickUpOrderItem> Search([FromUri]PickUpOrderItemFilter filter)
        {
            return SafeGetPagedData<PickUpOrderItem>((result) =>
            {
                var totalCount = 0;
                var data = PickUpOrderItemService.Search(filter, out totalCount);
                result.Data = data;
                result.TotalCount = totalCount;
            });
        }
    }
}
