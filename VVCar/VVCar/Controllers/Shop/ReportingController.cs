using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VVCar.Common;
using VVCar.Shop.Domain.Dtos;
using VVCar.Shop.Domain.Entities;
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
        [HttpGet, Route("ProductRetailStatistics"), AllowAnonymous]
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
        /// 数据分析
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet, Route("DataAnalyseList"), AllowAnonymous]

        public PagedActionResultForAnalyse<DataAnalyseDto> DataAnalyse([FromUri]DataAnalyseFilter filter)
        {
            return SafeGetPagedDataForAnalyse<DataAnalyseDto>((result) =>
            {
                var totalCount = 0;
                var data = ReportingService.DataAnalyseList(filter, ref totalCount);
                var sum_quantity = 0;
                decimal sum_money = 0;
                var dataAnalyseList = data.ToList();
                foreach (var dataAnalyse in dataAnalyseList)
                {
                    sum_quantity += dataAnalyse.TotalQuantity;
                    sum_money += dataAnalyse.TotalMoney;
                }
                DataAnalyseDto dataAnalyseDto = new DataAnalyseDto();
                dataAnalyseDto.MemberName = "合计:";
                dataAnalyseDto.MemberMobilePhone = "";
                dataAnalyseDto.TotalQuantity = sum_quantity;
                dataAnalyseDto.TotalMoney = sum_money;
                (data as List<DataAnalyseDto>).Add(dataAnalyseDto);
                result.Data = data;
                result.TotalCount = totalCount;
                result.TotalMemberCount = ReportingService.GetMemberTotalCount();
            });
        }

        private PagedActionResultForAnalyse<TResult> SafeGetPagedDataForAnalyse<TResult>(Action<PagedActionResultForAnalyse<TResult>> execAction)
        {
            var result = new PagedActionResultForAnalyse<TResult>();
            try
            {
                execAction(result);
            }
            catch (Exception ex)
            {
                YEF.Core.Logging.LoggerManager.GetLogger().Error(ex.ToString());
                result.IsSuccessful = false;
                result.TotalCount = 0;
                result.TotalMemberCount = 0;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 畅销/滞销产品
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("UnsaleProductHistory"), AllowAnonymous]
        public PagedActionResult<UnsaleProductHistoryDto> UnsaleProductHistory([FromUri]UnsaleProductHistoryFilter filter)
        {
            return SafeGetPagedData<UnsaleProductHistoryDto>((result) =>
            {
                var totalCount = 0;
                var data = ReportingService.UnsaleProductHistory(filter, ref totalCount);
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
        /// 员工个人产值报表
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet, Route("StaffOutputValueStatistics")]
        public PagedActionResult<StaffOutputValue> StaffOutputValueStatistics([FromUri]StaffOutputValueFilter filter)
        {
            return SafeGetPagedData<StaffOutputValue>((result) =>
            {
                var totalCount = 0;
                var data = ReportingService.StaffOutputValueStatistics(filter, out totalCount);
                var staffPerformanceStatisticsList = data.ToList();
                StaffOutputValue staffOutputValue = new StaffOutputValue();
                staffPerformanceStatisticsList.ForEach(t =>
                {
                    staffOutputValue.TotalPerformance += t.TotalPerformance;
                    staffOutputValue.TotalCostMoney += t.TotalCostMoney;
                    staffOutputValue.TotalProfit += t.TotalProfit;
                    staffOutputValue.DailyExpense += t.DailyExpense;
                    staffOutputValue.AverageDailyExpense += t.AverageDailyExpense;
                    staffOutputValue.TotalRetaainedProfit += t.TotalRetaainedProfit;
                });
                staffOutputValue.StaffName = "合计:";
                (data as List<StaffOutputValue>).Add(staffOutputValue);
                result.Data = data;
                result.TotalCount = totalCount;
            });
        }

        /// <summary>
        /// 员工个人产值报表
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet, Route("ExportStaffOutputValueStatistics")]
        public JsonActionResult<string> ExportStaffOutputValueStatistics([FromUri]StaffOutputValueFilter filter)
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
                var data = ReportingService.StaffOutputValueStatistics(filter, out totalCount);
                var staffOutputValueStatisticsList = data.ToList();
                StaffOutputValue staffOutputValue = new StaffOutputValue();
                staffOutputValueStatisticsList.ForEach(t =>
                {
                    staffOutputValue.TotalPerformance += t.TotalPerformance;
                    staffOutputValue.TotalCostMoney += t.TotalCostMoney;
                    staffOutputValue.TotalProfit += t.TotalProfit;
                    staffOutputValue.DailyExpense += t.DailyExpense;
                    staffOutputValue.AverageDailyExpense += t.AverageDailyExpense;
                    staffOutputValue.TotalRetaainedProfit += t.TotalRetaainedProfit;
                });
                staffOutputValue.StaffName = "合计:";
                var exporter = new ExportHelper(new[]
                {
                    new ExportInfo("StaffName", "员工姓名"),
                    new ExportInfo("StaffCode","员工编码"),
                    new ExportInfo("TotalPerformance","总业绩"),
                    new ExportInfo("TotalCostMoney","总成本"),
                    new ExportInfo("TotalProfit","总利润"),
                    new ExportInfo("DailyExpense","总日常支出"),
                    new ExportInfo("AverageDailyExpense","平摊日常支出"),
                    new ExportInfo("TotalRetaainedProfit","净利润"),
                });
                (data as List<StaffOutputValue>).Add(staffOutputValue);
                return exporter.Export(data.ToList(), "员工个人产值统计");
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
                    new ExportInfo("Unit","单位"),
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
        /// 消费历史记录导入模板
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("ImportConsumeHistoryDataTemplate")]
        public JsonActionResult<string> ImportConsumeHistoryDataTemplate()
        {
            return SafeExecute(() =>
            {
                var templateData = new List<ConsumeHistory>();
                var exportInfos = new[]
                 {
                    new ExportInfo("Name", "客户名称"),
                    new ExportInfo("PlateNumber", "车牌号码"),
                    new ExportInfo("TradeNo", "单据编号"),
                    new ExportInfo("MobilePhoneNo", "电话号码"),
                    new ExportInfo("Consumption", "消费项目"),
                    new ExportInfo("TradeCount", "交易数量"),
                    new ExportInfo("Unit", "单位"),
                    new ExportInfo("Price", "单价"),
                    new ExportInfo("TradeMoney", "金额"),
                    new ExportInfo("BasePrice", "商品成本"),
                    new ExportInfo("GrossProfit", "毛利"),
                    new ExportInfo("Remark", "备注"),
                    new ExportInfo("DepartmentName", "门店"),
                    new ExportInfo("CreatedDate", "消费时间"),

                };
                var eh = new ExportHelper(exportInfos);
                return eh.ImportTemplate("消费历史记录导入模板");
            });
        }

        /// <summary>
        /// 消费记录导入
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        [HttpGet, Route("ImportConsumeHistoryData")]
        public JsonActionResult<bool> ImportConsumeHistoryData(string fileName)
        {
            return SafeExecute(() =>
            {
                var targetDir = Path.Combine(AppContext.PathInfo.AppDataPath, "Upload/Excel/ConsumeHistory");
                string targetPath = Path.Combine(targetDir, fileName);

                var excelFieldInfos = new[]
                {
                    new ExcelFieldInfo(0, "Name", "客户名称", true),
                    new ExcelFieldInfo(1, "PlateNumber", "车牌号码", true),
                    new ExcelFieldInfo(2, "TradeNo", "单据编号", true),
                    new ExcelFieldInfo(3, "MobilePhoneNo", "电话号码", true),
                    new ExcelFieldInfo(4, "Consumption", "消费项目", true),
                    new ExcelFieldInfo(5, "TradeCount", "消费数量", true),
                    new ExcelFieldInfo(6, "Unit", "单位", true),
                    new ExcelFieldInfo(7, "Price", "单价", true),
                    new ExcelFieldInfo(8, "TradeMoney", "金额", true),
                    new ExcelFieldInfo(9, "BasePrice", "商品成本", true),
                    new ExcelFieldInfo(10, "GrossProfit", "毛利", true),
                    new ExcelFieldInfo(11, "Remark", "备注", true),
                    new ExcelFieldInfo(12, "DepartmentName", "门店", true),
                    new ExcelFieldInfo(13, "CreatedDate", "消费时间", true)
                };
                var data = ExcelHelper.ImportFromExcel<ConsumeHistory>(targetPath, excelFieldInfos);
                if (data == null || data.Count < 1)
                    throw new DomainException("没有可以导入的数据");
                return ReportingService.ImportConsumeHistoryData(data);
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
                    new ExportInfo("TradeNo","交易单号"),
                    new ExportInfo("Name", "姓名"),
                    new ExportInfo("MobilePhoneNo","手机号"),
                    new ExportInfo("PlateNumber","车牌号"),
                    new ExportInfo("Consumption", "消费项目"),
                    new ExportInfo("TradeCount", "消费数量"),
                    new ExportInfo("Unit", "单位"),
                    new ExportInfo("Price", "单价"),
                    new ExportInfo("TradeMoney","交易金额"),
                    new ExportInfo("Source","交易类型"),
                    new ExportInfo("BasePrice", "商品成本"),
                    new ExportInfo("GrossProfit", "毛利"),
                    new ExportInfo("Remark", "备注"),
                    new ExportInfo("CreatedDate","交易时间"),
                });
                (data as List<ConsumeHistoryDto>).Add(consumeHistoryDto);
                return exporter.Export(data.ToList(), "员工业绩汇总统计");
            });
        }

        /// <summary>
        /// 获取代理商门店开发报表
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetOpenAccountReporting"), AllowAnonymous]
        public JsonActionResult<OpenAccountReportingDto> GetOpenAccountReporting()
        {
            return SafeExecute(() =>
            {
                return ReportingService.GetOpenAccountReporting();
            });
        }

        /// <summary>
        /// 获取月开发门店业绩
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [HttpGet, Route("GetMonthOpenAccountPerformance"), AllowAnonymous]
        public JsonActionResult<MonthOpenAccountPerformanceDto> GetMonthOpenAccountPerformance(DateTime date)
        {
            return SafeExecute(() =>
            {
                return ReportingService.GetMonthOpenAccountPerformance(date);
            });
        }

        /// <summary>
        /// 营业报表
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet, Route("GetOperationStatement"), AllowAnonymous]
        public PagedActionResult<OperationStatementDto> GetOperationStatement([FromUri]OperationStatementFilter filter)
        {
            return SafeGetPagedData<OperationStatementDto>((result) =>
            {
                var totalCount = 0;
                var data = ReportingService.GetOperationStatement(filter, out totalCount).ToList();
                var operationStatementList = data.ToList();
                OperationStatementDto operationStatement = new OperationStatementDto();
                operationStatementList.ForEach(t =>
                {
                    operationStatement.TotalInCome += t.TotalInCome;
                    operationStatement.TotalOutCome += t.TotalOutCome;
                });
                operationStatement.Code = "合计:";
                data.Add(operationStatement);
                result.Data = data;
                result.TotalCount = totalCount;
            });
        }

        /// <summary>
        /// 营业报表统计导出
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet, Route("ExtportOperationStatement")]
        public JsonActionResult<string> ExtportOperationStatement([FromUri]OperationStatementFilter filter)
        {
            return SafeExecute(() =>
            {
                var totalCount = 0;
                filter.Start = null;
                filter.Limit = null;
                var operationStatementList = ReportingService.GetOperationStatement(filter, out totalCount).ToList();
                var operationStatementDto = new OperationStatementDto();
                operationStatementList.ForEach(t =>
                {
                    operationStatementDto.TotalInCome += t.TotalInCome;
                    operationStatementDto.TotalOutCome += t.TotalOutCome;

                });
                operationStatementDto.Code = "合计:";
                var exporter = new ExportHelper(new[]
                {
                    new ExportInfo("Code","时间"),
                    new ExportInfo("TotalInCome", "总收入"),
                    new ExportInfo("TotalOutCome","总支出"),
                    new ExportInfo("TotalProfit","利润"),
                });
                operationStatementList.Add(operationStatementDto);
                return exporter.Export(operationStatementList, "营业报表统计导出");
            });
        }

        /// <summary>
        /// 营业报表 详情
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet, Route("GetOperationStatementDetail"), AllowAnonymous]
        public PagedActionResult<OperationStatementDetailDto> GetOperationStatementDetail([FromUri]OperationStatementFilter filter)
        {
            return SafeGetPagedData<OperationStatementDetailDto>((result) =>
            {
                var totalCount = 0;
                var data = ReportingService.GetOperationStatementDetail(filter, out totalCount).ToList();
                var operationStatementDetail = new OperationStatementDetailDto();
                data.ForEach(t =>
                {
                    if(t.BudgetType == EBudgetType.InCome)
                        operationStatementDetail.Money += t.Money;
                    if (t.BudgetType == EBudgetType.OutCome)
                        operationStatementDetail.Money -= t.Money;
                });
                operationStatementDetail.TradeNo = "合计:";
                data.Add(operationStatementDetail);
                result.Data = data;
                result.TotalCount = totalCount;
            });
        }
    }
}
