using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VVCar.Common;
using VVCar.VIP.Domain.Dtos;
using VVCar.VIP.Domain.Filters;
using VVCar.VIP.Domain.Services;
using YEF.Core;
using YEF.Core.Dtos;
using YEF.Core.Export;

namespace VVCar.Controllers.Api
{
    /// <summary>
    /// 消费记录
    /// </summary>
    [RoutePrefix("api/TradeHistory")]
    public class TradeHistoryController : BaseApiController
    {
        #region ctor.

        /// <summary>
        /// 消费记录
        /// </summary>
        /// <param name="tradeHistoryService"></param>
        public TradeHistoryController(ITradeHistoryService tradeHistoryService)
        {
            this.TradeHistoryService = tradeHistoryService;
        }

        #endregion

        #region properties

        ITradeHistoryService TradeHistoryService { get; set; }

        #endregion

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter">过滤条件</param>
        /// <returns></returns>
        [HttpGet]
        public PagedActionResult<TradeHistoryDto> Search([FromUri]HistoryFilter filter)
        {
            return SafeGetPagedData<TradeHistoryDto>((result) =>
            {
                if (!ModelState.IsValid)//表示没有过滤参数成功匹配，判定为错误请求。
                {
                    throw new DomainException("查询参数错误。");
                }
                int totalCount = 0;
                var pagedData = this.TradeHistoryService.Search(filter, out totalCount);
                result.Data = pagedData;
                result.TotalCount = totalCount;
            });
        }

        /// <summary>
        /// 导出储值记录
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        /// <exception cref="DomainException"></exception>
        [HttpGet]
        [Route("exportTradeHistory")]
        public JsonActionResult<string> ExportTradeHistory([FromUri]HistoryFilter filter)
        {
            return SafeExecute(() =>
            {
                if (!ModelState.IsValid)//表示没有过滤参数成功匹配，判定为错误请求。
                {
                    throw new DomainException("查询参数错误。");
                }

                filter.Start = 0;
                filter.Limit = 1000000;
                int totalCount;
                return ExecuteExport(TradeHistoryService.Search(filter, out totalCount));
            });
        }

        private static string ExecuteExport(IEnumerable<TradeHistoryDto> entities)
        {
            var exportHelper = new ExportHelper(new[]
            {
                new ExportInfo("TradeNo", "交易流水号"),
                new ExportInfo("CardNumber", "会员卡号"),
                //new ExportInfo("CardTypeDesc","卡片类型"),
                new ExportInfo("MemberName", "会员姓名"),
                new ExportInfo("TradeAmount", "消费金额"),
                new ExportInfo("OutTradeNo", "外部订单号"),
                new ExportInfo("TradeSource", "交易来源"),
                //new ExportInfo("ConsumeTypeDesc", "消费类型"),
                new ExportInfo("UseBalanceAmount", "会员余额支付金额"),
                new ExportInfo("CreatedDate", "消费时间"),
                new ExportInfo("TradeDepartment", "消费门店"),
                new ExportInfo("CreatedUser", "业务员"),
                new ExportInfo("BusinessType", "业务类型"),
                //new ExportInfo("CardRemark", "卡片备注"),
            });
            return exportHelper.Export(entities.ToList(), "会员消费记录");
        }
    }
}
