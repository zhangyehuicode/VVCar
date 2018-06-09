using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.BaseData.Services;
using VVCar.Shop.Domain.Dtos;
using VVCar.Shop.Domain.Entities;
using VVCar.Shop.Domain.Filters;
using VVCar.Shop.Domain.Services;
using YEF.Core;
using YEF.Core.Data;
using YEF.Core.Domain;

namespace VVCar.Shop.Services.DomainServices
{
    /// <summary>
    /// 服务周期卡券领域服务
    /// </summary>
    public class ServicePeriodCouponService : DomainServiceBase<IRepository<ServicePeriodCoupon>, ServicePeriodCoupon, Guid>, IServicePeriodCouponService
    {
        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="servicePeriodCoupons"></param>
        /// <returns></returns>
        public bool BatchAdd(IEnumerable<ServicePeriodCoupon> servicePeriodCoupons)
        {
            if (servicePeriodCoupons == null || servicePeriodCoupons.Count() < 1)
                throw new DomainException("新增失败,没有数据");
            var servicePeriodCouponList = servicePeriodCoupons.ToList();
            var servicePeriodSettingID = servicePeriodCouponList.FirstOrDefault().ServicePeriodSettingID;
            var couponTemplateIDs = servicePeriodCouponList.Select(t=>t.ServicePeriodSettingID).Distinct();
            var existData = this.Repository.GetQueryable(false)
                .Where(t => t.ServicePeriodSettingID == servicePeriodSettingID && couponTemplateIDs.Contains(t.CouponTemplateID))
                .Select(t => t.CouponTemplateID).ToList();
            if (existData.Count > 0)
                servicePeriodCouponList.RemoveAll(t => existData.Contains(t.CouponTemplateID));
            if (servicePeriodCouponList.Count < 1)
                return true;
            foreach(var servicePeriodCoupon in servicePeriodCouponList)
            {
                servicePeriodCoupon.ID = Util.NewID();
                servicePeriodCoupon.MerchantID = AppContext.CurrentSession.MerchantID;
            }
            this.Repository.AddRange(servicePeriodCouponList);
            return true;
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool BatchDelete(Guid[] ids)
        {
            if (ids == null || ids.Length < 1)
                throw new DomainException("参数错误");
            var servicePeriodCouponList = this.Repository.GetQueryable(false)
                .Where(t => ids.Contains(t.ID)).ToList();
            if (servicePeriodCouponList == null || servicePeriodCouponList.Count < 1)
                throw new DomainException("数据不存在");
            return this.Repository.DeleteRange(servicePeriodCouponList) > 0;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public IEnumerable<ServicePeriodCouponDto> Search(ServicePeriodCouponFilter filter, out int totalCount)
        {
            var queryable = this.Repository.GetQueryable(false).Where(t=> t.MerchantID == AppContext.CurrentSession.MerchantID);
            if (filter.ServicePeriodSettingID.HasValue)
                queryable = queryable.Where(t=>t.ServicePeriodSettingID == filter.ServicePeriodSettingID.Value);
            totalCount = queryable.Count();
            if (filter.Start.HasValue && filter.Limit.HasValue)
                queryable = queryable.OrderByDescending(t=>t.ID).Skip(filter.Start.Value).Take(filter.Limit.Value);
            return queryable.MapTo<ServicePeriodCouponDto>().ToArray();
        }
    }
}
