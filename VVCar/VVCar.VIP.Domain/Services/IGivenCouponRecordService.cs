using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.VIP.Domain.Entities;
using VVCar.VIP.Domain.Filters;
using YEF.Core.Data;
using YEF.Core.Domain;

namespace VVCar.VIP.Domain.Services
{
    /// <summary>
    /// 赠送卡券记录 领域服务接口
    /// </summary>
    public interface IGivenCouponRecordService : IDomainService<IRepository<GivenCouponRecord>, GivenCouponRecord, Guid>
    {
        /// <summary>
        /// 获取赠送记录
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="totalCount">The total count.</param>
        /// <returns></returns>
        IEnumerable<GivenCouponRecord> GetGivenCouponRecord(GivenCouponFilter filter, ref int totalCount);
    }
}
