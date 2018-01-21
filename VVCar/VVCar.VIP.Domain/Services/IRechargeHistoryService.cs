using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VVCar.VIP.Domain.Entities;
using YEF.Core.Data;
using YEF.Core.Domain;

namespace VVCar.VIP.Domain.Services
{
    /// <summary>
    /// 储值记录 领域服务接口
    /// </summary>
    public partial interface IRechargeHistoryService : IDomainService<IRepository<RechargeHistory>, RechargeHistory, Guid>
    {
        ///// <summary>
        ///// 查询储值纪录
        ///// </summary>
        ///// <param name="filter">过滤条件</param>
        ///// <param name="totalCount">总记录数</param>
        ///// <returns></returns>
        //IEnumerable<TradeHistoryDto> Search(HistoryFilter filter, out int totalCount);

        /// <summary>
        /// 最后一次充值金额
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        decimal LastRecharge(Expression<Func<RechargeHistory, bool>> predicate);

        ///// <summary>
        ///// 获取储值统计信息
        ///// </summary>
        ///// <param name="filter"></param>
        //RechargeTotalDataDto GetTotalData(RechargeHistoryFilter filter);

        ///// <summary>
        ///// 次数
        ///// </summary>
        ///// <param name="predicate"></param>
        ///// <returns></returns>
        //int Count(Expression<Func<RechargeHistory, bool>> predicate);

        ///// <summary>
        ///// 开发票
        ///// </summary>
        ///// <param name="entity"></param>
        ///// <returns></returns>
        //bool DrawReceipt(RechargeHistory entity);
    }
}
