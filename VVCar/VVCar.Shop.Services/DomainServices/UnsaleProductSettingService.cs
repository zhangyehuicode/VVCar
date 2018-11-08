using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.BaseData.Services;
using VVCar.Shop.Domain.Dtos;
using VVCar.Shop.Domain.Entities;
using VVCar.Shop.Domain.Enums;
using VVCar.Shop.Domain.Filters;
using VVCar.Shop.Domain.Services;
using YEF.Core;
using YEF.Core.Data;
using YEF.Core.Domain;

namespace VVCar.Shop.Services.DomainServices
{
    /// <summary>
    /// 滞销产品参数设置领域服务
    /// </summary>
    public class UnsaleProductSettingService : DomainServiceBase<IRepository<UnsaleProductSetting>, UnsaleProductSetting, Guid>, IUnsaleProductSettingService
    {
        /// <summary>
        /// ctor
        /// </summary>
        public UnsaleProductSettingService()
        {
        }

        #region properties

        IRepository<UnsaleProductSettingItem> UnsaleProductSettingItemRepo { get => UnitOfWork.GetRepository<IRepository<UnsaleProductSettingItem>>(); }

        IRepository<Merchant> MerchantRepo { get => UnitOfWork.GetRepository<IRepository<Merchant>>(); }

        IRepository<UnsaleProductHistory> UnsaleProductHistoryRepo { get => UnitOfWork.GetRepository<IRepository<UnsaleProductHistory>>(); }

        IRepository<Product> ProductRepo { get => UnitOfWork.GetRepository<IRepository<Product>>(); }

        IRepository<OrderDividend> OrderDividendRepo { get => UnitOfWork.GetRepository<IRepository<OrderDividend>>(); }

        IRepository<OrderItem> OrderItemRepo { get => UnitOfWork.GetRepository<IRepository<OrderItem>>(); }

        IRepository<PickUpOrderItem> PickUpOrderItemRepo { get => UnitOfWork.GetRepository<IRepository<PickUpOrderItem>>(); }

        #endregion

        protected override bool DoValidate(UnsaleProductSetting entity)
        {
            bool exists = this.Repository.Exists(t => t.Code == entity.Code && t.ID != entity.ID && t.MerchantID == AppContext.CurrentSession.MerchantID);
            if (exists)
                throw new DomainException(string.Format("代码{0}已经使用,不能重复添加.", entity.Code));
            return true;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override UnsaleProductSetting Add(UnsaleProductSetting entity)
        {
            if (entity == null)
                return null;
            if (entity.UnsaleQuantity >= entity.SaleWellQuantity)
                throw new DomainException("参数错误, 滞销上限必须小于畅销下限!");
            entity.ID = Util.NewID();
            entity.CreatedDate = DateTime.Now;
            entity.CreatedUserID = AppContext.CurrentSession.UserID;
            entity.CreatedUser = AppContext.CurrentSession.UserName;
            entity.MerchantID = AppContext.CurrentSession.MerchantID;
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
            var unsaleProductSettingList = this.Repository.GetQueryable(true)
                .Where(t => ids.Contains(t.ID)).ToList();
            if (unsaleProductSettingList == null || unsaleProductSettingList.Count() < 1)
                throw new DomainException("数据不存在!");
            unsaleProductSettingList.ForEach(t =>
            {
                if (t.IsAvailable)
                    throw new DomainException("请选择未启用的数据进行删除");
                var unsaleProductSettingItemList = UnsaleProductSettingItemRepo.GetQueryable(true).Where(m => m.UnsaleProductSettingID == t.ID).ToList();
                unsaleProductSettingItemList.ForEach(m =>
                {
                    m.IsDeleted = true;
                });
                UnsaleProductSettingItemRepo.UpdateRange(unsaleProductSettingItemList);
                t.IsDeleted = true;
                t.LastUpdateDate = DateTime.Now;
                t.LastUpdateUserID = AppContext.CurrentSession.UserID;
                t.LastUpdateUser = AppContext.CurrentSession.UserName;
            });
            return Repository.UpdateRange(unsaleProductSettingList) > 0;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override bool Update(UnsaleProductSetting entity)
        {
            if (entity == null)
                return false;
            if (entity.UnsaleQuantity >= entity.SaleWellQuantity)
                throw new DomainException("参数错误, 滞销上限必须小于畅销下限!");
            var unsaleProductSetting = Repository.GetByKey(entity.ID);
            if (unsaleProductSetting == null)
                return false;
            unsaleProductSetting.Code = entity.Code;
            unsaleProductSetting.Name = entity.Name;
            unsaleProductSetting.UnsaleQuantity = entity.UnsaleQuantity;
            unsaleProductSetting.SaleWellQuantity = entity.SaleWellQuantity;
            unsaleProductSetting.IsAvailable = entity.IsAvailable;
            unsaleProductSetting.LastUpdateDate = DateTime.Now;
            unsaleProductSetting.LastUpdateUserID = AppContext.CurrentSession.UserID;
            unsaleProductSetting.LastUpdateUser = AppContext.CurrentSession.UserName;
            return base.Update(unsaleProductSetting);
        }

        /// <summary>
        /// 畅销/滞销产品报表
        /// </summary>
        /// <returns></returns>
        public bool RecordUnsaleHistoryData()
        {
            try
            {
                var merchantList = MerchantRepo.GetQueryable(false).ToList();
                var result = new List<UnsaleProductHistory>();
                var now = DateTime.Now.AddMonths(-1);
                var monthStartTime = new DateTime(now.Year, now.Month, 1);
                var startDate = monthStartTime;
                var days = DateTime.DaysInMonth(now.Year, now.Month);
                var endDate = new DateTime(now.Year, now.Month, days, 23, 59, 59);
                var orderDividendList = OrderDividendRepo.GetQueryable(false).Where(t => t.CreatedDate >= startDate && t.CreatedDate < endDate).ToList();
                var orderItemList = OrderItemRepo.GetQueryable(false).ToList();
                var unsaleProductSettingList = this.Repository.GetInclude(u => u.unsaleProductSettingItemList, false).Where(t =>t.IsAvailable == true).ToList();
                var pickUpOrderIteeList = PickUpOrderItemRepo.GetQueryable(false).ToList();
                merchantList.ForEach(t =>
                {
                    var unsaleProductSettings = unsaleProductSettingList.Where(u => u.MerchantID == t.ID).ToList();
                    var orderDividends = orderDividendList.Where(devidend => devidend.MerchantID == t.ID);
                    var TradeOrderIDs = orderDividends.Select(dividend => dividend.TradeOrderID).Distinct();
                    var orderItems = orderItemList.Where(item => TradeOrderIDs.Contains(item.OrderID)).ToList();
                    var pickUpOrderItems = pickUpOrderIteeList.Where(item => TradeOrderIDs.Contains(item.PickUpOrderID)).ToList();
                    unsaleProductSettings.ForEach(unsaleProductSetting =>
                    {
                        unsaleProductSetting.unsaleProductSettingItemList.ForEach(item =>
                        {
                            if(item.IsDeleted == false)
                            {
                                var unsaleProductHistory = new UnsaleProductHistory();
                                var product = ProductRepo.GetByKey(item.ProductID);
                                unsaleProductHistory.ID = Util.NewID();
                                unsaleProductHistory.StartDate = startDate;
                                unsaleProductHistory.EndDate = endDate;
                                unsaleProductHistory.ProductID = item.ProductID;
                                unsaleProductHistory.Code = now.Year.ToString() + (now.Month > 9 ? now.Month.ToString() : "0" + now.Month.ToString());
                                unsaleProductHistory.ProductType = product.ProductType;
                                unsaleProductHistory.ProductCode = product.Code;
                                unsaleProductHistory.ProductName = product.Name;
                                unsaleProductHistory.UnsaleQuantity = unsaleProductSetting.UnsaleQuantity;
                                unsaleProductHistory.SaleWellQuantity = unsaleProductSetting.SaleWellQuantity;
                                var orderItemQuantity = orderItems.Where(c=> c.GoodsID == item.ProductID).GroupBy(g => 1).Select(sl => sl.Sum(s => s.Quantity)).FirstOrDefault();
                                var pickOrderItemQuantity = pickUpOrderItems.Where(c=>c.ProductID == item.ProductID).GroupBy(g => 1).Select(sl => sl.Sum(s => s.Quantity)).FirstOrDefault();
                                unsaleProductHistory.Quantity = orderItemQuantity + pickOrderItemQuantity;
                                if (unsaleProductHistory.Quantity >= unsaleProductHistory.SaleWellQuantity)
                                    unsaleProductHistory.Status = EUnsaleProductStatus.SaleWell;
                                else if (unsaleProductHistory.Quantity < unsaleProductHistory.UnsaleQuantity)
                                    unsaleProductHistory.Status = EUnsaleProductStatus.Unsale;
                                else
                                    unsaleProductHistory.Status = EUnsaleProductStatus.Normal;
                                unsaleProductHistory.CreatedDate = DateTime.Now;
                                unsaleProductHistory.MerchantID = t.ID;
                                result.Add(unsaleProductHistory);
                            }
                        });
                    });
                });
                return UnsaleProductHistoryRepo.AddRange(result).Count() > 0;
            }catch(Exception e)
            {
                AppContext.Logger.Error("畅销/滞销产品记录失败:" + e.Message);
                return false;
            }
            
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public IEnumerable<UnsaleProductSettingDto> Search(UnsaleProductSettingFilter filter, out int totalCount)
        {
            var queryable = this.Repository.GetQueryable(false).Where(t=> t.MerchantID == AppContext.CurrentSession.MerchantID);
            if (!string.IsNullOrEmpty(filter.Keyword))
                queryable = queryable.Where(t => t.Code.Contains(filter.Keyword) || t.Name.Contains(filter.Keyword));
            totalCount = queryable.Count();
            if (filter.Start.HasValue && filter.Limit.HasValue)
                queryable = queryable.OrderByDescending(t => t.CreatedDate).Skip(filter.Start.Value).Take(filter.Limit.Value);
            return queryable.MapTo<UnsaleProductSettingDto>().ToArray();
        }
    }
}
