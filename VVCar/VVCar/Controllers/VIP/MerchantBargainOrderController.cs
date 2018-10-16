using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VVCar.VIP.Domain.Dtos;
using VVCar.VIP.Domain.Entities;
using VVCar.VIP.Domain.Filters;
using VVCar.VIP.Domain.Services;
using YEF.Core.Dtos;

namespace VVCar.Controllers.VIP
{
    /// <summary>
    /// 门店砍价
    /// </summary>
    [RoutePrefix("api/MerchantBargainOrder")]
    public class MerchantBargainOrderController : BaseApiController
    {
        public MerchantBargainOrderController(IMerchantBargainOrderService merchantBargainOrderService)
        {
            MerchantBargainOrderService = merchantBargainOrderService;
        }

        IMerchantBargainOrderService MerchantBargainOrderService { get; set; }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonActionResult<MerchantBargainOrder> Add(MerchantBargainOrder entity)
        {
            return SafeExecute(() =>
            {
                return MerchantBargainOrderService.Add(entity);
            });
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPut]
        public JsonActionResult<bool> Update(MerchantBargainOrder entity)
        {
            return SafeExecute(() =>
            {
                return MerchantBargainOrderService.Update(entity);
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
                return MerchantBargainOrderService.BatchDelete(parameter.IdList.ToArray());
            });
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        public PagedActionResult<MerchantBargainOrderDto> Search([FromUri]MerchantBargainOrderFilter filter)
        {
            return SafeGetPagedData<MerchantBargainOrderDto>((result) =>
            {
                var totalCount = 0;
                var data = MerchantBargainOrderService.Search(filter, out totalCount);
                result.Data = data;
                result.TotalCount = totalCount;
            });
        }

        /// <summary>
        /// 通过产品ID获取拼单信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet, Route("GetMerchantBargainOrderListByProductID"), AllowAnonymous]
        public PagedActionResult<MerchantBargainOrderDto> GetMerchantBargainOrderListByProductID(Guid id)
        {
            return SafeGetPagedData<MerchantBargainOrderDto>((result) =>
            {
                result.Data = MerchantBargainOrderService.GetMerchantBargainOrderListByProductID(id);
            });
        }
    }
}
