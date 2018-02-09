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
    /// <summary>
    /// 赠送卡券记录 领域服务
    /// </summary>
    public class GivenCouponRecordService : DomainServiceBase<IRepository<GivenCouponRecord>, GivenCouponRecord, Guid>, IGivenCouponRecordService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GivenCouponRecordService"/> class.
        /// </summary>
        public GivenCouponRecordService()
        {
        }

        public override GivenCouponRecord Add(GivenCouponRecord entity)
        {
            if (entity == null)
            {
                return null;
            }
            if (entity.CouponID == null || entity.CouponID == Guid.Empty)
            {
                return null;
            }
            entity.ID = Util.NewID();
            entity.CreatedDate = DateTime.Now;
            return base.Add(entity);
        }

        public IEnumerable<GivenCouponRecord> GetGivenCouponRecord(GivenCouponFilter filter, ref int totalCount)
        {
            var queryable = Repository.GetQueryable(false);
            if (filter.CouponID.HasValue)
            {
                queryable = queryable.Where(t => t.CouponID == filter.CouponID);
            }
            totalCount = queryable.Count();
            return queryable.OrderByDescending(t => t.CreatedDate).ToArray();
        }
    }
}
