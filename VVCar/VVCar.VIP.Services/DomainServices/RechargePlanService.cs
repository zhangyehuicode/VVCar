using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.VIP.Domain;
using VVCar.VIP.Domain.Dtos;
using VVCar.VIP.Domain.Entities;
using VVCar.VIP.Domain.Enums;
using VVCar.VIP.Domain.Services;
using YEF.Core;
using YEF.Core.Data;
using YEF.Core.Domain;

namespace VVCar.VIP.Services.DomainServices
{
    /// <summary>
    /// 储值方案领域服务
    /// </summary>
    public partial class RechargePlanService : DomainServiceBase<IRepository<RechargePlan>, RechargePlan, Guid>, IRechargePlanService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RechargePlanService"/> class.
        /// </summary>
        public RechargePlanService()
        {
        }

        #region properties

        //IRepository<RechargePlanCouponTemplate> RechargePlanCouponTemplateRepo { get => UnitOfWork.GetRepository<IRepository<RechargePlanCouponTemplate>>(); }

        #endregion

        #region methods

        protected override bool DoValidate(RechargePlan entity)
        {
            if (entity.RechargeAmount == 0 && entity.VisibleAtWeChat)
                throw new DomainException("储值金额为0时，不允许勾选微信在线储值");
            bool exists = this.Repository.Exists(t => t.Code == entity.Code && t.ID != entity.ID);
            if (exists)
                throw new DomainException(String.Format("编号 {0} 已使用，不能重复添加。", entity.Code));
            exists = this.Repository.Exists(t => t.Name == entity.Name && t.ID != entity.ID);
            //if (exists)
            // throw new DomainException(String.Format("名称 {0} 已使用，不能重复添加。", entity.Name));
            return true;
        }

        public RechargePlan NewRechargePlan(NewUpdateRechargePlanDto entitydto)
        {
            if (entitydto == null)
                return null;
            var entity = entitydto.MapTo<RechargePlan>();

            if (entitydto.MatchRechargeCard)
                entity.MatchCardType = MemberCardTypes.PrePaidCard.ToString();

            if (entitydto.MatchDiscountCard)
            {
                if (!string.IsNullOrEmpty(entity.MatchCardType))
                    entity.MatchCardType += $",{MemberCardTypes.DiscountCard}";
                else
                    entity.MatchCardType += MemberCardTypes.DiscountCard;
            }

            if (entitydto.MatchGiftCard)
            {
                if (!string.IsNullOrEmpty(entity.MatchCardType))
                    entity.MatchCardType += $",{MemberCardTypes.GiftCard}";
                else
                    entity.MatchCardType += MemberCardTypes.GiftCard;
            }

            return Add(entity);
        }

        public override RechargePlan Add(RechargePlan entity)
        {
            if (entity == null)
                return null;
            entity.ID = Util.NewID();
            entity.CreatedUserID = AppContext.CurrentSession.UserID;
            entity.CreatedUser = AppContext.CurrentSession.UserName;
            entity.CreatedDate = DateTime.Now;

            //entity.RechargePlanCouponTemplates.ForEach(t =>
            //{
            //    t.ID = Util.NewID();
            //    t.RechargePlanID = entity.ID;
            //});

            return base.Add(entity);
        }

        public override bool Delete(Guid key)
        {
            var rechargePlan = this.Repository.GetByKey(key);
            if (rechargePlan == null)
                throw new DomainException("删除失败，数据不存在");
            rechargePlan.IsDeleted = true;
            this.Repository.Update(rechargePlan);
            return true;
        }

        public bool UpdateRechargePlan(NewUpdateRechargePlanDto entitydto)
        {
            if (entitydto == null)
                return false;
            var entity = entitydto.MapTo<RechargePlan>();
            if (entitydto.MatchRechargeCard)
                entity.MatchCardType = MemberCardTypes.PrePaidCard.ToString();

            if (entitydto.MatchDiscountCard)
            {
                if (!string.IsNullOrEmpty(entity.MatchCardType))
                    entity.MatchCardType += $",{MemberCardTypes.DiscountCard}";
                else
                    entity.MatchCardType += MemberCardTypes.DiscountCard;
            }

            if (entitydto.MatchGiftCard)
            {
                if (!string.IsNullOrEmpty(entity.MatchCardType))
                    entity.MatchCardType += $",{MemberCardTypes.GiftCard}";
                else
                    entity.MatchCardType += MemberCardTypes.GiftCard;
            }

            return Update(entity);
        }

        public override bool Update(RechargePlan entity)
        {
            if (entity == null)
                return false;
            var rechargePlan = this.Repository.GetByKey(entity.ID);
            if (rechargePlan == null)
                throw new DomainException("数据不存在");
            rechargePlan.Name = entity.Name;
            rechargePlan.PlanType = entity.PlanType;
            rechargePlan.IsAvailable = entity.IsAvailable;
            rechargePlan.EffectiveDate = entity.EffectiveDate;
            rechargePlan.ExpiredDate = entity.ExpiredDate;
            rechargePlan.RechargeAmount = entity.RechargeAmount;
            rechargePlan.GiveAmount = entity.GiveAmount;
            rechargePlan.LastUpdateUserID = AppContext.CurrentSession.UserID;
            rechargePlan.LastUpdateUser = AppContext.CurrentSession.UserName;
            rechargePlan.LastUpdateDate = DateTime.Now;
            rechargePlan.MaxRechargeCount = entity.MaxRechargeCount;
            rechargePlan.VisibleAtPortal = entity.VisibleAtPortal;
            rechargePlan.VisibleAtWeChat = entity.VisibleAtWeChat;
            rechargePlan.MatchCardType = entity.MatchCardType;

            //RechargePlanCouponTemplateRepo.Delete(t => t.RechargePlanID == entity.ID);
            //var rechargePlanCouponTemplates = entity.RechargePlanCouponTemplates;
            //if (rechargePlanCouponTemplates.Count > 0)
            //{
            //    foreach (var coupon in rechargePlanCouponTemplates)
            //    {
            //        RechargePlanCouponTemplateRepo.Add(new RechargePlanCouponTemplate
            //        {
            //            ID = Util.NewID(),
            //            RechargePlanID = entity.ID,
            //            CouponTemplateID = coupon.CouponTemplateID,
            //            Title = coupon.Title,
            //            Quantity = coupon.Quantity,
            //        });
            //    }
            //}

            return base.Update(rechargePlan);
        }

        #endregion

        #region IRechargePlanService 成员

        /// <summary>
        /// 改变状态
        /// </summary>
        /// <param name="planID">储值方案ID</param>
        /// <param name="isAvailable">是否可用</param>
        /// <returns></returns>
        /// <exception cref="DomainException">数据不存在</exception>
        public bool ChangeStatus(Guid planID, bool isAvailable)
        {
            var plan = this.Repository.GetByKey(planID);
            if (plan == null)
                throw new DomainException("数据不存在");
            plan.IsAvailable = isAvailable;
            plan.LastUpdateUserID = AppContext.CurrentSession.UserID;
            plan.LastUpdateUser = AppContext.CurrentSession.UserName;
            plan.LastUpdateDate = DateTime.Now;
            this.Repository.Update(plan);
            return true;
        }

        /// <summary>
        /// 查询储值方案
        /// </summary>
        /// <param name="filter">过滤条件</param>
        /// <param name="totalCount">总记录数</param>
        /// <returns></returns>
        public IEnumerable<RechargePlan> Search(Domain.Filters.RechargePlanFilter filter, out int totalCount)
        {
            totalCount = 0;
            IEnumerable<RechargePlan> pagedData = null;
            var queryable = this.Repository.GetQueryable(false);// GetInclude(t => t.RechargePlanCouponTemplates, false);
            if (filter != null)
            {
                if (!string.IsNullOrEmpty(filter.Code))
                    queryable = queryable.Where(t => t.Code.Contains(filter.Code));
                if (!string.IsNullOrEmpty(filter.Name))
                    queryable = queryable.Where(t => t.Name.Contains(filter.Name));
                if (filter.Status.HasValue)
                    queryable = queryable.Where(t => t.IsAvailable == filter.Status.Value);
                if (filter.PlanType.HasValue)
                    queryable = queryable.Where(t => t.PlanType == filter.PlanType.Value);
            }
            if (filter != null && filter.Start.HasValue && filter.Limit.HasValue)
            {
                totalCount = queryable.Count();
                pagedData = queryable.OrderBy(t => t.ID).Skip(filter.Start.Value).Take(filter.Limit.Value).ToArray();
            }
            else
            {
                pagedData = queryable.ToArray();
                totalCount = pagedData.Count();
            }
            return pagedData;
        }

        /// <summary>
        /// 获取可用的储值方案
        /// </summary>
        /// <returns></returns>
        public IEnumerable<RechargePlanDto> GetUsablePlans(EClientType clientType, string cardTypeId)
        {
            var queryable = this.Repository.GetQueryable(false)
                .Where(t => t.IsAvailable == true && t.EffectiveDate < DateTime.Now && t.ExpiredDate > DateTime.Now);
            if (!string.IsNullOrEmpty(cardTypeId))
                queryable = queryable.Where(t => t.MatchCardType.Contains(cardTypeId));
            else
            {
                var giftCardId = MemberCardTypes.GiftCard.ToString();
                var rechargeCardId = MemberCardTypes.PrePaidCard.ToString();
                var disCountCardId = MemberCardTypes.DiscountCard.ToString();
                switch (clientType)
                {
                    case EClientType.Portal:
                        queryable = queryable.Where(p => p.VisibleAtPortal && (p.MatchCardType.Contains(rechargeCardId) || p.MatchCardType.Contains(disCountCardId)));
                        break;
                    case EClientType.WeChat:
                        queryable = queryable.Where(p => p.VisibleAtWeChat && (p.MatchCardType.Contains(rechargeCardId) || p.MatchCardType.Contains(disCountCardId)));
                        break;
                    case EClientType.GiftCard:
                        queryable = queryable.Where(t => t.MatchCardType.Contains(giftCardId));
                        break;
                    default:
                        throw new DomainException("未知来源");
                }
            }
            return queryable.MapTo<RechargePlanDto>()
                .ToArray();
        }

        ///// <summary>
        ///// 查询储值方案
        ///// </summary>
        ///// <param name="filter">过滤条件</param>
        ///// <param name="totalCount">总记录数</param>
        ///// <returns></returns>
        //public IEnumerable<RechargePlanCouponTemplate> GetRechargePlanCouponTemplates(Domain.Filters.RechargePlanFilter filter, out int totalCount)
        //{
        //    totalCount = 0;
        //    IEnumerable<RechargePlan> pagedData = null;
        //    var result = new List<RechargePlanCouponTemplate>();
        //    if (filter == null || filter.RechargePlans == null || filter.RechargePlans.Count < 1)
        //        return result;
        //    var queryable = this.Repository.GetInclude(t => t.RechargePlanCouponTemplates, false);

        //    var codes = filter.RechargePlans.Select(t => t.RechargePlanCode).ToList();
        //    queryable = queryable.Where(t => codes.Contains(t.Code));

        //    if (!string.IsNullOrEmpty(filter.Code))
        //        queryable = queryable.Where(t => t.Code.Contains(filter.Code));
        //    if (filter.Codes != null && filter.Codes.Count() > 0)
        //        queryable = queryable.Where(t => filter.Codes.Contains(t.Code));
        //    if (!string.IsNullOrEmpty(filter.Name))
        //        queryable = queryable.Where(t => t.Name.Contains(filter.Name));
        //    if (filter.Status.HasValue)
        //        queryable = queryable.Where(t => t.IsAvailable == filter.Status.Value);
        //    if (filter.PlanType.HasValue)
        //        queryable = queryable.Where(t => t.PlanType == filter.PlanType.Value);

        //    if (filter.Start.HasValue && filter.Limit.HasValue)
        //    {
        //        totalCount = queryable.Count();
        //        pagedData = queryable.OrderBy(t => t.ID).Skip(filter.Start.Value).Take(filter.Limit.Value).ToArray();
        //    }
        //    else
        //    {
        //        pagedData = queryable.ToArray();
        //        totalCount = pagedData.Count();
        //    }
        //    var resultTemp = pagedData.Select(t => new
        //    {
        //        RechargePlanCouponTemplates = t.RechargePlanCouponTemplates,
        //        Quantity = filter.RechargePlans.FirstOrDefault(r => r.RechargePlanCode == t.Code) != null ? filter.RechargePlans.FirstOrDefault(r => r.RechargePlanCode == t.Code).Quantity : 1,
        //    }).ToArray();
        //    resultTemp.ForEach(t =>
        //    {
        //        t.RechargePlanCouponTemplates.ForEach(r =>
        //        {
        //            r.Quantity = r.Quantity * t.Quantity;
        //        });
        //        result.AddRange(t.RechargePlanCouponTemplates);
        //    });
        //    return result;
        //}

        #endregion
    }
}
