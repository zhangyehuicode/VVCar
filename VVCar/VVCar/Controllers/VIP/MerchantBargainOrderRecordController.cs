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
    /// 发起砍价记录
    /// </summary>
    [RoutePrefix("api/MerchantBargainOrderRecord")]
    public class MerchantBargainOrderRecordController : BaseApiController
    {
        /// <summary>
        /// ctor.
        /// </summary>
        public MerchantBargainOrderRecordController(IMerchantBargainOrderRecordService merchantBargainOrderRecordService)
        {
            MerchantBargainOrderRecordService = merchantBargainOrderRecordService;
        }

        IMerchantBargainOrderRecordService MerchantBargainOrderRecordService { get; set; }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost, AllowAnonymous]
        public JsonActionResult<MerchantBargainOrderRecord> Add(MerchantBargainOrderRecord entity)
        {
            return SafeExecute(() =>
            {
                return MerchantBargainOrderRecordService.Add(entity);
            });
        }

        /// <summary>
        /// 新增拼单子项
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost, Route("AddMerchantBargainOrderRecordItem"), AllowAnonymous]
        public JsonActionResult<MerchantBargainOrderRecordDto> AddMerchantBargainOrderRecordItem(MerchantBargainOrderRecordItem entity)
        {
            return SafeExecute(() =>
            {
                return MerchantBargainOrderRecordService.AddMerchantBargainOrderRecordItem(entity);
            });
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet, AllowAnonymous]
        public PagedActionResult<MerchantBargainOrderRecordDto> Search([FromUri]MerchantBargainOrderRecordFilter filter)
        {
            return SafeGetPagedData<MerchantBargainOrderRecordDto>((result) =>
            {
                var totalCount = 0;
                result.Data = MerchantBargainOrderRecordService.Search(filter, out totalCount);
                result.TotalCount = totalCount;
            });
        }
    }
}
