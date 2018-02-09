using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.VIP.Domain.Dtos;
using VVCar.VIP.Domain.Entities;
using YEF.Core.Data;
using YEF.Core.Domain;

namespace VVCar.VIP.Domain.Services
{
    /// <summary>
    /// 优惠券库存 领域服务接口
    /// </summary>
    public partial interface ICouponTemplateStockService : IDomainService<IRepository<CouponTemplateStock>, CouponTemplateStock, Guid>
    {
        /// <summary>
        /// 更改卡券库存
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool UpdateStock(CouponTemplateDto entity);
    }
}
