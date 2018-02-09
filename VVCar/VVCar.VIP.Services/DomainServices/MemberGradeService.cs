using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    /// 会员等级 领域服务实现
    /// </summary>
    public class MemberGradeService : DomainServiceBase<IRepository<MemberGrade>, MemberGrade, Guid>, IMemberGradeService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MemberGradeService"/> class.
        /// </summary>
        public MemberGradeService()
        {
        }

        #region properties

        IRepository<Member> MemberRepo { get => ServiceLocator.Instance.GetService<IRepository<Member>>(); }

        IMemberService _memberService;
        IMemberService MemberService
        {
            get
            {
                if (_memberService == null)
                {
                    _memberService = ServiceLocator.Instance.GetService<IMemberService>();
                }
                return _memberService;
            }
        }

        IRepository<TradeHistory> TradeHistoryRepo { get => ServiceLocator.Instance.GetService<IRepository<TradeHistory>>(); }

        IRepository<MemberGradeHistory> MemberGradeHistoryRepo { get => ServiceLocator.Instance.GetService<IRepository<MemberGradeHistory>>(); }

        IRepository<MemberGradeRight> MemberGradeRightRepo { get => ServiceLocator.Instance.GetService<IRepository<MemberGradeRight>>(); }

        #endregion

        #region methods
        protected override bool DoValidate(MemberGrade entity)
        {
            bool exists = this.Repository.Exists(t => t.Name == entity.Name && t.ID != entity.ID);
            if (exists)
                throw new DomainException($"等级名称 {entity.Name} 已使用，不能重复添加。");
            if (!entity.IsDefault)
            {
                if (entity.Level == 1)
                    throw new DomainException("等级排序1必须为默认等级");
                exists = this.Repository.Exists(t => t.IsDefault == true && t.ID != entity.ID);
                if (!exists)
                    throw new DomainException("必须选择一个默认等级");
            }
            return true;
        }

        public override MemberGrade Add(MemberGrade entity)
        {
            if (entity == null)
                return null;
            DoValidate(entity);
            entity.ID = Util.NewID();
            entity.CreatedUserID = AppContext.CurrentSession.UserID;
            entity.CreatedUser = AppContext.CurrentSession.UserName;
            entity.CreatedDate = DateTime.Now;
            if (entity.GradeRights != null && entity.GradeRights.Count > 0)
            {
                entity.GradeRights.ForEach(right =>
                {
                    right.ID = Util.NewID();
                    right.MemberGradeID = entity.ID;
                });
            }
            UnitOfWork.BeginTransaction();
            try
            {
                RearrangeGrade(entity);
                var addData = Repository.Add(entity);
                UnitOfWork.CommitTransaction();
                return addData;
            }
            catch (Exception ex)
            {
                UnitOfWork.RollbackTransaction();
                AppContext.Logger.Error("新增会员等级出现异常。", ex);
                throw ex;
            }
        }

        public override bool Update(MemberGrade entity)
        {
            if (entity == null)
                return false;
            var grade = Repository.GetInclude(t => t.GradeRights).Where(t => t.ID == entity.ID).FirstOrDefault();
            if (grade == null)
                throw new DomainException("数据不存在");
            DoValidate(entity);
            entity.CreatedUserID = grade.CreatedUserID;
            entity.CreatedUser = grade.CreatedUser;
            entity.CreatedDate = grade.CreatedDate;
            entity.LastUpdateUserID = AppContext.CurrentSession.UserID;
            entity.LastUpdateUser = AppContext.CurrentSession.UserName;
            entity.LastUpdateDate = DateTime.Now;
            UnitOfWork.BeginTransaction();
            try
            {
                RearrangeGrade(entity);
                Repository.Update(entity);
                MemberGradeRightRepo.Delete(grade.GradeRights);
                if (entity.GradeRights != null && entity.GradeRights.Count > 0)
                {
                    entity.GradeRights.ForEach(right =>
                    {
                        right.ID = Util.NewID();
                        right.MemberGradeID = entity.ID;
                    });
                    MemberGradeRightRepo.Add(entity.GradeRights);
                }
                UnitOfWork.CommitTransaction();
                return true;
            }
            catch (Exception ex)
            {
                UnitOfWork.RollbackTransaction();
                AppContext.Logger.Error("修改会员等级出现异常。", ex);
                throw ex;
            }
        }

        public override bool Delete(Guid key)
        {
            var grade = Repository.GetByKey(key);
            if (grade == null)
                throw new DomainException("数据不存在");
            if (grade.IsDefault)
                throw new DomainException("不能删除默认等级");
            var hasMember = MemberRepo.Exists(t => t.MemberGradeID == key);
            if (hasMember)
                throw new DomainException("该等级已有会员存在，无法删除！");
            Repository.Delete(grade);
            return true;
        }

        /// <summary>
        /// 重新整理队列
        /// </summary>
        void RearrangeGrade(MemberGrade grade)
        {
            if (grade.IsDefault)
            {
                grade.Status = EMemberGradeStatus.Enabled;
                var defaultGrade = Repository.Get(t => t.IsDefault);
                if (defaultGrade != null)
                {
                    defaultGrade.IsDefault = false;
                    Repository.Update(defaultGrade);
                }
            }
            var level = grade.Level;
            var upperGrades = Repository.GetQueryable().Where(t => t.Level >= level && t.ID != grade.ID)
                .OrderBy(t => t.Level)
                .ToArray();
            if (upperGrades.Length > 0)
            {
                foreach (var upperGrade in upperGrades)
                {
                    upperGrade.Level = ++level;
                }
                Repository.Update(upperGrades);
            }
        }

        /// <summary>
        /// 获取默认会员等级ID
        /// </summary>
        /// <returns></returns>
        public MemberGrade GetDefaultMemberGrade()
        {
            return Repository.GetQueryable(false)
                .Where(t => t.IsDefault).FirstOrDefault();
        }

        /// <summary>
        /// 会员等级查询
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="totalCount">The total count.</param>
        /// <returns></returns>
        public IEnumerable<MemberGrade> Search(MemberGradeFilter filter, out int totalCount)
        {
            var queryable = Repository.GetInclude(t => t.GradeRights);
            if (filter != null)
            {
                if (filter.Status.HasValue)
                {
                    queryable = queryable.Where(t => t.Status == filter.Status.Value);
                }
            }
            totalCount = queryable.Count();
            if (filter != null && filter.Start.HasValue && filter.Limit.HasValue)
            {
                queryable = queryable.OrderBy(t => t.Level).Skip(filter.Start.Value).Take(filter.Limit.Value);
            }
            return queryable.ToArray();
        }

        /// <summary>
        /// 设置等级状态
        /// </summary>
        /// <param name="memberGradeID">The grade identifier.</param>
        /// <param name="status"></param>
        /// <returns></returns>
        public bool ChangeStatus(Guid memberGradeID, EMemberGradeStatus status)
        {
            var memberGrade = Repository.GetByKey(memberGradeID);
            if (memberGrade == null)
                throw new DomainException("数据不存在");
            if (status == EMemberGradeStatus.Disabled && memberGrade.IsDefault)
                throw new DomainException("该等级为默认等级，不能关闭！");
            memberGrade.Status = status;
            Repository.Update(memberGrade);
            return true;
        }

        /// <summary>
        /// 设置开闭状态
        /// </summary>
        /// <param name="memberGradeID">The grade identifier.</param>
        /// <param name="openStatus"></param>
        /// <returns></returns>
        public bool ChangeOpen(Guid memberGradeID, bool isNotOpen)
        {
            var memberGrade = Repository.GetByKey(memberGradeID);
            if (memberGrade == null)
                throw new DomainException("数据不存在");
            memberGrade.IsNotOpen = isNotOpen;
            return Repository.Update(memberGrade) > 0;
        }

        /// <summary>
        /// 使用会员权益
        /// </summary>
        /// <param name="memberID">会员ID</param>
        /// <param name="isCousome">是否是消费操作</param>
        /// <param name="tradeAmount">交易金额</param>
        /// <exception cref="DomainException"></exception>
        public UseMemberGradeRightsResult UseMemberGradeRights(Guid memberID, bool isCousome, decimal tradeAmount)
        {
            var result = new UseMemberGradeRightsResult();
            var member = MemberRepo.GetIncludes(false, "MemberGrade", "Card").Where(t => t.ID == memberID).FirstOrDefault();
            if (member == null)
                throw new DomainException("使用会员权益失败，会员不存在");
            var giftPoint = 0;
            if (member.MemberGrade != null)
            {
                try
                {
                    var memberGrade = member.MemberGrade;
                    var giftPointType = EMemberPointType.MemberConsume;
                    if (isCousome && memberGrade.GiftPointByConsumeAmount.HasValue && memberGrade.ConsumeGiftPoint.HasValue)
                    {
                        giftPoint = decimal.ToInt32(tradeAmount / memberGrade.GiftPointByConsumeAmount.Value) * memberGrade.ConsumeGiftPoint.Value;
                        giftPointType = EMemberPointType.MemberConsume;
                    }
                    else if (!isCousome && memberGrade.GiftPointByConsumeAmount.HasValue && memberGrade.ConsumeGiftPoint.HasValue)
                    {
                        giftPoint = decimal.ToInt32(tradeAmount / memberGrade.GiftPointByRechargeAmount.Value) * memberGrade.RechargeGiftPoint.Value;
                        giftPointType = EMemberPointType.MemberRecharge;
                    }
                    if (giftPoint > 0)
                    {
                        MemberService.AdjustMemberPoint(memberID, giftPointType, giftPoint);
                    }
                }
                catch (Exception ex)
                {
                    AppContext.Logger.Error("使用会员权益 出现异常。", ex);
                }
            }
            result = CheckGradeUpgrade(member, isCousome, tradeAmount);
            result.GiftPoint = giftPoint;
            return result;
        }

        /// <summary>
        /// 检查是否可以升级会员等级
        /// </summary>
        public UseMemberGradeRightsResult CheckGradeUpgrade(Member member, bool isCousome, decimal tradeAmount)
        {
            var result = new UseMemberGradeRightsResult();
            try
            {
                var queryable = Repository.GetInclude(t => t.GradeRights, false)
                    .Where(t => t.Status == EMemberGradeStatus.Enabled && t.Level > member.MemberGrade.Level);
                MemberGrade matchGrade = null;
                if (isCousome)
                {
                    queryable = queryable.Where(t => t.IsQualifyByConsume);
                    var highGrades = queryable.OrderByDescending(t => t.Level).ToList();
                    foreach (var highGrade in highGrades)
                    {
                        if (highGrade.QualifyByConsumeOneOffAmount.HasValue && tradeAmount >= highGrade.QualifyByConsumeOneOffAmount)
                        {
                            matchGrade = highGrade;
                            break;
                        }
                        //if (highGrade.QualifyByConsumeTotalAmount.HasValue && member.Card.TotalConsume >= highGrade.QualifyByConsumeTotalAmount)
                        //{
                        //    matchGrade = highGrade;
                        //    break;
                        //}
                        //if (highGrade.QualifyByConsumeLimitedMonths.HasValue && highGrade.QualifyByConsumeTotalCount.HasValue)
                        //{
                        //    var limitDate = DateTime.Today.AddMonths(-1 * highGrade.QualifyByConsumeLimitedMonths.Value);
                        //    var tradeCount = TradeHistoryRepo.Count(t => t.MemberID == member.ID && t.CreatedDate > limitDate);
                        //    if (tradeCount >= highGrade.QualifyByConsumeTotalCount.Value)
                        //    {
                        //        matchGrade = highGrade;
                        //        break;
                        //    }
                        //}
                        if (highGrade.QualifyByConsumeTotalAmount.HasValue && highGrade.QualifyByConsumeLimitedMonths.HasValue && highGrade.QualifyByConsumeTotalCount.HasValue)
                        {
                            var limitDate = DateTime.Today.AddMonths(-1 * highGrade.QualifyByConsumeLimitedMonths.Value);
                            var tradeCount = TradeHistoryRepo.Count(t => t.MemberID == member.ID && t.MemberGradeID == member.MemberGradeID && t.CreatedDate > limitDate);
                            var totalConsume = TradeHistoryRepo.GetQueryable(false).Where(t => t.MemberID == member.ID && t.MemberGradeID == member.MemberGradeID).GroupBy(t => 1).Select(t => t.Sum(s => s.TradeAmount)).FirstOrDefault();
                            if (tradeCount >= highGrade.QualifyByConsumeTotalCount.Value && totalConsume >= highGrade.QualifyByConsumeTotalAmount)
                            {
                                matchGrade = highGrade;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    queryable = queryable.Where(t => t.IsQualifyByRecharge);
                    var highGrades = queryable.OrderByDescending(t => t.Level).ToList();
                    foreach (var highGrade in highGrades)
                    {
                        if (highGrade.QualifyByRechargeOneOffAmount.HasValue && tradeAmount >= highGrade.QualifyByRechargeOneOffAmount)
                        {
                            matchGrade = highGrade;
                            break;
                        }
                        if (highGrade.QualifyByRechargeTotalAmount.HasValue && member.Card.TotalRecharge >= highGrade.QualifyByRechargeTotalAmount)
                        {
                            matchGrade = highGrade;
                            break;
                        }
                    }
                }
                if (matchGrade == null)
                    return result;
                result.IsUpGrade = MemberService.SetMemberGrade(member.ID, matchGrade);
                if (result.IsUpGrade)
                {
                    result.UpGradeName = matchGrade.Name;
                    if (matchGrade.GradeRights != null && matchGrade.GradeRights.Count > 0)
                    {
                        foreach (var right in matchGrade.GradeRights)
                        {
                            if (!string.IsNullOrEmpty(right.PosRightName))
                                result.GradeRightsDesc += $"{right.PosRightName}、";
                        }
                        if (result.GradeRightsDesc != null && result.GradeRightsDesc.Length > 0)
                            result.GradeRightsDesc = result.GradeRightsDesc.Substring(0, result.GradeRightsDesc.Length - 1);
                    }
                }
            }
            catch (Exception ex)
            {
                AppContext.Logger.Error("检查是否可以升级会员等级 出现异常。", ex);
            }
            return result;
        }

        /// <summary>
        /// 检查会员等级降级
        /// </summary>
        public void CheckGradeDegrade()
        {
            AppContext.Logger.Debug("开始调用 检查会员等级降级");
            var grades = Repository.GetQueryable(false)
                .Where(t => !t.IsNeverExpires)
                .Select(t => new
                {
                    t.ID,
                    t.Name,
                    t.ExpireAfterJoinDays
                }).ToList();
            if (grades.Count < 1)
                return;
            AppContext.Logger.Debug($"会员等级降级 {grades.Count}项");
            var defalutGrade = GetDefaultMemberGrade();
            DateTime expiredDate;
            foreach (var grade in grades)
            {
                expiredDate = DateTime.Today.AddDays(-1 * grade.ExpireAfterJoinDays.Value).AddDays(1);
                var degradeMemberList = MemberGradeHistoryRepo.GetQueryable(false)
                    .Where(t => t.Member.MemberGradeID == grade.ID && t.CreatedDate < expiredDate)
                    .Select(t => t.MemberID)
                    .ToList();
                AppContext.Logger.Debug($"会员等级{grade.Name} 有{degradeMemberList.Count} 会员需要降级");
                foreach (var degradeMember in degradeMemberList)
                {
                    MemberService.SetMemberGrade(degradeMember, defalutGrade);
                }
            }
        }

        public IEnumerable<MemberGradeIntroDto> GetCanPurchaseGradeList(Guid? currentGradeID)
        {
            var queryable = Repository.GetQueryable(false)
                .Where(t => t.Status == EMemberGradeStatus.Enabled && t.IsQualifyByPurchase && !t.IsNotOpen);
            decimal currentGradePrice = 0;
            if (currentGradeID.HasValue)
            {
                var currentGrade = Repository.GetQueryable(false)
                    .Where(t => t.ID == currentGradeID)
                    .Select(t => new
                    {
                        t.Level,
                        t.QualifyByPurchaseAmount
                    }).FirstOrDefault();
                if (currentGrade != null)
                {
                    queryable = queryable.Where(t => t.Level > currentGrade.Level);
                    currentGradePrice = currentGrade.QualifyByPurchaseAmount.GetValueOrDefault();
                }
            }
            var gradeList = queryable.OrderBy(t => t.Level)
                .Select(t => new
                {
                    t.ID,
                    t.Name,
                    t.QualifyByPurchaseAmount,
                    t.IsAllowDiffPurchaseAmount,
                    t.DiscountRate,
                    t.GradePoint,
                    ProductRightCount = t.GradeRights.Where(r => r.RightType == EGradeRightType.Product).Count(),
                    t.ConsumeGiftPoint,
                    t.RechargeGiftPoint,
                    DiscountRights = t.GradeRights.Where(r => r.RightType == EGradeRightType.Discount).ToList(),
                    t.IsDefault,
                }).ToList();
            var result = gradeList.Select(t => new MemberGradeIntroDto
            {
                ID = t.ID,
                Name = t.Name,
                PurchaseAmount = t.IsAllowDiffPurchaseAmount ? t.QualifyByPurchaseAmount.GetValueOrDefault() - currentGradePrice : t.QualifyByPurchaseAmount.GetValueOrDefault(),
                OrignalPurchaseAmount = t.QualifyByPurchaseAmount.GetValueOrDefault(),
                DiscountRate = t.DiscountRate,
                GradePoint = t.GradePoint,
                ProductRight = t.ProductRightCount > 0 ? "有" : "无",
                IsConsumeGiftPoint = t.ConsumeGiftPoint.HasValue,
                IsRechargeGiftPoint = t.RechargeGiftPoint.HasValue,
                DiscountDesc = t.DiscountRights?.FirstOrDefault() != null ? (t.DiscountRights?.FirstOrDefault().PosRightDiscount < 1 ? t.DiscountRights?.FirstOrDefault().PosRightName : "无") : "无",/* (t.DiscountRate.HasValue && t.DiscountRate > 0) ? (t.DiscountRate.Value == 1 ? "无" : ((t.DiscountRate.Value * 10).ToString("0.#") + "折")) : GetDiscountRightsCoefficientDesc(t.DiscountRights),*/
                IsDefault = t.IsDefault,
            }).ToList();
            result.ForEach(grade =>
            {
                if (grade.PurchaseAmount < 0) grade.PurchaseAmount = 0;
            });
            return result;
        }

        private string GetDiscountRightsCoefficientDesc(List<MemberGradeRight> discountGradeRights)
        {
            var count = 0;
            var result = string.Empty;
            foreach (var right in discountGradeRights)
            {
                if (count >= 3)
                    break;
                result += $"{(right.PosRightDiscount == 1 ? "无" : ((right.PosRightDiscount * 10).ToString("0.#") + "折"))}、";
                count++;
            }
            return result.Substring(0, result.Length - 1);
        }

        public IEnumerable<MemberRightDto> GetAllGradePosProductRights()
        {
            return MemberGradeRightRepo.GetQueryable(false)
                .Where(t => t.RightType == EGradeRightType.Product)
                .Select(t => new
                {
                    PosRightID = t.PosRightID,
                    PosRightCode = t.PosRightCode,
                    PosRightName = t.PosRightName,
                    GradeLevel = t.MemberGrade.Level,
                    GradeName = t.MemberGrade.Name
                })
                .GroupBy(t => t.PosRightID)
                .Select(t => new MemberRightDto
                {
                    RightID = t.Key,
                    PosRightCode = t.Max(g => g.PosRightCode),
                    PosRightName = t.Max(g => g.PosRightName),
                    Remark = t.OrderBy(g => g.GradeLevel).FirstOrDefault().GradeName + "专享",
                }).ToList();
        }

        public IEnumerable<IDCodeNameDto> GetGradeDiscountRights(string openid)
        {
            var result = new List<IDCodeNameDto>();
            var member = MemberRepo.GetIncludes(false, "MemberGrade", "MemberGrade.GradeRights").Where(t => t.WeChatOpenID == openid).FirstOrDefault();
            if (member != null && member.MemberGrade != null && member.MemberGrade.GradeRights != null)
            {
                result = member.MemberGrade.GradeRights
                   .Where(t => t.RightType == EGradeRightType.Discount)
                   .Select(t => new IDCodeNameDto
                   {
                       ID = t.PosRightID,
                       Code = t.PosRightCode,
                       Name = t.PosRightName,
                       Discount = t.PosRightDiscount,
                   }).ToList();
            }
            return result;
        }

        #endregion
    }
}
