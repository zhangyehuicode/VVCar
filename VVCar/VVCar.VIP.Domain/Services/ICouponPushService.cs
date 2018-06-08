using System;
using System.Collections.Generic;
using VVCar.VIP.Domain.Dtos;
using VVCar.VIP.Domain.Entities;
using VVCar.VIP.Domain.Filters;
using YEF.Core.Data;
using YEF.Core.Domain;

namespace VVCar.VIP.Domain.Services
{
    /// <summary>
    /// 卡券推送服务领域接口
    /// </summary>
    public partial interface ICouponPushService : IDomainService<IRepository<CouponPush>, CouponPush, Guid>
    {
        /// <summary>
        /// 批量删除卡券推送任务
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        bool DeleteCouponPushs(Guid[] ids);

        /// <summary>
        /// 手动批量推送
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        bool BatchHandCouponPush(Guid[] ids);
        
        /// <summary>
        /// 查询卡券推送任务
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        IEnumerable<CouponPushDto> Search(CouponPushFilter filter, out int totalCount);
    }
}
