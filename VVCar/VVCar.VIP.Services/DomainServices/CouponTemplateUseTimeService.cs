using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.VIP.Domain.Entities;
using VVCar.VIP.Domain.Services;
using YEF.Core;
using YEF.Core.Data;
using YEF.Core.Domain;

namespace VVCar.VIP.Services.DomainServices
{
    /// <summary>
    /// 卡券模板可用时段 领域服务
    /// </summary>
    public partial class CouponTemplateUseTimeService : DomainServiceBase<IRepository<CouponTemplateUseTime>, CouponTemplateUseTime, Guid>, ICouponTemplateUseTimeService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CouponTemplateUseTimeService"/> class.
        /// </summary>
        public CouponTemplateUseTimeService()
        {
        }

        public override CouponTemplateUseTime Add(CouponTemplateUseTime entity)
        {
            entity.ID = Util.NewID();
            return base.Add(entity);
        }
    }
}
