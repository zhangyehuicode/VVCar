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

        #region properties
        IRepository<PickUpOrderItem> PickUpOrderItemRepo { get => UnitOfWork.GetRepository<IRepository<PickUpOrderItem>>(); }

        IRepository<PickUpOrder> PickUpOrderRepo { get => UnitOfWork.GetRepository<IRepository<PickUpOrder>>(); }
        #endregion

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override PickUpOrderTaskDistribution Add(PickUpOrderTaskDistribution entity)
        {
            if (entity == null)
                return null;
            UnitOfWork.BeginTransaction();
            try
            {
                entity.ID = Util.NewID();
                var pickUpOrderItem = PickUpOrderItemRepo.GetInclude(t => t.Product, false).Where(t => t.ID == entity.PickUpOrderItemID).FirstOrDefault();
                if (entity.PeopleType == Domain.Enums.ETaskDistributionPeopleType.ConstructionCrew)
                {
                    var distributionList = Repository.GetQueryable(true).Where(t => t.PickUpOrderID == entity.PickUpOrderID && t.PickUpOrderItemID == entity.PickUpOrderItemID && t.PeopleType == Domain.Enums.ETaskDistributionPeopleType.ConstructionCrew).ToList();
                    var constructionCount = distributionList.Count() + 1;
                    entity.CommissionRate = constructionCount != 0 ? Math.Round(pickUpOrderItem.Product.CommissionRate / constructionCount) : 0;
                    pickUpOrderItem.ConstructionCount = constructionCount;
                    distributionList.ForEach(t =>
                    {
                        t.CommissionRate = entity.CommissionRate;
                    });
                    Repository.UpdateRange(distributionList);
                }
                if (entity.PeopleType == Domain.Enums.ETaskDistributionPeopleType.Salesman)
                {
                    var distributionList = Repository.GetQueryable(true).Where(t => t.PickUpOrderID == entity.PickUpOrderID && t.PickUpOrderItemID == entity.PickUpOrderItemID && t.PeopleType == Domain.Enums.ETaskDistributionPeopleType.Salesman).ToList();
                    var salesmanCount = distributionList.Count() + 1;
                    entity.CommissionRate = salesmanCount != 0 ? pickUpOrderItem.Product.SalesmanCommissionRate / salesmanCount : 0;
                    pickUpOrderItem.SalesmanCount = salesmanCount;
                    distributionList.ForEach(t =>
                    {
                        t.CommissionRate = entity.CommissionRate;
                    });
                    Repository.UpdateRange(distributionList);
                }
                entity.CreatedUserID = AppContext.CurrentSession.UserID;
                entity.CreatedUser = AppContext.CurrentSession.UserName;
                entity.CreatedDate = DateTime.Now;
                entity.MerchantID = AppContext.CurrentSession.MerchantID;
                var result = Repository.Add(entity);
                PickUpOrderItemRepo.Update(pickUpOrderItem);
                var pickUpOrder = PickUpOrderRepo.GetByKey(entity.PickUpOrderID);
                ReCountCommission(pickUpOrder.ID);
                UnitOfWork.CommitTransaction();
                return result;
            }
            catch (Exception e)
            {
                UnitOfWork.RollbackTransaction();
                throw new DomainException("新增人员失败: " + e.Message);
            }

        }

        void ReCountCommission(Guid pickUpOrderID)
        {
            var pickUpOrder = PickUpOrderRepo.GetByKey(pickUpOrderID);
            var pickUpOrderTaskDistributionList = Repository.GetQueryable(true).Where(m => m.PickUpOrderID == pickUpOrder.ID).ToList();
            pickUpOrderTaskDistributionList.ForEach(m =>
            {
                var pickUpOrderItem = PickUpOrderItemRepo.GetByKey(m.PickUpOrderItemID);
                if (m.PeopleType == Domain.Enums.ETaskDistributionPeopleType.ConstructionCrew)
                {
                    m.TotalMoney = pickUpOrderItem.Money;
                    m.CommissionRate = pickUpOrderItem.ConstructionCount != 0 ? Math.Round(pickUpOrderItem.CommissionRate / pickUpOrderItem.ConstructionCount, 2) : 0;
                    m.Commission = Math.Round(m.TotalMoney * m.CommissionRate / 100, 2);
                }
                if (m.PeopleType == Domain.Enums.ETaskDistributionPeopleType.Salesman)
                {
                    m.TotalMoney = pickUpOrderItem.Money;
                    m.SalesmanCommissionRate = pickUpOrderItem.SalesmanCount != 0 ? Math.Round(pickUpOrderItem.SalesmanCommissionRate / pickUpOrderItem.SalesmanCount, 2) : 0;
                    m.SalesmanCommission = Math.Round(m.TotalMoney * m.SalesmanCommissionRate / 100, 2);
                }
            });
            Repository.UpdateRange(pickUpOrderTaskDistributionList);
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
            ReCountCommission(entity.PickUpOrderID);
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
            UnitOfWork.BeginTransaction();
            try {
                var pickUpOrderItemID = pickUpOrderTaskDistributions.FirstOrDefault().PickUpOrderItemID;
                var pickUpOrderItem = PickUpOrderItemRepo.GetByKey(pickUpOrderItemID);
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
                    if(t.PeopleType == Domain.Enums.ETaskDistributionPeopleType.ConstructionCrew)
                    {
                        pickUpOrderItem.ConstructionCount += 1;
                    }
                    if(t.PeopleType == Domain.Enums.ETaskDistributionPeopleType.Salesman)
                    {
                        pickUpOrderItem.SalesmanCount += 1;
                    }
                    t.CreatedDate = DateTime.Now;
                    t.CreatedUserID = AppContext.CurrentSession.UserID;
                    t.CreatedUser = AppContext.CurrentSession.UserName;
                    t.MerchantID = AppContext.CurrentSession.MerchantID;
                });
                PickUpOrderItemRepo.Update(pickUpOrderItem);         
                Repository.AddRange(pickUpOrderTaskDistributions);
                ReCountCommission(pickUpOrderItem.PickUpOrderID);
                UnitOfWork.CommitTransaction();
                return true;

            }catch(Exception e)
            {
                UnitOfWork.RollbackTransaction();
                throw new DomainException(e.Message);
            }
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
            var pickUpOrderID = pickUpOrderItems.Select(t => t.PickUpOrderID).FirstOrDefault();
            if (pickUpOrderItems == null || pickUpOrderItems.Count() < 1)
                throw new DomainException("数据不存在");
            pickUpOrderItems.ForEach(t =>
            {
                t.IsDeleted = true;
                t.LastUpdateDate = DateTime.Now;
                t.LastUpdateUserID = AppContext.CurrentSession.UserID;
                t.LastUpdateUser = AppContext.CurrentSession.UserName;
            });
            ReCountCommission(pickUpOrderID);
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
            if (filter.PickUpOrderItemID.HasValue)
                queryable = queryable.Where(t => t.PickUpOrderItemID == filter.PickUpOrderItemID.Value);
            if (filter.PeopleType.HasValue)
                queryable = queryable.Where(t => t.PeopleType == filter.PeopleType.Value);
            totalCount = queryable.Count();
            if (filter.Start.HasValue && filter.Limit.HasValue)
                queryable = queryable.OrderBy(t => t.ID).Skip(filter.Start.Value).Take(filter.Limit.Value);
            var result = queryable.MapTo<PickUpOrderTaskDistributionDto>().ToList();
            result.ForEach(t =>
            {
                var distribution = Repository.GetByKey(t.ID);
                if(t.PeopleType == Domain.Enums.ETaskDistributionPeopleType.ConstructionCrew)
                {
                    t.CommissionRate = distribution.CommissionRate;
                }
                else
                {
                    t.CommissionRate = distribution.SalesmanCommissionRate;
                }
            });
            return result;
        }
    }
}
