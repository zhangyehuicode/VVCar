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
    /// 寻客侠广告浏览记录领域服务
    /// </summary>
    public partial interface IAdvisementBrowseHistoryService : IDomainService<IRepository<AdvisementBrowseHistory>, AdvisementBrowseHistory, Guid>
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        IEnumerable<AdvisementBrowseHistoryDto> Search(AdvisementBrowseHistoryFilter filter, out int totalCount);
    }
}
