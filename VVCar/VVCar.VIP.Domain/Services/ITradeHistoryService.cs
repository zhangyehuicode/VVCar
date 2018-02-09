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
    /// 消费记录 领域服务接口
    /// </summary>
    public partial interface ITradeHistoryService : IDomainService<IRepository<TradeHistory>, TradeHistory, Guid>
    {
        /// <summary>
        /// 查询消费记录
        /// </summary>
        /// <param name="filter">过滤条件</param>
        /// <param name="totalCount">总记录数</param>
        /// <returns></returns>
        IEnumerable<TradeHistoryDto> Search(HistoryFilter filter, out int totalCount);
    }
}
