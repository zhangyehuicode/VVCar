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
    [RoutePrefix("api/Recruitment")]
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
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonActionResult<Recruitment> Add(Recruitment entity)
        {
            return SafeExecute(() =>
            {
                return RecruitmentService.Add(entity);
            });
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPut]
        public JsonActionResult<bool> Update(Recruitment entity)
        {
            return SafeExecute(() =>
            {
                return RecruitmentService.Update(entity);
            });
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="paramer"></param>
        /// <returns></returns>
        [HttpDelete, Route("BatchDelete")]
        public JsonActionResult<bool> BatchDelete(BatchOperationDto paramer)
        {
            return SafeExecute(() =>
            {
                return RecruitmentService.BatchDelete(paramer.IdList.ToArray());
            });
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        public PagedActionResult<Recruitment> Search([FromUri]RecruitmentFilter filter)
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