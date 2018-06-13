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
