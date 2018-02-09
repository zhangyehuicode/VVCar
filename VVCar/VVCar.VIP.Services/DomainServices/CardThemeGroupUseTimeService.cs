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
    /// 卡片主题适用时段领域服务
    /// </summary>
    public partial class CardThemeGroupUseTimeService : DomainServiceBase<IRepository<CardThemeGroupUseTime>, CardThemeGroupUseTime, Guid>, ICardThemeGroupUseTimeService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CardThemeGroupUseTimeService"/> class.
        /// </summary>
        public CardThemeGroupUseTimeService()
        {
        }

        public override CardThemeGroupUseTime Add(CardThemeGroupUseTime entity)
        {
            entity.ID = Util.NewID();
            return base.Add(entity);
        }
    }
}
