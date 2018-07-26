using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.BaseData.Domain.Dtos;
using VVCar.BaseData.Domain.Entities;
using VVCar.BaseData.Domain.Filters;
using VVCar.BaseData.Domain.Services;
using YEF.Core;
using YEF.Core.Data;
using YEF.Core.Domain;

namespace VVCar.BaseData.Services.DomainServices
{
    /// <summary>
    /// 门店标签关联领域服务
    /// </summary>
    public class AgentDepartmentTagService : DomainServiceBase<IRepository<AgentDepartmentTag>, AgentDepartmentTag, Guid>, IAgentDepartmentTagService
    {
        /// <summary>
        /// ctor
        /// </summary>
        public AgentDepartmentTagService()
        {
        }

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="agentDepartmentTags"></param>
        /// <returns></returns>
        public bool BatchAdd(IEnumerable<AgentDepartmentTag> agentDepartmentTags)
        {
            if (agentDepartmentTags == null || agentDepartmentTags.Count() < 1)
                throw new DomainException("新增失败,没有数据");
            var agentDepartmentTagList = agentDepartmentTags.ToList();
            var agentDepartmentId = agentDepartmentTagList.FirstOrDefault().AgentDepartmentID;
            var tagIds = agentDepartmentTagList.Select(t => t.TagID).Distinct();
            var existData = Repository.GetQueryable(false)
                .Where(t => agentDepartmentId == t.AgentDepartmentID && tagIds.Contains(t.TagID))
                .Select(t => t.TagID).ToList();
            if (existData.Count > 0)
                agentDepartmentTagList.RemoveAll(t => existData.Contains(t.TagID));
            if (agentDepartmentTagList.Count < 1)
                return true;
            foreach (var agentDepartmentTag in agentDepartmentTagList)
            {
                agentDepartmentTag.ID = Util.NewID();
                agentDepartmentTag.CreatedDate = DateTime.Now;
                agentDepartmentTag.CreatedUserID = AppContext.CurrentSession.UserID;
                agentDepartmentTag.CreatedUser = AppContext.CurrentSession.UserName;
                agentDepartmentTag.MerchantID = AppContext.CurrentSession.MerchantID;
            }
            return Repository.AddRange(agentDepartmentTagList).Count() > 0;
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool BatchDelete(Guid[] ids)
        {
            if (ids == null || ids.Count() < 1)
                throw new DomainException("参数错误");
            var agentDepartmentTagList = Repository.GetQueryable(false).Where(t => ids.Contains(t.ID)).ToList();
            if (agentDepartmentTagList == null || agentDepartmentTagList.Count() < 1)
                throw new DomainException("数据不存在");
            return Repository.DeleteRange(agentDepartmentTagList) > 0;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public IEnumerable<AgentDepartmentTagDto> Search(AgentDepartmentTagFilter filter, out int totalCount)
        {
            var queryable = Repository.GetInclude(t => t.Tag, false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID);
            if (filter.AgentDepartmentID.HasValue)
                queryable = queryable.Where(t => t.AgentDepartmentID == filter.AgentDepartmentID);
            if (filter.TagID.HasValue)
                queryable = queryable.Where(t => t.TagID == filter.TagID);
            totalCount = queryable.Count();
            if (filter.Start.HasValue && filter.Limit.HasValue)
                queryable = queryable.OrderByDescending(t => t.CreatedDate).Skip(filter.Start.Value).Take(filter.Limit.Value);
            return queryable.MapTo<AgentDepartmentTagDto>().ToList();
        }
    }
}
