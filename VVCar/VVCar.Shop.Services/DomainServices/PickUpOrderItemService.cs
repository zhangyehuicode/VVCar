using System;
using System.Collections.Generic;
using System.Linq;
using VVCar.Shop.Domain.Entities;
using VVCar.Shop.Domain.Filters;
using VVCar.Shop.Domain.Services;
using YEF.Core;
using YEF.Core.Data;
using YEF.Core.Domain;

namespace VVCar.Shop.Services.DomainServices
{
    /// <summary>
    /// 接车单子项领域服务
    /// </summary>
    public class PickUpOrderItemService : DomainServiceBase<IRepository<PickUpOrderItem>, PickUpOrderItem, Guid>, IPickUpOrderItemService
    {
        public PickUpOrderItemService()
        {
        }

        #region properties

        IPickUpOrderService PickUpOrderService { get => ServiceLocator.Instance.GetService<IPickUpOrderService>(); }

        IRepository<PickUpOrder> PickUpOrderRepo { get => UnitOfWork.GetRepository<IRepository<PickUpOrder>>(); }

        IRepository<Product> ProductRepo { get => UnitOfWork.GetRepository<IRepository<Product>>(); }

        IRepository<PickUpOrderTaskDistribution> PickUpOrderTaskDistributionRepo { get => UnitOfWork.GetRepository<IRepository<PickUpOrderTaskDistribution>>(); }

        #endregion

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="pickUpOrderItems"></param>
        /// <returns></returns>
        public PickUpOrder BatchAdd(IEnumerable<PickUpOrderItem> pickUpOrderItems)
        {
            UnitOfWork.BeginTransaction();
            try
            {
                if (pickUpOrderItems == null || pickUpOrderItems.Count() < 1)
                    throw new DomainException("参数错误");
                var pickUpOrderID = pickUpOrderItems.FirstOrDefault().PickUpOrderID;
                var productIDs = pickUpOrderItems.Select(t => t.ProductID).Distinct();
                var pickUpOrder = PickUpOrderRepo.GetByKey(pickUpOrderID);
                var existData = Repository.GetQueryable(false)
                    .Where(t => t.PickUpOrderID == pickUpOrderID && productIDs.Contains(t.ProductID)).ToList();
                if (existData.Count > 0)
                {
                    throw new DomainException("数据已存在");
                }
                pickUpOrderItems.ForEach(t =>
                {
                    if (t.PickUpOrderID == null || t.ProductID == null)
                        throw new DomainException("参数错误");
                    t.ID = Util.NewID();
                    t.ReducedPrice = t.PriceSale;
                    if (t.IsReduce)
                    {
                        t.ReducedPrice = t.PriceSale;
                        t.Money = t.ReducedPrice * t.Quantity;
                    }
                    else
                    {
                        t.ReducedPrice = t.ReducedPrice;
                        t.Money = t.PriceSale * t.Quantity;
                    }
                    var product = ProductRepo.GetByKey(t.ProductID);
                    t.IsCommissionRate = product.IsCommissionRate;
                    t.CommissionRate = product.CommissionRate;
                    t.CommissionMoney = product.CommissionMoney;
                    t.IsSalesmanCommissionRate = product.IsSalesmanCommissionRate;
                    t.SalesmanCommissionRate = product.SalesmanCommissionRate;
                    t.SalesmanCommissionMoney = product.SalesmanCommissionMoney;
                    t.MerchantID = AppContext.CurrentSession.MerchantID;
                });
                Repository.AddRange(pickUpOrderItems).Count();
                PickUpOrderService.RecountMoneySave(pickUpOrder.Code);
                pickUpOrder = PickUpOrderRepo.GetByKey(pickUpOrderID);
                pickUpOrder.Status = Domain.Enums.EPickUpOrderStatus.UnPay;
                PickUpOrderRepo.Update(pickUpOrder);
                UnitOfWork.CommitTransaction();
                return PickUpOrderRepo.GetByKey(pickUpOrderID);
            }
            catch (Exception e)
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
        public PickUpOrder BatchDelete(Guid[] ids)
        {
            UnitOfWork.BeginTransaction();
            try
            {
                if (ids == null || ids.Length < 1)
                    throw new DomainException("参数错误");
                var pickUpOrderItems = this.Repository.GetQueryable(false).Where(t => ids.Contains(t.ID)).ToList();
                var pickUpOrderID = pickUpOrderItems.Select(t => t.PickUpOrderID).FirstOrDefault();
                var pickUpOrder = PickUpOrderRepo.GetByKey(pickUpOrderID);

                if (pickUpOrderItems == null || pickUpOrderItems.Count() < 1)
                    throw new DomainException("数据不存在");
                pickUpOrderItems.ForEach(t =>
                {
                    t.IsDeleted = true;
                    var distributionList = PickUpOrderTaskDistributionRepo.GetQueryable(true).Where(m => m.PickUpOrderID == pickUpOrderID && m.PickUpOrderItemID == t.ID).ToList();
                    distributionList.ForEach(m =>
                    {
                        m.IsDeleted = true;
                    });
                    PickUpOrderTaskDistributionRepo.UpdateRange(distributionList);
                });
                Repository.UpdateRange(pickUpOrderItems);
                PickUpOrderService.RecountMoneySave(pickUpOrder.Code);
                pickUpOrder = PickUpOrderRepo.GetByKey(pickUpOrderID);
                pickUpOrder.Status = Domain.Enums.EPickUpOrderStatus.UnPay;
                PickUpOrderRepo.Update(pickUpOrder);
                UnitOfWork.CommitTransaction();
                return PickUpOrderRepo.GetByKey(pickUpOrderID);
            }
            catch (Exception e)
            {
                UnitOfWork.RollbackTransaction();
                throw new DomainException(e.Message);
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override bool Update(PickUpOrderItem entity)
        {
            var pickUpOrderItem = Repository.GetByKey(entity.ID);
            if (pickUpOrderItem == null)
                return false;
            UnitOfWork.BeginTransaction();
            try {
                pickUpOrderItem.Remark = entity.Remark;
                pickUpOrderItem.Quantity = entity.Quantity;
                pickUpOrderItem.IsReduce = entity.IsReduce;
                if (entity.IsReduce)
                {
                    pickUpOrderItem.ReducedPrice = entity.ReducedPrice;
                    pickUpOrderItem.Money = entity.ReducedPrice * entity.Quantity;
                }
                else
                {
                    pickUpOrderItem.ReducedPrice = entity.PriceSale;
                    pickUpOrderItem.Money = entity.PriceSale * entity.Quantity;
                }              
                var pickUpOrder = PickUpOrderRepo.GetByKey(entity.PickUpOrderID);
                if (pickUpOrder.Status == Domain.Enums.EPickUpOrderStatus.Payed)
                    throw new DomainException("订单已付款");
                Repository.Update(pickUpOrderItem);
                PickUpOrderService.RecountMoneySave(pickUpOrder.Code);
                UnitOfWork.CommitTransaction();
                return true;
            }
            catch (Exception e)
            {
                UnitOfWork.RollbackTransaction();
                throw new DomainException(e.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public PickUpOrder UpdatePickUpOrder(PickUpOrderItem entity)
        {
            var pickUpOrderItem = Repository.GetByKey(entity.ID);
            if (pickUpOrderItem == null)
                return null;
            UnitOfWork.BeginTransaction();
            try
            {
                pickUpOrderItem.Remark = entity.Remark;
                pickUpOrderItem.Quantity = entity.Quantity;
                pickUpOrderItem.IsReduce = entity.IsReduce;
                if (entity.IsReduce)
                {
                    pickUpOrderItem.ReducedPrice = entity.ReducedPrice;
                    pickUpOrderItem.Money = entity.ReducedPrice * entity.Quantity;
                }
                else
                {
                    pickUpOrderItem.ReducedPrice = entity.PriceSale;
                    pickUpOrderItem.Money = entity.PriceSale * entity.Quantity;
                }
                var pickUpOrder = PickUpOrderRepo.GetByKey(entity.PickUpOrderID);
                if (pickUpOrder.Status == Domain.Enums.EPickUpOrderStatus.Payed)
                    throw new DomainException("订单已付款");
                Repository.Update(pickUpOrderItem);
                PickUpOrderService.RecountMoneySave(pickUpOrder.Code);
                UnitOfWork.CommitTransaction();
                return PickUpOrderRepo.GetByKey(entity.PickUpOrderID);
            }
            catch (Exception e)
            {
                UnitOfWork.RollbackTransaction();
                throw new DomainException(e.Message);
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public IEnumerable<PickUpOrderItem> Search(PickUpOrderItemFilter filter, out int totalCount)
        {
            var queryable = Repository.GetQueryable(false).Where(t => t.PickUpOrder.MerchantID == AppContext.CurrentSession.MerchantID);
            if (filter.PickUpOrderID.HasValue)
                queryable = queryable.Where(t => t.PickUpOrderID == filter.PickUpOrderID.Value);
            totalCount = queryable.Count();
            if (filter.Start.HasValue && filter.Limit.HasValue)
                queryable = queryable.OrderBy(t => t.ID).Skip(filter.Start.Value).Take(filter.Limit.Value);
            return queryable.ToArray();
        }
    }
}
