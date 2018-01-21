using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.VIP.Domain.Dtos;
using VVCar.VIP.Domain.Entities;
using VVCar.VIP.Domain.Filters;
using VVCar.VIP.Domain.Services;
using YEF.Core;
using YEF.Core.Data;
using YEF.Core.Domain;

namespace VVCar.VIP.Services.DomainServices
{
    public class MemberCardTypeService : DomainServiceBase<IRepository<MemberCardType>, MemberCardType, Guid>, IMemberCardTypeService
    {
        public MemberCardTypeService()
        {
        }

        #region methods

        protected override bool DoValidate(MemberCardType entity)
        {
            bool exists = this.Repository.Exists(t => t.Name == entity.Name && t.ID != entity.ID);
            if (exists)
                throw new DomainException(String.Format("名称 {0} 已使用，不能重复使用", entity.Name));
            return true;
        }

        public override MemberCardType Add(MemberCardType entity)
        {
            if (entity == null)
                return null;
            entity.ID = Util.NewID();
            entity.CreatedUserID = AppContext.CurrentSession.UserID;
            entity.CreatedUser = AppContext.CurrentSession.UserName;
            entity.CreatedDate = DateTime.Now;
            return base.Add(entity);
        }

        public override bool Update(MemberCardType entity)
        {
            if (entity == null)
                return false;
            var cardType = this.Repository.GetByKey(entity.ID);
            if (cardType == null)
                return false;
            entity.LastUpdateUserID = AppContext.CurrentSession.UserID;
            entity.LastUpdateUser = AppContext.CurrentSession.UserName;
            entity.LastUpdateDate = DateTime.Now;
            //回填不允许修改的数据。
            entity.CreatedUserID = cardType.CreatedUserID;
            entity.CreatedUser = cardType.CreatedUser;
            entity.CreatedDate = cardType.CreatedDate;
            return base.Update(entity);
        }

        /// <summary>
        /// 查询卡片类型
        /// </summary>
        /// <param name="filter">过滤条件</param>
        /// <param name="totalCount">总记录数</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public IEnumerable<MemberCardType> Search(MemberCardTypeFilter filter, out int totalCount)
        {
            IEnumerable<MemberCardType> pagedData = null;
            var queryable = this.Repository.GetQueryable(false);
            if (filter != null)
            {
                if (!string.IsNullOrEmpty(filter.Name))
                    queryable = queryable.Where(t => t.Name.Contains(filter.Name));
                if (filter.AllowStoreActivate.HasValue)
                    queryable = queryable.Where(t => t.AllowStoreActivate == filter.AllowStoreActivate.Value);
                if (filter.AllowDiscount.HasValue)
                    queryable = queryable.Where(t => t.AllowDiscount == filter.AllowDiscount.Value);
                if (filter.AllowRecharge.HasValue)
                    queryable = queryable.Where(t => t.AllowRecharge == filter.AllowRecharge.Value);
            }
            queryable = queryable.OrderBy(t => t.CreatedDate);
            if (filter != null && filter.Start.HasValue && filter.Limit.HasValue)
            {
                totalCount = queryable.Count();
                pagedData = queryable.Skip(filter.Start.Value).Take(filter.Limit.Value).ToArray();
            }
            else
            {
                pagedData = queryable.ToArray();
                totalCount = pagedData.Count();
            }
            return pagedData;
        }

        /// <summary>
        /// 获取可用的卡片类型
        /// </summary>
        /// <returns></returns>
        public IEnumerable<MemberCardTypeDto> GetUsableTypes()
        {
            return this.Repository.GetQueryable(false)
                .MapTo<MemberCardTypeDto>()
                .ToArray();
        }

        #endregion
    }
}
