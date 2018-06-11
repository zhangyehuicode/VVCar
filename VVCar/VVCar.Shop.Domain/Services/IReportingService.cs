using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.Shop.Domain.Dtos;
using VVCar.Shop.Domain.Filters;
using YEF.Core;

namespace VVCar.Shop.Domain.Services
{
    /// <summary>
    /// 报表服务接口
    /// </summary>
    public interface IReportingService : IDependency
    {
        /// <summary>
        /// 营业额报表
        /// </summary>
        /// <returns></returns>
        TurnoverReportingDto GetTurnoverReporting();

        /// <summary>
        /// 月营业额
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        MonthTurnoverDto GetMonthTurnover(DateTime date);

        /// <summary>
        /// 获取员工绩效
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        StaffPerformance GetStaffPerformance(Guid userId, DateTime? date);

        /// <summary>
        /// 零售产品汇总统计
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        IEnumerable<ProductRetailStatisticsDto> ProductRetailStatistics(ProductRetailStatisticsFilter filter, ref int totalCount);

        /// <summary>
        /// 员工业绩统计
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        IEnumerable<StaffPerformance> StaffPerformanceStatistics(StaffPerformanceFilter filter, ref int totalCount);
    }
}
