using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        /// 通过车牌获取会员
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public IEnumerable<Member> GetMemberByPlate(MemberPlateFilter filter, ref int totalCount)
        {
            if (string.IsNullOrEmpty(filter.PlateNumber))
                throw new DomainException("车牌号错误");
            var queryable = Repository.GetQueryable(false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID && t.PlateNumber == filter.PlateNumber).Select(t => t.Member);
            var result = queryable.ToList();
            if (result.Count < 1) {
                var member = new Member();
                member.ID = Guid.Parse("00000000-0000-0000-0000-000000000000");
                member.Name = "未注册会员";
                result.Add(member);
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
