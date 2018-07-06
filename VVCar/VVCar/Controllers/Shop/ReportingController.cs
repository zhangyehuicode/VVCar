using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VVCar.Common;
using VVCar.Shop.Domain.Dtos;
using VVCar.Shop.Domain.Enums;
using VVCar.Shop.Domain.Filters;
using VVCar.Shop.Domain.Services;
using YEF.Core;
using YEF.Core.Dtos;
using YEF.Core.Export;

namespace VVCar.Controllers.Shop
{
    /// <summary>
    /// 报表
    /// </summary>
    [RoutePrefix("api/Reporting")]
    public class ReportingController : BaseApiController
    {
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="reportingService"></param>
        public ReportingController(IReportingService reportingService)
        {
            ReportingService = reportingService;
        }

        IReportingService ReportingService { get; set; }

        /// <summary>
        /// 营业报表
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetTurnoverReporting"), AllowAnonymous]
        public JsonActionResult<TurnoverReportingDto> GetTurnoverReporting()
        {
            return SafeExecute(() =>
            {
                return ReportingService.GetTurnoverReporting();
            });
        }

        /// <summary>
        /// 月营业额
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [HttpGet, Route("GetMonthTurnover"), AllowAnonymous]
        public JsonActionResult<MonthTurnoverDto> GetMonthTurnover(DateTime date)
        {
            return SafeExecute(() =>
            {
                return ReportingService.GetMonthTurnover(date);
            });
        }

        /// <summary>
        /// 获取员工绩效
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        [HttpGet, Route("GetStaffPerformance"), AllowAnonymous]
        public JsonActionResult<StaffPerformance> GetStaffPerformance(Guid userId, DateTime? date)
        {
            return SafeExecute(() =>
            {
                return ReportingService.GetStaffPerformance(userId, date);
            });
        }

        /// <summary>
        /// 零售产品汇总统计
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet, Route("ProductRetailStatistics")]
        public PagedActionResult<ProductRetailStatisticsDto> ProductRetailStatistics([FromUri]ProductRetailStatisticsFilter filter)
        {
            return SafeGetPagedData<ProductRetailStatisticsDto>((result) =>
            {
                var totalCount = 0;
                var data = ReportingService.ProductRetailStatistics(filter, ref totalCount);
                var sum_quantity = 0;
                decimal sum_money = 0;
                var productRetailStatisticsList = data.ToList();
                foreach (var productRetailStatistics in productRetailStatisticsList)
                {
                    sum_quantity += productRetailStatistics.Quantity;
                    sum_money += productRetailStatistics.Money;
                }
                ProductRetailStatisticsDto productRetailStatisticsDto = new ProductRetailStatisticsDto();
                productRetailStatisticsDto.ProductName = "合计:";
                productRetailStatisticsDto.ProductCode = "";
                productRetailStatisticsDto.Quantity = sum_quantity;
                productRetailStatisticsDto.Money = sum_money;
                (data as List<ProductRetailStatisticsDto>).Add(productRetailStatisticsDto);
                result.Data = data;
                result.TotalCount = totalCount;
            });
        }

        /// <summary>
        /// 员工业绩统计
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet, Route("StaffPerformanceStatistics")]
        public PagedActionResult<StaffPerformance> StaffPerformanceStatistics([FromUri]StaffPerformanceFilter filter)
        {
            return SafeGetPagedData<StaffPerformance>((result) =>
            {
                var totalCount = 0;
                filter.Start = null;
                filter.Limit = null;
                var data = ReportingService.StaffPerformanceStatistics(filter, ref totalCount);
                var staffPerformanceStatisticsList = data.ToList();
                StaffPerformance staffPerformance = new StaffPerformance();
                staffPerformanceStatisticsList.ForEach(t =>
                {
                    staffPerformance.TotalPerformance += t.TotalPerformance;
                    staffPerformance.TotalCommission += t.TotalCommission;
                    staffPerformance.CurrentPerformance += t.CurrentPerformance;
                    staffPerformance.MonthCommission += t.MonthCommission;
                    staffPerformance.CurrentCommission += t.CurrentCommission;
                    staffPerformance.MonthCommission += t.MonthCommission;
                    staffPerformance.CustomerServiceCount += t.CustomerServiceCount;
                    staffPerformance.CurrentCustomerServiceCount += t.CurrentCustomerServiceCount;
                    staffPerformance.MonthCustomerServiceCount += t.MonthCustomerServiceCount;
                });
                staffPerformance.StaffName = "合计:";
                (data as List<StaffPerformance>).Add(staffPerformance);
                result.Data = data;
                result.TotalCount = totalCount;
            });
        }

        /// <summary>
        /// 零售产品汇总统计导出
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet, Route("ExportProductRetailStatistics")]
        public JsonActionResult<string> ExportProductRetailStatistics([FromUri]ProductRetailStatisticsFilter filter)
        {
            return SafeExecute(() =>
            {
                if (!ModelState.IsValid)
                {
                    throw new DomainException("查询参数错误。");
                }
                var totalCount = 0;
                filter.Start = null;
                filter.Limit = null;
                var data = ReportingService.ProductRetailStatistics(filter, ref totalCount);
                var sum_quantity = 0;
                decimal sum_money = 0;
                var productRetailStatisticsList = data.ToList();
                foreach (var productRetailStatistics in productRetailStatisticsList)
                {
                    sum_quantity += productRetailStatistics.Quantity;
                    sum_money += productRetailStatistics.Money;
                }
                ProductRetailStatisticsDto productRetailStatisticsDto = new ProductRetailStatisticsDto();
                productRetailStatisticsDto.ProductName = "合计:";
                productRetailStatisticsDto.ProductCode = "";
                productRetailStatisticsDto.ProductCategoryName = "";
                productRetailStatisticsDto.ProductType = "";
                productRetailStatisticsDto.Quantity = sum_quantity;
                productRetailStatisticsDto.Money = sum_money;
                (data as List<ProductRetailStatisticsDto>).Add(productRetailStatisticsDto);
                var exporter = new ExportHelper(new[]
                {
                    new ExportInfo("ProductType", "产品类别"),
                    new ExportInfo("ProductCategoryName","产品类别名称"),
                    new ExportInfo("ProductName","产品名称"),
                    new ExportInfo("ProductCode","产品编码"),
                    new ExportInfo("Quantity","销售数量"),
                    new ExportInfo("Unit","单位"    ),
                    new ExportInfo("Money","销售总额"),
                });
                return exporter.Export(data.ToList(), "零售产品汇总统计");
            });
        }

        /// <summary>
        /// 员工业绩汇总统计导出
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet, Route("ExportStaffPerformanceStatistics")]
        public JsonActionResult<string> ExportStaffPerformanceStatistics([FromUri]StaffPerformanceFilter filter)
        {
            return SafeExecute(() =>
            {
                if (!ModelState.IsValid)
                {
                    throw new DomainException("查询参数错误");
                }
                var totalCount = 0;
                filter.Start = null;
                filter.Limit = null;
                var data = ReportingService.StaffPerformanceStatistics(filter, ref totalCount);
                var staffPerformanceStatisticsList = data.ToList();
                StaffPerformance staffPerformance = new StaffPerformance();
                staffPerformanceStatisticsList.ForEach(t =>
                {
                    staffPerformance.TotalPerformance += t.TotalPerformance;
                    staffPerformance.TotalCommission += t.TotalCommission;
                    staffPerformance.CurrentPerformance += t.CurrentPerformance;
                    staffPerformance.MonthCommission += t.MonthCommission;
                    staffPerformance.CurrentCommission += t.CurrentCommission;
                    staffPerformance.MonthCommission += t.MonthCommission;
                    staffPerformance.CustomerServiceCount += t.CustomerServiceCount;
                    staffPerformance.CurrentCustomerServiceCount += t.CurrentCustomerServiceCount;
                    staffPerformance.MonthCustomerServiceCount += t.MonthCustomerServiceCount;
                });
                staffPerformance.StaffName = "合计:";
                var exporter = new ExportHelper(new[]
                {
                    new ExportInfo("StaffName", "员工姓名"),
                    new ExportInfo("StaffCode","员工编码"),
                    new ExportInfo("TotalPerformance","总业绩"),
                    new ExportInfo("TotalCommission","总抽成"),
                    new ExportInfo("CurrentPerformance","当前业绩"),
                    new ExportInfo("MonthPerformance","当月业绩"),
                    new ExportInfo("CurrentCommission","当前抽成"),
                    new ExportInfo("MonthCommission","当月抽成"),
                    new ExportInfo("CustomerServiceCount","客户服务量"),
                    new ExportInfo("CurrentCustomerServiceCount","当前客户服务量"),
                    new ExportInfo("MonthCustomerServiceCount","月客户服务量"),
                });
                (data as List<StaffPerformance>).Add(staffPerformance);
                return exporter.Export(data.ToList(), "员工业绩汇总统计");
            });
        }

        /// <summary>
        /// 门店开发业绩统计
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet, Route("DepartmentPerformanceStatistics")]
        public PagedActionResult<DepartmentPerformance> DepartmentPerformanceStatistics([FromUri]DepartmentPerformanceFilter filter)
        {
            return SafeGetPagedData<DepartmentPerformance>((result) =>
            {
                var totalCount = 0;
                filter.Start = null;
                filter.Limit = null;
                var data = ReportingService.DepartmentPerformanceStatistics(filter, ref totalCount).ToList();
                var departmentPerformance = new DepartmentPerformance();
                var departmentPerformanceStatisticsList = data.ToList();
                departmentPerformanceStatisticsList.ForEach(t =>
                {
                    departmentPerformance.TotalDepartmentNumber += t.TotalDepartmentNumber;
                    departmentPerformance.CurrentDepartmentNumber += t.CurrentDepartmentNumber;
                    departmentPerformance.MonthDepartmentNumber += t.MonthDepartmentNumber;
                });
                departmentPerformance.StaffName = "合计:";
                data.Add(departmentPerformance);
                result.Data = data;
                result.TotalCount = totalCount;
            });
        }

        /// <summary>
        /// 门店开发业绩统计导出
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet, Route("ExportDepartmentPerformanceStatistics")]
        public JsonActionResult<string> ExportDepartmentPerformanceStatistics([FromUri]DepartmentPerformanceFilter filter)
        {
            return SafeExecute(() =>
            {
                if (!ModelState.IsValid)
                {
                    throw new DomainException("查询参数错误");
                }
                var totalCount = 0;
                filter.Start = null;
                filter.Limit = null;
                var data = ReportingService.DepartmentPerformanceStatistics(filter, ref totalCount);
                var departmentPerformanceStatisticsList = data.ToList();
                var departmentPerformance = new DepartmentPerformance();
                departmentPerformanceStatisticsList.ForEach(t =>
                {
                    departmentPerformance.TotalDepartmentNumber += t.TotalDepartmentNumber;
                    departmentPerformance.CurrentDepartmentNumber += t.CurrentDepartmentNumber;
                    departmentPerformance.MonthDepartmentNumber += t.MonthDepartmentNumber;
                });
                departmentPerformance.StaffName = "合计:";
                var exporter = new ExportHelper(new[]
                {
                    new ExportInfo("StaffName", "员工姓名"),
                    new ExportInfo("StaffCode","员工编码"),
                    new ExportInfo("TotalDepartmentNumber","总开发门店数量"),
                    new ExportInfo("CurrentDepartmentNumber","当前开发门店数量"),
                    new ExportInfo("MonthDepartmentNumber","当月开发门店数量"),
                });
                (data as List<DepartmentPerformance>).Add(departmentPerformance);
                return exporter.Export(data.ToList(), "门店开发业绩汇总统计");
            });
        }

        /// <summary>
        /// 获取消费记录
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet, Route("GetConsumeHistory")]
        public PagedActionResult<ConsumeHistoryDto> GetConsumeHistory([FromUri]ConsumeHistoryFilter filter)
        {
            return SafeGetPagedData<ConsumeHistoryDto>((result) =>
            {
                var totalCount = 0;
                var data = ReportingService.GetConsumeHistory(filter, ref totalCount);
                result.Data = data;
                result.TotalCount = totalCount;
            });
        }


        /// <summary>
        /// 员工业绩汇总统计导出
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet, Route("ExportConsumeHistory")]
        public JsonActionResult<string> ExportConsumeHistory([FromUri]ConsumeHistoryFilter filter)
        {
            return SafeExecute(() =>
            {
                if (!ModelState.IsValid)
                {
                    throw new DomainException("查询参数错误");
                }
                var totalCount = 0;
                filter.Start = null;
                filter.Limit = null;
                var data = ReportingService.GetConsumeHistory(filter, ref totalCount);
                var consumeHistoryList = data.ToList();
                ConsumeHistoryDto consumeHistoryDto = new ConsumeHistoryDto();
                consumeHistoryList.ForEach(t =>
                {
                    consumeHistoryDto.TradeMoney += t.TradeMoney;
                });
                consumeHistoryDto.Name = "合计:";
                consumeHistoryDto.MobilePhoneNo = "";
                consumeHistoryDto.PlateNumber = "";
                consumeHistoryDto.Source = null;
                consumeHistoryDto.TradeNo = "";
                consumeHistoryDto.CreatedDate = null;
                var exporter = new ExportHelper(new[]
                {
                    new ExportInfo("Name", "姓名"),
                    new ExportInfo("MobilePhoneNo","手机号"),
                    new ExportInfo("PlateNumber","车牌号"),
                    new ExportInfo("TradeNo","交易单号"),
                    new ExportInfo("TradeMoney","交易金额"),
                    new ExportInfo("Source","交易类型"),
                    new ExportInfo("CreatedDate","交易时间"),
                });
                (data as List<ConsumeHistoryDto>).Add(consumeHistoryDto);
                return exporter.Export(data.ToList(), "员工业绩汇总统计");
            });
        }
    }
}
