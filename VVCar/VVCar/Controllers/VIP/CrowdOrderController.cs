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
    /// 拼单
    /// </summary>
    [RoutePrefix("api/CrowdOrder")]
    public class CrowdOrderController : BaseApiController
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="crowdOrderService"></param>
        public CrowdOrderController(ICrowdOrderService crowdOrderService)
        {
            CrowdOrderService = crowdOrderService;
        }

        ICrowdOrderService CrowdOrderService { get; set; }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonActionResult<CrowdOrder> Add(CrowdOrder entity)
        {
            return SafeExecute(() =>
            {
                return CrowdOrderService.Add(entity);
            });
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPut]
        public JsonActionResult<bool> Update(CrowdOrder entity)
        {
            return SafeExecute(() =>
            {
                return CrowdOrderService.Update(entity);
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
                return CrowdOrderService.BatchDelete(parameter.IdList.ToArray());
            });
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        public PagedActionResult<CrowdOrderDto> Search([FromUri]CrowdOrderFilter filter)
        {
            return SafeGetPagedData<CrowdOrderDto>((result) =>
            {
                var totalCount = 0;
                var data = CrowdOrderService.Search(filter, out totalCount);
                result.Data = data;
                result.TotalCount = totalCount;
            });
        }

        /// <summary>
        /// 获取拼单数据
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetCrowdOrders"), AllowAnonymous]
        public PagedActionResult<CrowdOrderDto> GetCrowdOrders()
        {
            return SafeGetPagedData<CrowdOrderDto>((result) =>
            {
                result.Data = CrowdOrderService.GetCrowdOrders();
            });
        }
    }
}
