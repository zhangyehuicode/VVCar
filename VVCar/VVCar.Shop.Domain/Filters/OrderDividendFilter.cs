using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.Shop.Domain.Enums;
using YEF.Core.Dtos;

namespace VVCar.Shop.Domain.Filters
{
    /// <summary>
    /// 订单分红过滤条件
    /// </summary>
    public class OrderDividendFilter : BasePageFilter
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid? UserID { get; set; }

        /// <summary>
        /// 关键词
        /// </summary>
        public string Keyword { get; set; }

        /// <summary>
        /// 交易单号
        /// </summary>
        public string TradeNo { get; set; }

        public EShopTradeOrderType? OrderType { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// 是否已结算
        /// </summary>
        public bool? IsBalance { get; set; }
    }
}
