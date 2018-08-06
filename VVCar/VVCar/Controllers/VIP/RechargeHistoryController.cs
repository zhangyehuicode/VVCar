using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VVCar.Common;
using VVCar.VIP.Domain.Dtos;
using VVCar.VIP.Domain.Entities;
using VVCar.VIP.Domain.Filters;
using VVCar.VIP.Domain.Services;
using YEF.Core;
using YEF.Core.Dtos;
using YEF.Core.Export;

namespace VVCar.Controllers.VIP
{
    /// <summary>
    /// 充值记录
    /// </summary>
    [RoutePrefix("api/RechargeHistory")]
    public class RechargeHistoryController : BaseApiController
    {
        #region ctor.

        /// <summary>
        /// 充值记录
        /// </summary>
        /// <param name="rechargeHistoryService"></param>
        public RechargeHistoryController(IRechargeHistoryService rechargeHistoryService)
        {
            this.RechargeHistoryService = rechargeHistoryService;
        }

        #endregion

        #region properties
        IRechargeHistoryService RechargeHistoryService { get; set; }
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
                var pagedData = this.RechargeHistoryService.Search(filter, out totalCount);
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
                return ExecuteExport(RechargeHistoryService.Search(filter, out totalCount));
            });
        }

        private static string ExecuteExport(IEnumerable<TradeHistoryDto> entities)
        {
            var exportHelper = new ExportHelper(new[]
            {
                new ExportInfo("TradeNo", "交易流水号"),
                new ExportInfo("CardNumber", "会员卡号"),
                new ExportInfo("MemberName", "会员姓名"),
                new ExportInfo("TradeAmount", "储值金额"),
                new ExportInfo("GiveAmount", "赠送金额"),
                new ExportInfo("PaymentType", "支付方式"),
                new ExportInfo("TradeSource", "交易来源"),
                new ExportInfo("CreatedDate", "储值时间"),
                new ExportInfo("TradeDepartment", "储值门店"),
                new ExportInfo("CreatedUser", "业务员"),
                //new ExportInfo("HasDrawReceipt", "是否开票"),
                //new ExportInfo("DrawReceiptMoney", "开票金额"),
                //new ExportInfo("DrawReceiptUser", "开票人"),
                //new ExportInfo("DrawReceiptDepartment", "开票门店"),
                //new ExportInfo("CardRemark", "卡片备注"),
            });
            return exportHelper.Export(entities.ToList(), "会员储值记录");
        }

        /// <summary>
        /// 开发票
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("DrawReceipt")]
        public JsonActionResult<bool> DrawReceipt(RechargeHistory entity)
        {
            return SafeExecute(() => RechargeHistoryService.DrawReceipt(entity));
        }
    }
}
