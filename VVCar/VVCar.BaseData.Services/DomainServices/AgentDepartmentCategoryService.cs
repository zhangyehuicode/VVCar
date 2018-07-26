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
    /// 门店分类领域服务
    /// </summary>
    public class AgentDepartmentCategoryService : DomainServiceBase<IRepository<AgentDepartmentCategory>, AgentDepartmentCategory, Guid>, IAgentDepartmentCategoryService
    {
        /// <summary>
        /// ctor
        /// </summary>
        public AgentDepartmentCategoryService()
        {
        }

        #region properties

        private IRepository<AgentDepartment> _agentDepartmentRepo;

        public IRepository<AgentDepartment> AgentDepartmentRepo
        {
            get
            {
                if (_agentDepartmentRepo == null)
                {
                    _agentDepartmentRepo = UnitOfWork.GetRepository<IRepository<AgentDepartment>>();
                }
                return _agentDepartmentRepo;
            }
        }

        #endregion

        #region methods

        /// <summary>
        /// 校验
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected override bool DoValidate(AgentDepartmentCategory entity)
        {
            var exists = Repository.Exists(t => t.Code == entity.Code && t.ID != entity.ID && t.MerchantID == AppContext.CurrentSession.MerchantID);
            if (exists)
                throw new DomainException(string.Format("代码 {0} 已使用", entity.Code));
            if (entity.ID == entity.ParentId)
                throw new DomainException("不能选择本类为自己的上级分类");
            return true;
        }

        /// <summary>
        /// 添加门店分类
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override AgentDepartmentCategory Add(AgentDepartmentCategory entity)
        {
            if (entity == null)
                return null;
            entity.ID = Util.NewID();
            entity.CreatedUser = AppContext.CurrentSession.UserName;
            entity.CreatedUserID = AppContext.CurrentSession.UserID;
            entity.CreatedUser = AppContext.CurrentSession.UserName;
            entity.MerchantID = AppContext.CurrentSession.MerchantID;
            entity.CreatedDate = DateTime.Now;
            return base.Add(entity);
        }

        /// <summary>
        /// 删除门店分类
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public override bool Delete(Guid key)
        {
            if (AgentDepartmentRepo.Exists(t => t.AgentDepartmentCategoryID == key))
                throw new DomainException("该类别存在明细，无法删除！");
            if (Repository.Exists(t => t.ParentId == key))
                throw new DomainException("该分类存在下级分类，无法删除！");
            var agentDepartmentCategory = Repository.GetByKey(key);
            if (agentDepartmentCategory == null)
                throw new DomainException("数据不存在！");
            agentDepartmentCategory.IsDeleted = true;
            agentDepartmentCategory.LastUpdateDate = DateTime.Now;
            agentDepartmentCategory.LastUpdateUser = AppContext.CurrentSession.UserName;
            agentDepartmentCategory.LastUpdateUserID = AppContext.CurrentSession.UserID;
            return Repository.Update(agentDepartmentCategory) > 0;
        }

        /// <summary>
        /// 更新门店分类
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override bool Update(AgentDepartmentCategory entity)
        {
            if (entity == null)
                return false;
            var agentDepartmentCategory = Repository.GetByKey(entity.ID);
            if (agentDepartmentCategory == null)
                return false;
            agentDepartmentCategory.ParentId = entity.ParentId;
            agentDepartmentCategory.Code = entity.Code;
            agentDepartmentCategory.Name = entity.Name;
            agentDepartmentCategory.Index = entity.Index;
            agentDepartmentCategory.LastUpdateDate = DateTime.Now;
            agentDepartmentCategory.LastUpdateUser = AppContext.CurrentSession.UserName;
            agentDepartmentCategory.LastUpdateUserID = AppContext.CurrentSession.UserID;
            return base.Update(entity);
        }

        /// <summary>
        /// 获取树形数据
        /// </summary>
        /// <param name="parentID"></param>
        /// <returns></returns>
        public IEnumerable<AgentDepartmentCategoryTreeDto> GetTreeData(Guid? parentID)
        {
            var agentDepartmentRegions = Repository.GetQueryable(false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID)
                .MapTo<AgentDepartmentCategory, AgentDepartmentCategoryTreeDto>()
                .OrderBy(t => t.ParentId).ThenBy(t => t.Index)
                .ToList();
            return BuildTree(agentDepartmentRegions, null);
        }

        IEnumerable<AgentDepartmentCategoryTreeDto> BuildTree(IList<AgentDepartmentCategoryTreeDto> sources, Guid? parentID)
        {
            var children = sources.Where(t => t.ParentId == parentID).ToList();
            foreach (var child in children)
            {
                child.Children = BuildTree(sources, child.ID);
                child.leaf = child.Children.Count() == 0;
                child.expanded = false;
            }
            return children;
        }

        /// <summary>
        /// 根据条件查询门店分类
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public IEnumerable<AgentDepartmentCategory> Search(AgentDepartmentCategoryFilter filter, out int totalCount)
        {
            var result = new List<AgentDepartmentCategory>();
            var queryable = Repository.GetInclude(t => t.SubAgentDepartments, false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID);
            if (filter != null)
            {
                if (!string.IsNullOrEmpty(filter.Code))
                    queryable = queryable.Where(t => t.Code.Contains(filter.Code));
                if (!string.IsNullOrEmpty(filter.Name))
                    queryable = queryable.Where(t => t.Name.Contains(filter.Name));
                if (!string.IsNullOrEmpty(filter.NameOrCode))
                    queryable = queryable.Where(t => t.Code.Contains(filter.NameOrCode) || t.Name.Contains(filter.NameOrCode));
            }
            queryable = queryable.OrderBy(t => t.ParentId).ThenBy(t => t.Index);
            totalCount = queryable.Count();
            if (filter != null && filter.Start.HasValue && filter.Limit.HasValue)
                queryable = queryable.Skip(filter.Start.Value).Take(filter.Limit.Value);
            result = queryable.ToList();
            return result;
        }

        /// <summary>
        /// 获取精简结构数据
        /// </summary>
        /// <returns></returns>
        public IList<IDCodeNameDto> GetLiteData()
        {
            var categories = Repository.GetQueryable(false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID)
                .OrderBy(t => t.Index)
                .Select(t => new IDCodeNameDto
                {
                    ID = t.ID,
                    Code = t.Code,
                    Name = t.Name
                }).ToList();
            return categories;
        }

        #endregion

    }
}
