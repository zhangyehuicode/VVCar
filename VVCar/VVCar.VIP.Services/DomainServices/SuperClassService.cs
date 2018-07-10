﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.VIP.Domain.Entities;
using VVCar.VIP.Domain.Filters;
using VVCar.VIP.Domain.Services;
using YEF.Core;
using YEF.Core.Data;
using YEF.Core.Domain;

namespace VVCar.VIP.Services.DomainServices
{
    /// <summary>
    /// 超能课堂领域服务
    /// </summary>
    public class SuperClassService : DomainServiceBase<IRepository<SuperClass>, SuperClass, Guid>, ISuperClassService
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override SuperClass Add(SuperClass entity)
        {
            if (entity == null)
                return null;
            entity.ID = Util.NewID();
            entity.CreatedDate = DateTime.Now;
            entity.CreatedUserID = AppContext.CurrentSession.UserID;
            entity.CreatedUser = AppContext.CurrentSession.UserName;
            entity.MerchantID = AppContext.CurrentSession.MerchantID;
            return base.Add(entity);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override bool Update(SuperClass entity)
        {
            if (entity == null)
                return false;
            var superClass = Repository.GetByKey(entity.ID);
            if (superClass == null)
                throw new DomainException("更新的视频不存在");
            superClass.Name = entity.Name;
            superClass.VideoUrl = entity.VideoUrl;
            superClass.LastUpdateDate = DateTime.Now;
            superClass.LastUpdateUser = AppContext.CurrentSession.UserName;
            superClass.LastUpdateUserID = AppContext.CurrentSession.UserID;
            return base.Update(entity);
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
            var superClassList = this.Repository.GetQueryable(true).Where(t => ids.Contains(t.ID)).ToList();
            if (superClassList == null || superClassList.Count() < 1)
                return true;
            superClassList.ForEach(t =>
            {
                t.IsDeleted = true;
            });
            return this.Repository.Update(superClassList) > 0;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public IEnumerable<SuperClass> Search(SuperClassFilter filter, out int totalCount)
        {
            var queryable = Repository.GetQueryable(false);
            if (!string.IsNullOrEmpty(filter.Name))
                queryable = queryable.Where(t => t.Name.Contains(filter.Name));
            totalCount = queryable.Count();
            if (filter.Start.HasValue && filter.Limit.HasValue)
                queryable = queryable.OrderByDescending(t => t.CreatedDate).Skip(filter.Start.Value).Take(filter.Limit.Value);
            return queryable.ToArray();
        }
    }
}