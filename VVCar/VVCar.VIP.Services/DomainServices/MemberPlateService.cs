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
    public class MemberPlateService : DomainServiceBase<IRepository<MemberPlate>, MemberPlate, Guid>, IMemberPlateService
    {
        public MemberPlateService()
        {
        }

        #region properties
        IRepository<MemberCard> MemberCardRepo { get => UnitOfWork.GetRepository<IRepository<MemberCard>>(); }

        IRepository<Member> MemberRepo { get => UnitOfWork.GetRepository<IRepository<Member>>(); }


        #endregion

        public override MemberPlate Add(MemberPlate entity)
        {
            if (entity == null)
                return null;
            if (string.IsNullOrEmpty(entity.PlateNumber))
                throw new DomainException("车牌号不能为空");
            entity.ID = Util.NewID();
            entity.PlateNumber = entity.PlateNumber.ToUpper();
            entity.MerchantID = AppContext.CurrentSession.MerchantID;
            return base.Add(entity);
        }

        /// <summary>
        /// 获取会员
        /// </summary>
        /// <param name="memberID"></param>
        /// <returns></returns>
        public MemberDto GetMemberByMemberID(Guid memberID)
        {
            if (memberID == null)
                throw new DomainException("参数错误");
            var member = MemberRepo.GetByKey(memberID).MapTo<MemberDto>();
            if(member == null)
                throw new DomainException("会员不存在");
            var memberCard = MemberCardRepo.GetByKey(member.CardID);
            if(memberCard != null)
            {
                member.CardNumber = memberCard.Code;
                member.CardBalance = memberCard.CardBalance;
                member.CardStatus = memberCard.Status;
                member.EffectiveDate = memberCard.EffectiveDate;
            }
            return member;
        }

        /// <summary>
        /// 通过车牌获取会员
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public IEnumerable<MemberDto> GetMemberByPlate(MemberPlateFilter filter, ref int totalCount)
        {
            if (string.IsNullOrEmpty(filter.PlateNumber))
                throw new DomainException("车牌号错误");
            var queryable = Repository.GetQueryable(false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID && t.PlateNumber == filter.PlateNumber && t.Member.IsDeleted == false).Select(t => t.Member).MapTo<MemberDto>();
            var result = queryable.ToList();
            if (result.Count < 1)
            {
                var member = new MemberDto();
                member.ID = Guid.Parse("00000000-0000-0000-0000-000000000000");
                member.Name = "未注册会员";
                result.Add(member);
            }
            else {
                result.ForEach(t =>
                {
                    var memberCard = MemberCardRepo.GetByKey(t.CardID);
                    if (memberCard != null)
                    {
                        t.CardNumber = memberCard.Code;
                        t.CardBalance = memberCard.CardBalance;
                        t.CardStatus = memberCard.Status;
                        t.EffectiveDate = memberCard.EffectiveDate;
                    }
                });
            }
            return result;
        }

        public IEnumerable<MemberPlate> Search(MemberPlateFilter filter, ref int totalCount)
        {
            var queryable = Repository.GetQueryable(false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID);
            if (filter.MemberID.HasValue)
                queryable = queryable.Where(t => t.MerchantID == filter.MemberID.Value);
            if (!string.IsNullOrEmpty(filter.OpenID))
                queryable = queryable.Where(t => t.OpenID == filter.OpenID);
            if (!string.IsNullOrEmpty(filter.PlateNumber))
                queryable = queryable.Where(t => t.PlateNumber.Contains(filter.PlateNumber));
            totalCount = queryable.Count();
            if (filter.Start.HasValue && filter.Limit.HasValue)
                queryable = queryable.OrderBy(t => t.ID).Skip(filter.Start.Value).Take(filter.Limit.Value);
            return queryable.ToArray();
        }
    }
}
