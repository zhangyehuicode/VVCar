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
    /// 发起拼单记录
    /// </summary>
    [RoutePrefix("api/MerchantCrowdOrderRecord")]
    public class MerchantCrowdOrderRecordController : BaseApiController
    {
        /// <summary>
        /// ctor.
        /// </summary>
        public MerchantCrowdOrderRecordController(IMerchantCrowdOrderRecordService merchantCrowdOrderRecordService)
        {
            MerchantCrowdOrderRecordService = merchantCrowdOrderRecordService;
        }

        IMerchantCrowdOrderRecordService MerchantCrowdOrderRecordService { get; set; }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost, AllowAnonymous]
        public JsonActionResult<MerchantCrowdOrderRecord> Add(MerchantCrowdOrderRecord entity)
        {
            return SafeExecute(() =>
            {
                return MerchantCrowdOrderRecordService.Add(entity);
            });
        }

        /// <summary>
        /// 新增拼单子项
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost, Route("AddMerchantCrowdOrderRecordItem"), AllowAnonymous]
        public JsonActionResult<MerchantCrowdOrderRecordDto> AddMerchantCrowdOrderRecordItem(MerchantCrowdOrderRecordItem entity)
        {
            return SafeExecute(() =>
            {
                return MerchantCrowdOrderRecordService.AddMerchantCrowdOrderRecordItem(entity);
            });
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet, AllowAnonymous]
        public PagedActionResult<MerchantCrowdOrderRecordDto> Search([FromUri]MerchantCrowdOrderRecordFilter filter)
        {
            return SafeGetPagedData<MerchantCrowdOrderRecordDto>((result) =>
            {
                var totalCount = 0;
                result.Data = MerchantCrowdOrderRecordService.Search(filter, out totalCount);
                result.TotalCount = totalCount;
            });
        }
    }
}
