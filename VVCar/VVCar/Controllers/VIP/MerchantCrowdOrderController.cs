using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using VVCar.VIP.Domain.Dtos;
using VVCar.VIP.Domain.Entities;
using VVCar.VIP.Domain.Filters;
using VVCar.VIP.Domain.Services;
using YEF.Core.Dtos;

namespace VVCar.Controllers.VIP
{
    /// <summary>
    /// 门店拼单
    /// </summary>
    [RoutePrefix("api/MerchantCrowdOrder")]
    public class MerchantCrowdOrderController : BaseApiController
    {
        public MerchantCrowdOrderController(IMerchantCrowdOrderService merchantCrowdOrderService)
        {
            MerchantCrowdOrderService = merchantCrowdOrderService;
        }

        IMerchantCrowdOrderService MerchantCrowdOrderService { get; set; }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonActionResult<MerchantCrowdOrder> Add(MerchantCrowdOrder entity)
        {
            return SafeExecute(() =>
            {
                return MerchantCrowdOrderService.Add(entity);
            });
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPut]
        public JsonActionResult<bool> Update(MerchantCrowdOrder entity)
        {
            return SafeExecute(() =>
            {
                return MerchantCrowdOrderService.Update(entity);
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
                return MerchantCrowdOrderService.BatchDelete(parameter.IdList.ToArray());
            });
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        public PagedActionResult<MerchantCrowdOrderDto> Search([FromUri]MerchantCrowdOrderFilter filter)
        {
            return SafeGetPagedData<MerchantCrowdOrderDto>((result) =>
            {
                var totalCount = 0;
                var data = MerchantCrowdOrderService.Search(filter, out totalCount);
                result.Data = data;
                result.TotalCount = totalCount;
            });
        }

        /// <summary>
        /// 通过产品ID获取拼单信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet, Route("GetMerchantCrowdOrderListByProductID"), AllowAnonymous]
        public PagedActionResult<MerchantCrowdOrderDto> GetMerchantCrowdOrderListByProductID(Guid id)
        {
            return SafeGetPagedData<MerchantCrowdOrderDto>((result) =>
            {
                result.Data = MerchantCrowdOrderService.GetMerchantCrowdOrderListByProductID(id);
            });
        }
    }
}