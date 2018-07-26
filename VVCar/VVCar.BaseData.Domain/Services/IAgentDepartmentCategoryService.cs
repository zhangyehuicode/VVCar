using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.BaseData.Domain.Dtos;
using VVCar.BaseData.Domain.Entities;
using VVCar.BaseData.Domain.Filters;
using YEF.Core;
using YEF.Core.Data;
using YEF.Core.Domain;

namespace VVCar.BaseData.Domain.Services
{
    /// <summary>
    /// 门店分类领域服务接口
    /// </summary>
    public interface IAgentDepartmentCategoryService : IDomainService<IRepository<AgentDepartmentCategory>, AgentDepartmentCategory, Guid>
    {
        /// <summary>
        /// 获取树形数据
        /// </summary>
        /// <param name="parentID"></param>
        /// <returns></returns>
        IEnumerable<AgentDepartmentCategoryTreeDto> GetTreeData(Guid? parentID);

        /// <summary>
        /// 根据条件查询门店分类
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        IEnumerable<AgentDepartmentCategory> Search(AgentDepartmentCategoryFilter filter, out int totalCount);

        /// <summary>
        /// 获取精简结构数据类型
        /// </summary>
        /// <returns></returns>
        IList<IDCodeNameDto> GetLiteData();
    }
}
