using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.Shop.Domain.Entities;
using VVCar.Shop.Domain.Filters;
using VVCar.Shop.Domain.Services;
using YEF.Core;
using YEF.Core.Data;
using YEF.Core.Domain;

namespace VVCar.Shop.Services.DomainServices
{
    /// <summary>
    /// 素材服务
    /// </summary>
    public class MaterialService : DomainServiceBase<IRepository<Material>, Material, Guid>, IMaterialService
    {
        public MaterialService()
        {
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override Material Add(Material entity)
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
        /// 删除
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public override bool Delete(Guid key)
        {
            var entity = Repository.GetByKey(key);
            if (entity == null)
                throw new DomainException("数据不存在");
            entity.IsDeleted = true;
            return Repository.Update(entity) > 0;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override bool Update(Material entity)
        {
            if (entity == null)
                return false;
            var material = Repository.GetByKey(entity.ID);
            material.Name = entity.Name;
            material.Url = entity.Url;
            material.LastUpdateDate = DateTime.Now;
            material.LastUpdateUser = AppContext.CurrentSession.UserName;
            material.LastUpdateUserID = AppContext.CurrentSession.UserID;
            return base.Update(entity);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool BatchDelete(Guid[] ids)
        {
            if (ids == null || ids.Length < 1)
                throw new DomainException("参数错误");
            var materialList = Repository.GetQueryable(false).Where(t => ids.Contains(t.ID)).ToList();
            if (materialList == null || materialList.Count() < 1)
                throw new DomainException("数据不存在");
            materialList.ForEach(t =>
            {
                t.IsDeleted = true;
            });
            return Repository.UpdateRange(materialList) > 0;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public IEnumerable<Material> Search(MaterialFilter filter, out int totalCount)
        {
            var queryable = Repository.GetQueryable(false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID);
            if (!string.IsNullOrEmpty(filter.Name))
                queryable = queryable.Where(t => t.Name.Contains(filter.Name));
            totalCount = queryable.Count();
            if (filter.Start.HasValue && filter.Limit.HasValue)
                queryable = queryable.OrderByDescending(t => t.CreatedDate).Skip(filter.Start.Value).Take(filter.Limit.Value);
            return queryable.OrderByDescending(t=> t.CreatedDate).ToArray();
        }
    }
}
