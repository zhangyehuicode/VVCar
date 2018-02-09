using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.VIP.Domain.Dtos;
using VVCar.VIP.Domain.Entities;
using VVCar.VIP.Domain.Services;
using YEF.Core.Data;
using YEF.Core.Domain;

namespace VVCar.VIP.Services.DomainServices
{
    /// <summary>
    /// 优惠券库存 领域服务
    /// </summary>
    public partial class CouponTemplateStockService : DomainServiceBase<IRepository<CouponTemplateStock>, CouponTemplateStock, Guid>, ICouponTemplateStockService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CouponTemplateStockService"/> class.
        /// </summary>
        public CouponTemplateStockService()
        {
        }

        public bool UpdateStock(CouponTemplateDto entity)
        {
            var stock = this.Get(entity.ID);
            if (stock != null)
            {
                if ((stock.Stock + entity.Stock) < 0)
                {
                    return false;
                }
                stock.Stock += entity.Stock;
                return base.Update(stock);
            }
            return false;
        }
    }
}
