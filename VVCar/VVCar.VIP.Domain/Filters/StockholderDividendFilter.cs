using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.VIP.Domain.Enums;
using YEF.Core.Dtos;

namespace VVCar.VIP.Domain.Filters
{
    public class StockholderDividendFilter : BasePageFilter
    {
        /// <summary>
        /// 会员ID（股东ID）
        /// </summary>
        public Guid? MemberID { get; set; }

        /// <summary>
        /// 股东分红来源
        /// </summary>
        public EStockholderDividendSource? Source { get; set; }

        /// <summary>
        /// 交易订单类型
        /// </summary>
        public ETradeOrderType? OrderType { get; set; }

        /// <summary>
        /// 交易单号
        /// </summary>
        public string TradeNo { get; set; }

    }
}
