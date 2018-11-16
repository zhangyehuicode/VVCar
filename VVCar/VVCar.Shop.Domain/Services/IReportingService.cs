using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.Shop.Domain.Dtos;
using VVCar.Shop.Domain.Entities;
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
        /// 数据分析
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        IEnumerable<DataAnalyseDto> DataAnalyseList(DataAnalyseFilter filter, ref int totalCount);

        /// <summary>
        /// 滞销产品提醒
        /// </summary>
        /// <returns></returns>
        IEnumerable<UnsaleProductHistoryDto> UnsaleProductHistory(UnsaleProductHistoryFilter filter, ref int totalCount);

        /// <summary>
        /// 员工业绩统计
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        IEnumerable<StaffPerformance> StaffPerformanceStatistics(StaffPerformanceFilter filter, ref int totalCount);

        /// <summary>
        /// 门店开发业绩统计
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        IEnumerable<DepartmentPerformance> DepartmentPerformanceStatistics(DepartmentPerformanceFilter filter, ref int totalCount);

        /// <summary>
        /// 消费记录
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        IEnumerable<ConsumeHistoryDto> GetConsumeHistory(ConsumeHistoryFilter filter, ref int totalCount);

        /// <summary>
        /// 获取代理商门店开发报表
        /// </summary>
        /// <returns></returns>
        OpenAccountReportingDto GetOpenAccountReporting();

        /// <summary>
        /// 获取月开发门店业绩
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        MonthOpenAccountPerformanceDto GetMonthOpenAccountPerformance(DateTime date);

        /// <summary>
        /// 导入消费历史记录
        /// </summary>
        /// <param name="consumeHistories"></param>
        /// <returns></returns>
        bool ImportConsumeHistoryData(IEnumerable<ConsumeHistory> consumeHistories);

        /// <summary>
        /// 营业报表
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        IEnumerable<OperationStatementDto> GetOperationStatement(OperationStatementFilter filter, out int totalCount);

        /// <summary>
        /// 营业报表详情
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        IEnumerable<OperationStatementDetailDto> GetOperationStatementDetail(OperationStatementFilter filter, out int totalCount);   
    }
}
