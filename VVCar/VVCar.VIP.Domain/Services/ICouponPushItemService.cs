using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.VIP.Domain.Dtos;
using VVCar.VIP.Domain.Entities;
using VVCar.VIP.Domain.Filters;
using YEF.Core.Data;
using YEF.Core.Domain;

namespace VVCar.VIP.Domain.Services
{
    /// <summary>
    /// 卡券推送子项服务
    /// </summary>
    public interface ICouponPushItemService : IDomainService<IRepository<CouponPushItem>, CouponPushItem, Guid>
    {
        /// <summary>
        /// 批量新增卡券推送子项
        /// </summary>
        /// <param name="couponPushItems"></param>
        /// <returns></returns>
        bool BatchAdd(IEnumerable<CouponPushItem> couponPushItems);

        /// <summary>
        /// 批量删除卡券推送子项
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        bool DeleteCouponPushItems(Guid[] ids);

        /// <summary>
        /// 查询卡券推送子项
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        IEnumerable<CouponPushItemDto> Search(CouponPushItemFilter filter, out int totalCount);
    }
}
