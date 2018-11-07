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
    /// 寻客侠广告浏览记录
    /// </summary>
    [RoutePrefix("api/AdvisementBrowseHistory")]
    public class AdvisementBrowseHistoryController : BaseApiController
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="advisementBrowseHistoryService"></param>
        public AdvisementBrowseHistoryController(IAdvisementBrowseHistoryService advisementBrowseHistoryService)
        {
            AdvisementBrowseHistoryService = advisementBrowseHistoryService;
        }

        IAdvisementBrowseHistoryService AdvisementBrowseHistoryService { get; set; }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost, AllowAnonymous]
        public JsonActionResult<AdvisementBrowseHistory> Add(AdvisementBrowseHistory entity)
        {
            return SafeExecute(() =>
            {
                return AdvisementBrowseHistoryService.Add(entity);
            });
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet, Route("GetBrowseAnalyse"), AllowAnonymous]
        public PagedActionResult<BrowseAnalyseDto> GetBrowseAnalyse([FromUri]BrowseAnalyseFilter filter)
        {
            return SafeGetPagedData<BrowseAnalyseDto>((result) =>
            {
                var totalCount = 0;
                var data = AdvisementBrowseHistoryService.GetBrowseAnalyse(filter, out totalCount);
                result.Data = data;
                result.TotalCount = totalCount;
            });
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet, AllowAnonymous]
        public PagedActionResult<AdvisementBrowseHistoryDto> Search([FromUri]AdvisementBrowseHistoryFilter filter)
        {
            return SafeGetPagedData<AdvisementBrowseHistoryDto>((result) =>
            {
                var totalCount = 0;
                var data = AdvisementBrowseHistoryService.Search(filter, out totalCount);
                result.Data = data;
                result.TotalCount = totalCount;
            });
        }
    }
}
