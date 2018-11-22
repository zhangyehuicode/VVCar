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

        IRepository<CouponPushMember> CouponPushMemberRepo { get => UnitOfWork.GetRepository<IRepository<CouponPushMember>>(); }

        ICouponService CouponService { get => ServiceLocator.Instance.GetService<ICouponService>(); }

        IRepository<Member> MemberRepo { get => UnitOfWork.GetRepository<IRepository<Member>>(); }

        IRepository<Merchant> MerchantRepo { get => UnitOfWork.GetRepository<IRepository<Merchant>>(); }

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
            var couponPushList = this.Repository.GetIncludes(false, "CouponPushItems", "CouponPushMembers").Where(t => ids.Contains(t.ID) && t.Status == ECouponPushStatus.NotPush).ToList();
            if (couponPushList == null || couponPushList.Count() < 1)
                throw new DomainException("请选择未推送的数据");
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
                    if (couponPush.CouponPushMembers.Count() > 0)
                    {
                        CouponPushMemberRepo.DeleteRange(couponPush.CouponPushMembers);
                        couponPush.CouponPushMembers = null;
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
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override bool Update(CouponPush entity)
        {
            if (entity == null)
                return false;
            var couponPush = Repository.GetByKey(entity.ID);
            if (couponPush == null)
                return false;
            if (couponPush.Status == ECouponPushStatus.Pushed)
                throw new DomainException("已推送不能修改");
            couponPush.Title = entity.Title;
            couponPush.PushDate = entity.PushDate;
            couponPush.PushAllMembers = entity.PushAllMembers;
            couponPush.Status = entity.Status;
            couponPush.LastUpdateUserID = AppContext.CurrentSession.UserID;
            couponPush.LastUpdateUser = AppContext.CurrentSession.UserName;
            couponPush.LastUpdateDate = DateTime.Now;
            return Repository.Update(couponPush) > 0;
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
            var notPushData = this.Repository.GetIncludes(false, "CouponPushItems", "CouponPushMembers").Where(t => ids.Contains(t.ID) && ECouponPushStatus.NotPush == t.Status).ToList();
            if (notPushData.Count < 1)
                throw new DomainException("请选择未推送的数据");
            var notExistItem = false;
            notPushData.ForEach(t =>
            {
                if (t.CouponPushItems.Count() < 1)
                    notExistItem = true;
            });
            if (notExistItem)
                throw new DomainException("请先添加卡券");
            var memberCount = MemberRepo.GetQueryable(false).Where(t => t.Card.Status == ECardStatus.Activated && t.MerchantID == AppContext.CurrentSession.MerchantID).Count();
            if (memberCount < 1)
                throw new DomainException("还没有会员!");
            return CouponPushAction(notPushData);
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
            if (filter.Status.HasValue)
                queryable = queryable.Where(t => t.Status == filter.Status);
            totalCount = queryable.Count();
            if (filter.Start.HasValue && filter.Limit.HasValue)
                queryable = queryable.OrderByDescending(t => t.CreatedDate).Skip(filter.Start.Value).Take(filter.Limit.Value);
            return queryable.MapTo<CouponPushDto>().ToArray();
        }

        /// <summary>
        /// 卡券推送任务
        /// </summary>
        /// <returns></returns>
        public bool CouponPushTask()
        {
            var starttime = DateTime.Now.Date;
            var endtime = starttime.AddDays(1);
            var couponPushList = Repository.GetIncludes(false, "CouponPushItems", "CouponPushMembers").Where(t => t.PushDate >= starttime && t.PushDate < endtime && t.Status == ECouponPushStatus.NotPush).ToList();
            return CouponPushAction(couponPushList);
        }

        /// <summary>
        /// 卡券推送动作
        /// </summary>
        /// <param name="couponPushList"></param>
        /// <returns></returns>
        public bool CouponPushAction(List<CouponPush> couponPushList)
        {
            if (couponPushList != null && couponPushList.Count() > 0)
            {
                UnitOfWork.BeginTransaction();
                try
                {
                    var memberQueryable = MemberRepo.GetQueryable(false).Where(t => t.Card.Status == ECardStatus.Activated);
                    couponPushList.ForEach(t =>
                    {
                        if (t.CouponPushItems != null && t.CouponPushItems.Count() > 0)
                        {
                            if (!t.PushAllMembers)
                            {
                                var memberids = new List<Guid>();
                                if (t.CouponPushMembers != null)
                                    memberids = t.CouponPushMembers.Select(m => m.MemberID).ToList();
                                memberQueryable = memberQueryable.Where(m => memberids.Contains(m.ID));
                            }
                            var members = memberQueryable.Where(m => m.MerchantID == t.MerchantID).ToList();
                            var merchant = MerchantRepo.GetQueryable(false).Where(m => m.ID == t.MerchantID).FirstOrDefault();
                            if (members != null && members.Count() > 0 && merchant != null)
                            {
                                members.ForEach(m =>
                                {
                                    //if (!string.IsNullOrEmpty(m.WeChatOpenID))
                                    //{
                                    try
                                    {
                                        CouponService.ReceiveCouponsAtcion(new ReceiveCouponDto
                                        {
                                            ReceiveOpenID = m.WeChatOpenID,
                                            CouponTemplateIDs = t.CouponPushItems.Select(item => item.CouponTemplateID).ToList(),
                                            ReceiveChannel = "卡券推送",
                                            CompanyCode = merchant.Code,
                                            NickName = m.Name,
                                            MerchantID = merchant.ID,
                                            MemberID = m.ID,
                                        }, true);
                                    }
                                    catch (Exception e)
                                    {
                                        AppContext.Logger.Error($"卡券推送自动领取卡券出现异常，{e.Message}");
                                    }
                                    //}
                                });
                                t.Status = ECouponPushStatus.Pushed;
                            }
                        }
                    });
                    Repository.Update(couponPushList);
                    UnitOfWork.CommitTransaction();
                }
                catch (Exception e)
                {
                    UnitOfWork.RollbackTransaction();
                    throw e;
                }
            }
            return true;
        }

        /// <summary>
        /// 立即推送
        /// </summary>
        /// <param name="couponTemplateIDs"></param>
        /// <param name="memberIDs"></param>
        /// <returns></returns>
        public bool ImmediatePushAction(Guid[] couponTemplateIDs, Guid[] memberIDs)
        {
            if (couponTemplateIDs.Count() < 1 || memberIDs.Count() < 1)
                throw new DomainException("参数错误");
            var count = 0;
            var memberList = MemberRepo.GetQueryable(false).Where(t => memberIDs.Contains(t.ID)).ToList();
            memberList.ForEach(t =>
            {
                try
                {
                    CouponService.ReceiveCouponsAtcion(new ReceiveCouponDto
                    {
                        ReceiveOpenID = t.WeChatOpenID,
                        CouponTemplateIDs = couponTemplateIDs,
                        ReceiveChannel = "卡券推送",
                        CompanyCode = AppContext.CurrentSession.MerchantCode,
                        NickName = t.Name,
                        MerchantID = AppContext.CurrentSession.MerchantID,
                        MemberID = t.ID,
                    }, true);
                }
                catch (Exception e)
                {
                    AppContext.Logger.Error($"立即推送卡券出现异常，{e.Message}");
                    if (memberIDs.Count() == 1)
                    {
                        throw new DomainException(e.Message);
                    }
                    count++;
                }
            });
            if (count == memberIDs.Count() && memberIDs.Count() != 1)
            {
                throw new DomainException("推送失败");
            }
            return true;
        }
    }
}
