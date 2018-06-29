using System;
using System.Collections.Generic;
using System.Linq;
using VVCar.VIP.Domain.Dtos;
using VVCar.VIP.Domain.Entities;
using VVCar.VIP.Domain.Filters;
using VVCar.VIP.Domain.Services;
using YEF.Core;
using YEF.Core.Data;
using YEF.Core.Domain;

namespace VVCar.VIP.Services.DomainServices
{
    /// <summary>
    /// 卡轩推送子项服务
    /// </summary>
    public class CouponPushItemService : DomainServiceBase<IRepository<CouponPushItem>, CouponPushItem, Guid>, ICouponPushItemService
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public CouponPushItemService()
        {
        }

        #region properties

        IRepository<CouponPush> CouponPushRepo { get => UnitOfWork.GetRepository<IRepository<CouponPush>>(); }

        #endregion

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override CouponPushItem Add(CouponPushItem entity)
        {
            if (entity == null)
                return null;
            entity.ID = Util.NewID();
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
            var couponPushItem = this.Repository.GetByKey(key);
            if (couponPushItem == null)
                throw new DomainException("删除失败, 数据不存在");
            couponPushItem.IsDeleted = true;
            return this.Repository.Update(couponPushItem) > 0;
        }

        /// <summary>
        /// 批量新增卡券推送子项
        /// </summary>
        /// <param name="couponPushItems"></param>
        /// <returns></returns>
        public bool BatchAdd(IEnumerable<CouponPushItem> couponPushItems)
        {
            if (couponPushItems == null || couponPushItems.Count() < 1)
                throw new DomainException("没有数据");
            var couponPushItemList = couponPushItems.ToList();
            var couponPushID = couponPushItemList.FirstOrDefault().CouponPushID;
            var couponPush = CouponPushRepo.GetByKey(couponPushID);
            if (couponPush.Status != Domain.Enums.ECouponPushStatus.NotPush)
            {
                throw new DomainException("请选择未推送的数据!");
            }
            var couponTemplateIDs = couponPushItemList.Select(t => t.CouponTemplateID).Distinct();
            var existData = this.Repository.GetQueryable(false)
                .Where(t => t.CouponPushID == couponPushID && couponTemplateIDs.Contains(t.CouponTemplateID))
                .Select(t => t.CouponTemplateID).ToList();
            if (existData.Count > 0)
            {
                couponPushItemList.RemoveAll(t => existData.Contains(t.CouponTemplateID));
            }
            if (couponPushItemList.Count < 1)
                return true;
            foreach (var couponPushItem in couponPushItemList)
            {
                couponPushItem.ID = Util.NewID();
                couponPushItem.MerchantID = AppContext.CurrentSession.MerchantID;
                couponPushItem.CreatedUserID = AppContext.CurrentSession.UserID;
                couponPushItem.CreatedDate = DateTime.Now;
            }
            this.Repository.AddRange(couponPushItemList);
            return true;
        }

        /// <summary>
        /// 批量删除卡券推送子项
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool DeleteCouponPushItems(Guid[] ids)
        {
            if (ids == null || ids.Length < 1)
                throw new DomainException("参数错误");

            var couponPushItems = this.Repository.GetQueryable(false).Where(t => ids.Contains(t.ID)).ToList();
            if (couponPushItems == null || couponPushItems.Count() < 1)
                throw new DomainException("数据不存在");
            var couponPushIDs = this.Repository.GetQueryable(false).Where(t => ids.Contains(t.ID)).Select(t => t.CouponPushID).Distinct();
            foreach (var couponPushID in couponPushIDs)
            {
                var couponPush = CouponPushRepo.GetByKey(couponPushID);
                if (couponPush.Status != Domain.Enums.ECouponPushStatus.NotPush)
                    throw new DomainException("请选择未推送的数据!");
            }
            return this.Repository.DeleteRange(couponPushItems) > 0;
        }

        /// <summary>
        /// 查询卡券推送子项
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public IEnumerable<CouponPushItemDto> Search(CouponPushItemFilter filter, out int totalCount)
        {
            var queryable = this.Repository.GetQueryable(false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID);
            if (filter.CouponPushID.HasValue)
                queryable = queryable.Where(t => t.CouponPushID == filter.CouponPushID.Value);
            totalCount = queryable.Count();
            if (filter.Start.HasValue && filter.Limit.HasValue)
                queryable = queryable.OrderByDescending(t => t.ID).Skip(filter.Start.Value).Take(filter.Limit.Value);
            return queryable.MapTo<CouponPushItemDto>().ToArray();
        }
    }
}
