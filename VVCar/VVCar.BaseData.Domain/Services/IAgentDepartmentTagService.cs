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
    /// 门店标签关联服务接口
    /// </summary>
    public partial interface IAgentDepartmentTagService : IDomainService<IRepository<AgentDepartmentTag>, AgentDepartmentTag, Guid>
    {
        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="agentDepartmentTags"></param>
        /// <returns></returns>
        bool BatchAdd(IEnumerable<AgentDepartmentTag> agentDepartmentTags);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        bool BatchDelete(Guid[] ids);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        IEnumerable<AgentDepartmentTagDto> Search(AgentDepartmentTagFilter filter, out int totalCount);
    }
}
