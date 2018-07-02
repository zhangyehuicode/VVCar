using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using VVCar.VIP.Domain.Entities;
using VVCar.VIP.Domain.Filters;
using VVCar.VIP.Domain.Services;
using YEF.Core.Dtos;

namespace VVCar.Controllers.VIP
{
    /// <summary>
    /// 人才招聘
    /// </summary>
    [RoutePrefix("Recruitment")]
    public class RecruitmentController : BaseApiController
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="recruitmentService"></param>
        public RecruitmentController(IRecruitmentService recruitmentService)
        {
            RecruitmentService = recruitmentService;
        }

        IRecruitmentService RecruitmentService { get; set; }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        public PagedActionResult<Recruitment> Search(RecruitmentFilter filter)
        {
            return SafeGetPagedData<Recruitment>((result) =>
            {
                var totalCount = 0;
                var data = RecruitmentService.Search(filter, out totalCount);
                result.Data = data;
                result.TotalCount = totalCount;
            });
        }
    }
}