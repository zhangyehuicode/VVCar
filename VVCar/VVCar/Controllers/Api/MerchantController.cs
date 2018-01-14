using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VVCar.BaseData.Domain.Filters;
using VVCar.BaseData.Domain.Services;
using YEF.Core.Data;
using YEF.Core.Dtos;

namespace VVCar.Controllers.Api
{
    /// <summary>
    ///  商户
    /// </summary>
    [RoutePrefix("api/Merchant")]
    public class MerchantController : BaseApiController
    {
        public MerchantController(IMerchantService merchantService)
        {
            MerchantService = merchantService;
        }

        IMerchantService MerchantService { get; set; }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonActionResult<Merchant> Add(Merchant entity)
        {
            return SafeExecute(() =>
            {
                return MerchantService.Add(entity);
            });
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        public PagedActionResult<Merchant> Search([FromUri]MerchantFilter filter)
        {
            return SafeGetPagedData<Merchant>((result) =>
            {
                var totalCount = 0;
                var data = MerchantService.Search(filter, out totalCount);
                result.Data = data;
                result.TotalCount = totalCount;
            });
        }
    }
}
