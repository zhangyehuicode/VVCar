using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.VIP.Domain.Entities;
using YEF.Core.Data;
using YEF.Core.Domain;

namespace VVCar.VIP.Domain.Services
{
    /// <summary>
    /// 卡片主题适用时段领域服务接口
    /// </summary>
    public partial interface ICardThemeGroupUseTimeService : IDomainService<IRepository<CardThemeGroupUseTime>, CardThemeGroupUseTime, Guid>
    {
    }
}
