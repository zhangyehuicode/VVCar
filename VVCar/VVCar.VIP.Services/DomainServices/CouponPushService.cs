using System;
using System.Collections.Generic;
using System.Linq;
using VVCar.VIP.Domain.Dtos;
using VVCar.VIP.Domain.Entities;
using VVCar.VIP.Domain.Enums;
using VVCar.VIP.Domain.Filters;
using VVCar.VIP.Domain.Services;
using YEF.Core;
using YEF.Core.Data;
using YEF.Core.Domain;

namespace VVCar.VIP.Services.DomainServices
{
    /// <summary>
    /// 卡券推送服务 
    /// </summary>
    public partial class CouponPushService : DomainServiceBase<IRepository<CouponPush>, CouponPush, Guid>, ICouponPushService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CouponPushService"/> class.
        /// </summary>
        public CouponPushService()
        {
        }

        #region properties

        IRepository<CouponPushItem> CouponPushItemRepo { get => UnitOfWork.GetRepository<IRepository<CouponPushItem>>(); }

        #endregion

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override CouponPush Add(CouponPush entity)
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

        /// <summary>
        ///批量删除卡券推送任务
        /// </summary>
        /// <param name="ids"></param>
        public bool DeleteCouponPushs(Guid[] ids)
        {
            if (ids == null || ids.Length < 1)
                throw new DomainException("参数错误");
            var couponPushList = this.Repository.GetInclude(t => t.CouponPushItems, false).Where(t => ids.Contains(t.ID)).ToList();
            if (couponPushList == null || couponPushList.Count() < 1)
                throw new DomainException("未选择数据");
            UnitOfWork.BeginTransaction();
            try
            {
                foreach (var couponPush in couponPushList)
                {
                    if (couponPush.CouponPushItems.Count() > 0)
                    {
                        CouponPushItemRepo.DeleteRange(couponPush.CouponPushItems);
                        couponPush.CouponPushItems = null;
                    }
                }
                this.Repository.Delete(couponPushList);
                UnitOfWork.CommitTransaction();
                return true;
            }
            catch (Exception e)
            {
                UnitOfWork.RollbackTransaction();
                throw e;
            }
        }

        /// <summary>
        /// 手动批量推送卡券
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool BatchHandCouponPush(Guid[] ids)
        {
            if (ids == null || ids.Length < 1)
                throw new DomainException("参数不正确");
            var notPushData = this.Repository.GetQueryable().Where(t => ids.Contains(t.ID) && ECouponPushStatus.NotPush == t.Status).ToList();
            if (notPushData.Count < 1)
                throw new DomainException("请选择未推送的数据");
            notPushData.ForEach(t =>
                t.Status = ECouponPushStatus.Pushed
            );
            this.Repository.UpdateRange(notPushData);
            return true;
        }



        /// <summary>
        /// 查询卡券推送任务
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public IEnumerable<CouponPushDto> Search(CouponPushFilter filter, out int totalCount)
        {
            var queryable = this.Repository.GetQueryable(false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID);
            if (!string.IsNullOrEmpty(filter.Title))
                queryable = queryable.Where(t => t.Title.Contains(filter.Title));
            if (!filter.ShowAll)
                queryable = queryable.Where(t => t.Status == filter.Status);
            totalCount = queryable.Count();
            if (filter.Start.HasValue && filter.Limit.HasValue)
                queryable = queryable.OrderByDescending(t => t.CreatedDate).Skip(filter.Start.Value).Take(filter.Limit.Value);
            return queryable.MapTo<CouponPushDto>().ToArray();
        }
    }
}
