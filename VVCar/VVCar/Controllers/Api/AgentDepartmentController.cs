using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VVCar.BaseData.Domain.Dtos;
using VVCar.BaseData.Domain.Entities;
using VVCar.BaseData.Domain.Filters;
using VVCar.BaseData.Domain.Services;
using YEF.Core.Dtos;

namespace VVCar.Controllers.Api
{
    /// <summary>
    /// 代理商门店
    /// </summary>
    [RoutePrefix("api/AgentDepartment")]
    public class AgentDepartmentController : BaseApiController
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="agentDepartmentService"></param>
        public AgentDepartmentController(IAgentDepartmentService agentDepartmentService)
        {
            AgentDepartmentService = agentDepartmentService;
        }

        IAgentDepartmentService AgentDepartmentService { get; set; }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonActionResult<AgentDepartment> Add(AgentDepartment entity)
        {
            return SafeExecute(() =>
            {
                return AgentDepartmentService.Add(entity);
            });
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public JsonActionResult<bool> Delete(Guid id)
        {
            return SafeExecute(() =>
            {
                return AgentDepartmentService.Delete(id);
            });
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPut]
        public JsonActionResult<bool> Update(AgentDepartment entity)
        {
            return SafeExecute(() =>
            {
                return AgentDepartmentService.Update(entity);
            });
        }

        /// <summary>
        /// 审核代理商门店
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        [HttpPost, Route("ApproveAgentDepartment")]
        public JsonActionResult<bool> ApproveAgentDepartment(BatchOperationDto parameter)
        {
            return SafeExecute(() =>
            {
                return AgentDepartmentService.ApproveAgentDepartment(parameter.IdList.ToArray());
            });
        }

        /// <summary>
        /// 反审核代理商门店
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        [HttpPost, Route("RejectAgentDepartment")]
        public JsonActionResult<bool> RejectAgentDepartment(BatchOperationDto parameter)
        {
            return SafeExecute(() =>
            {
                return AgentDepartmentService.RejectAgentDepartment(parameter.IdList.ToArray());
            });
        }

        /// <summary>
        /// 导入
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        [HttpPost, Route("ImportAgentDepartment")]
        public JsonActionResult<bool> ImportAgentDepartment(BatchOperationDto parameter)
        {
            return SafeExecute(() =>
            {
                return AgentDepartmentService.ImportAgentDepartment(parameter.IdList.ToArray());
            });
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        public PagedActionResult<AgentDepartmentDto> Search([FromUri]AgentDepartmentFilter filter)
        {
            return SafeGetPagedData<AgentDepartmentDto>((result) =>
            {
                var totalCount = 0;
                var data = AgentDepartmentService.Search(filter, out totalCount);
                result.Data = data;
                result.TotalCount = totalCount;
            });
        }
    }
}
