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
    /// <summary>
    /// 卡券推送会员服务
    /// </summary>
    public class CouponPushMemberService : DomainServiceBase<IRepository<CouponPushMember>, CouponPushMember, Guid>, ICouponPushMemberService
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public CouponPushMemberService()
        {
        }

        #region properties

        IRepository<CouponPush> CouponPushRepo { get => UnitOfWork.GetRepository<IRepository<CouponPush>>(); }

        IRepository<MemberPlate> MemberPlateRepo { get => UnitOfWork.GetRepository<IRepository<MemberPlate>>(); }

        #endregion

        /// <summary>
        /// 批量新增卡券推送会员
        /// </summary>
        /// <param name="couponPushMembers"></param>
        /// <returns></returns>
        public bool BatchAdd(IEnumerable<CouponPushMember> couponPushMembers)
        {
            if (couponPushMembers == null || couponPushMembers.Count() < 1)
                throw new DomainException("没有数据");
            var couponPushMemberList = couponPushMembers.ToList();
            var couponPushID = couponPushMemberList.FirstOrDefault().CouponPushID;
            var couponPush = CouponPushRepo.GetByKey(couponPushID);
            if (couponPush.Status != Domain.Enums.ECouponPushStatus.NotPush)
                throw new DomainException("请选择未推送的数据!");
            var couponPushMemberIDs = couponPushMemberList.Select(t => t.MemberID).Distinct();
            var existData = this.Repository.GetQueryable(false)
                .Where(t => t.CouponPushID == couponPushID && couponPushMemberIDs.Contains(t.MemberID))
                .Select(t => t.MemberID).ToList();
            if (existData.Count > 0)
                couponPushMemberList.RemoveAll(t => existData.Contains(t.MemberID));
            if (couponPushMemberList.Count < 1)
                return true;
            foreach (var couponPushMember in couponPushMemberList)
            {
                couponPushMember.ID = Util.NewID();
                couponPushMember.MerchantID = AppContext.CurrentSession.MerchantID;
                couponPushMember.CreatedUserID = AppContext.CurrentSession.UserID;
                couponPushMember.CreatedUser = AppContext.CurrentSession.UserName;
                couponPushMember.CreatedDate = DateTime.Now;
            }
            return Repository.AddRange(couponPushMemberList).Count() > 0;
        }

        /// <summary>
        /// 批量删除卡券推送会员
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool BatchDelete(Guid[] ids)
        {
            if (ids == null || ids.Length < 1)
                throw new DomainException("参数错误");
            var couponPushMemberList = Repository.GetQueryable(false).Where(t => ids.Contains(t.ID)).ToList();
            if (couponPushMemberList == null || couponPushMemberList.Count() < 1)
                throw new DomainException("数据不存在");
            var couponPushIDs = Repository.GetQueryable(false).Where(t => ids.Contains(t.ID)).Select(t => t.CouponPushID).Distinct();
            foreach (var couponPushID in couponPushIDs)
            {
                var couponPush = CouponPushRepo.GetByKey(couponPushID);
                if (couponPush.Status != Domain.Enums.ECouponPushStatus.NotPush)
                    throw new DomainException("请选择未推送的数据!");
            }
            return Repository.DeleteRange(couponPushMemberList) > 0;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public IEnumerable<CouponPushMemberDto> Search(CouponPushMemberFilter filter, out int totalCount)
        {
            if (filter.CouponPushID == null)
                throw new DomainException("参数错误");
            var queryable = this.Repository.GetInclude(t => t.Member, false).Where(t => t.CouponPushID == filter.CouponPushID && t.MerchantID == AppContext.CurrentSession.MerchantID);
            totalCount = queryable.Count();
            if (filter.Start.HasValue && filter.Limit.HasValue)
                queryable = queryable.OrderByDescending(t => t.ID).Skip(filter.Start.Value).Take(filter.Limit.Value);
            var result = queryable.MapTo<CouponPushMemberDto>().ToArray();
            var memberPlateQueryable = MemberPlateRepo.GetQueryable(false);
            result.ForEach(t =>
            {
                var memberPlateList = memberPlateQueryable.Where(p => p.MemberID == t.MemberID);
                if (memberPlateList.Count() > 0)
                {
                    foreach (var memberPlate in memberPlateList)
                    {
                        t.PlateList += memberPlate.PlateNumber + "、";
                    }
                    t.PlateList = t.PlateList.Substring(0, t.PlateList.Length - 1);
                }
            });
            return result;
        }
    }
}
