using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.BaseData.Domain.Dtos;
using VVCar.BaseData.Domain.Entities;
using VVCar.BaseData.Domain.Filters;
using YEF.Core.Data;
using YEF.Core.Domain;

namespace VVCar.BaseData.Domain.Services
{
    /// <summary>
    /// 代理商门店领域服务接口
    /// </summary>
    public interface IAgentDepartmentService : IDomainService<IRepository<AgentDepartment>, AgentDepartment, Guid>
    {
        /// <summary>
        /// 审核代理商门店
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        bool ApproveAgentDepartment(Guid[] ids);

        /// <summary>
        /// 反审核代理门店
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        bool RejectAgentDepartment(Guid[] ids);

        /// <summary>
        /// 导入
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        bool ImportAgentDepartment(Guid[] ids);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        IEnumerable<AgentDepartmentDto> Search(AgentDepartmentFilter filter, out int totalCount);

        /// <summary>
        /// 新增带标签
        /// </summary>
        /// <param name="agentDepartmentDto"></param>
        /// <returns></returns>
        AgentDepartment AddWidthTag(AgentDepartmentDto agentDepartmentDto);
    }
}
