using System;
using System.Collections.Generic;
using System.Linq;
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
    /// 门店标签(客户标签)领域服务
    /// </summary>
    public class TagService : DomainServiceBase<IRepository<Tag>, Tag, Guid>, ITagService
    {
        /// <summary>
        /// ctor
        /// </summary>
        public TagService()
        {
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override Tag Add(Tag entity)
        {
            if (entity == null)
                return null;
            entity.ID = Util.NewID();
            entity.MerchantID = AppContext.CurrentSession.MerchantID;
            entity.CreatedUserID = AppContext.CurrentSession.UserID;
            entity.CreatedUser = AppContext.CurrentSession.UserName;
            entity.CreatedDate = DateTime.Now;
            return base.Add(entity);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool BatchDelete(Guid[] ids)
        {
            if (ids == null || ids.Length < 1)
                throw new DomainException("参数错误");
            var agentDepartmentTagList = Repository.GetQueryable(false).Where(t => ids.Contains(t.ID)).ToList();
            if (agentDepartmentTagList == null || agentDepartmentTagList.Count() < 1)
                throw new DomainException("数据不存在");
            agentDepartmentTagList.ForEach(t =>
            {
                t.IsDeleted = true;
                t.LastUpdatedDate = DateTime.Now;
                t.LastUpdatedUserID = AppContext.CurrentSession.UserID;
                t.LastUpdatedUser = AppContext.CurrentSession.UserName;
            });
            return Repository.Update(agentDepartmentTagList) > 0;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override bool Update(Tag entity)
        {
            if (entity == null)
                return false;
            var agentDepartmentTag = Repository.GetByKey(entity.ID);
            if (agentDepartmentTag == null)
                return false;
            agentDepartmentTag.Name = entity.Name;
            agentDepartmentTag.Code = entity.Code;
            agentDepartmentTag.LastUpdatedDate = DateTime.Now;
            agentDepartmentTag.LastUpdatedUser = AppContext.CurrentSession.UserName;
            agentDepartmentTag.LastUpdatedUserID = AppContext.CurrentSession.UserID;
            return base.Update(agentDepartmentTag);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public IEnumerable<TagDto> Search(TagFilter filter, out int totalCount)
        {
            var queryable = Repository.GetQueryable(false);
            if (!(AppContext.CurrentSession.MerchantID == Guid.Parse("00000000-0000-0000-0000-000000000001")))
            {
                queryable = queryable.Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID);
            }
            if (!string.IsNullOrEmpty(filter.Code))
                queryable = queryable.Where(t => t.Code.Contains(filter.Code));
            if (!string.IsNullOrEmpty(filter.Name))
                queryable = queryable.Where(t => t.Name.Contains(filter.Name));
            totalCount = queryable.Count();
            if (filter.Start.HasValue && filter.Limit.HasValue)
                queryable = queryable.OrderByDescending(t => t.CreatedDate).Skip(filter.Start.Value).Take(filter.Limit.Value);
            return queryable.MapTo<TagDto>().ToArray();
        }
    }
}
