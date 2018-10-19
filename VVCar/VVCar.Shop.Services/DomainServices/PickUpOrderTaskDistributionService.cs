using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.BaseData.Services;
using VVCar.Shop.Domain.Dtos;
using VVCar.Shop.Domain.Entities;
using VVCar.Shop.Domain.Filters;
using VVCar.Shop.Domain.Services;
using YEF.Core;
using YEF.Core.Data;
using YEF.Core.Domain;

namespace VVCar.Shop.Services.DomainServices
{
    /// <summary>
    /// 接车单任务分配领域服务
    /// </summary>
    public class PickUpOrderTaskDistributionService : DomainServiceBase<IRepository<PickUpOrderTaskDistribution>, PickUpOrderTaskDistribution, Guid>, IPickUpOrderTaskDistributionService
    {
        public PickUpOrderTaskDistributionService()
        {
        }

        public override PickUpOrderTaskDistribution Add(PickUpOrderTaskDistribution entity)
        {
            if (entity == null)
                return null;
            entity.ID = Util.NewID();
            entity.CreatedUserID = AppContext.CurrentSession.UserID;
            entity.CreatedUser = AppContext.CurrentSession.UserName;
            entity.CreatedDate = DateTime.Now;
            entity.MerchantID = AppContext.CurrentSession.MerchantID;
            return base.Add(entity);
        }

        public override bool Delete(Guid key)
        {
            var entity = Repository.GetByKey(key);
            if (entity == null)
                throw new DomainException("数据不存在");
            entity.IsDeleted = true;
            entity.LastUpdateUserID = AppContext.CurrentSession.UserID;
            entity.LastUpdateUser = AppContext.CurrentSession.UserName;
            entity.LastUpdateDate = DateTime.Now;
            return Repository.Update(entity) > 0;
        }

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="pickUpOrderTaskDistributions"></param>
        /// <returns></returns>
        public bool BatchAdd(IEnumerable<PickUpOrderTaskDistribution> pickUpOrderTaskDistributions)
        {
            if (pickUpOrderTaskDistributions == null || pickUpOrderTaskDistributions.Count() < 1)
                throw new DomainException("参数错误");
            var pickUpOrderItemID = pickUpOrderTaskDistributions.FirstOrDefault().PickUpOrderItemID;
            var peopleType = pickUpOrderTaskDistributions.FirstOrDefault().PeopleType;
            var userIDs = pickUpOrderTaskDistributions.Select(t => t.UserID).Distinct();
            var existData = Repository.GetQueryable(false)
                .Where(t => t.PickUpOrderItemID == pickUpOrderItemID && userIDs.Contains(t.UserID) && t.PeopleType == peopleType).ToList();
            if (existData.Count > 0)
            {
                throw new DomainException("数据已存在");
            }
            pickUpOrderTaskDistributions.ForEach(t =>
            {
                t.ID = Util.NewID();
                t.CreatedDate = DateTime.Now;
                t.CreatedUserID = AppContext.CurrentSession.UserID;
                t.CreatedUser = AppContext.CurrentSession.UserName;
                t.MerchantID = AppContext.CurrentSession.MerchantID;
            });
            return Repository.AddRange(pickUpOrderTaskDistributions).Count() > 0;
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
            var pickUpOrderItems = this.Repository.GetQueryable(false).Where(t => ids.Contains(t.ID)).ToList();
            if (pickUpOrderItems == null || pickUpOrderItems.Count() < 1)
                throw new DomainException("数据不存在");
            pickUpOrderItems.ForEach(t =>
            {
                t.IsDeleted = true;
                t.LastUpdateDate = DateTime.Now;
                t.LastUpdateUserID = AppContext.CurrentSession.UserID;
                t.LastUpdateUser = AppContext.CurrentSession.UserName;
            });
            return this.Repository.UpdateRange(pickUpOrderItems) > 0;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public IEnumerable<PickUpOrderTaskDistributionDto> Search(PickUpOrderTaskDistributionFilter filter, out int totalCount)
        {
            var queryable = Repository.GetQueryable(false).Where(t => t.PickUpOrder.MerchantID == AppContext.CurrentSession.MerchantID);
            if(filter.PickUpOrderItemID.HasValue)
                queryable = queryable.Where(t => t.PickUpOrderItemID == filter.PickUpOrderItemID.Value);
            if (filter.PeopleType.HasValue)
                queryable = queryable.Where(t => t.PeopleType == filter.PeopleType.Value);
            totalCount = queryable.Count();
            if (filter.Start.HasValue && filter.Limit.HasValue)
                queryable = queryable.OrderBy(t => t.ID).Skip(filter.Start.Value).Take(filter.Limit.Value);
            return queryable.MapTo<PickUpOrderTaskDistributionDto>().ToArray();
        }
    }
}
