using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using VVCar.BaseData.Domain.Dtos;
using VVCar.BaseData.Domain.Entities;
using VVCar.BaseData.Domain.Filters;
using VVCar.BaseData.Domain.Services;
using YEF.Core;
using YEF.Core.Dtos;

namespace VVCar.Controllers.Api
{
    /// <summary>
    /// 门店标签关联
    /// </summary>
    [RoutePrefix("api/AgentDepartmentTag")]
    public class AgentDepartmentTagController : BaseApiController
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="agentDepartmentTagService"></param>
        public AgentDepartmentTagController(IAgentDepartmentTagService agentDepartmentTagService)
        {
            AgentDepartmentTagService = agentDepartmentTagService;
        }

        IAgentDepartmentTagService AgentDepartmentTagService { get; set; }

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="agentDepartmentTags"></param>
        /// <returns></returns>
        [HttpPost, Route("BatchAdd")]
        public JsonActionResult<bool> BatchAdd(IEnumerable<AgentDepartmentTag> agentDepartmentTags)
        {
            return SafeExecute(() =>
            {
                if (agentDepartmentTags == null)
                    throw new DomainException("参数错误");
                return AgentDepartmentTagService.BatchAdd(agentDepartmentTags);
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
                if (parameter == null)
                    throw new DomainException("参数错误");
                return AgentDepartmentTagService.BatchDelete(parameter.IdList.ToArray());
            });
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        public PagedActionResult<AgentDepartmentTagDto> Search([FromUri]AgentDepartmentTagFilter filter)
        {
            return SafeGetPagedData<AgentDepartmentTagDto>((result) =>
            {
                var totalCount = 0;
                var data = AgentDepartmentTagService.Search(filter, out totalCount);
                result.Data = data;
                result.TotalCount = totalCount;
            });
        }
    }
}
