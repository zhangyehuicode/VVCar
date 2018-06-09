using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.Shop.Domain.Dtos;
using VVCar.Shop.Domain.Entities;
using VVCar.Shop.Domain.Filters;
using YEF.Core.Data;
using YEF.Core.Domain;
using YEF.Core.Dtos;

namespace VVCar.Shop.Domain.Services
{
    /// <summary>
    /// 服务周期卡券服务领域接口
    /// </summary>
    public interface IServicePeriodCouponService : IDomainService<IRepository<ServicePeriodCoupon>, ServicePeriodCoupon, Guid>
    {
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="servicePeriodCoupons"></param>
        /// <returns></returns>
        bool BatchAdd(IEnumerable<ServicePeriodCoupon> servicePeriodCoupons);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        bool BatchDelete(Guid[] ids);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        IEnumerable<ServicePeriodCouponDto> Search(ServicePeriodCouponFilter filter, out int totalCount);
    }
}
