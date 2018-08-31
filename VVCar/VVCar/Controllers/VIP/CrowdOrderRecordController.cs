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
    [RoutePrefix("api/CrowdOrderRecord")]
    public class CrowdOrderRecordController : BaseApiController
    {
        /// <summary>
        /// ctor.
        /// </summary>
        public CrowdOrderRecordController(ICrowdOrderRecordService crowdOrderRecordService)
        {
            CrowdOrderRecordService = crowdOrderRecordService;
        }

        ICrowdOrderRecordService CrowdOrderRecordService { get; set; }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost, AllowAnonymous]
        public JsonActionResult<CrowdOrderRecord> Add(CrowdOrderRecord entity)
        {
            return SafeExecute(() =>
            {
                return CrowdOrderRecordService.Add(entity);
            });
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet, AllowAnonymous]
        public PagedActionResult<CrowdOrderRecordDto> Search([FromUri]CrowdOrderRecordFilter filter)
        {
            return SafeGetPagedData<CrowdOrderRecordDto>((result) =>
            {
                var totalCount = 0;
                result.Data = CrowdOrderRecordService.Search(filter, out totalCount);
                result.TotalCount = totalCount;
            });
        }
    }
}
