using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.VIP.Domain.Enums;

namespace VVCar.VIP.Domain.Dtos
{
    /// <summary>
    /// 股东分红Dto
    /// </summary>
    public class StockholderDividendDto
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// 会员ID（股东ID）
        /// </summary>
        public Guid MemberID { get; set; }

        /// <summary>
        /// 会员（股东）名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 下级会员ID
        /// </summary>
        public Guid SubMemberID { get; set; }

        /// <summary>
        /// 下级会员名称
        /// </summary>
        public string SubMemberName { get; set; }

        /// <summary>
        /// 消费返额度比例(返回额度=下级会员消费*比例)
        /// </summary>
        public decimal ConsumePointRate { get; set; }

        /// <summary>
        /// 下级会员消费金额
        /// </summary>
        public decimal Money { get; set; }

        /// <summary>
        /// 分红
        /// </summary>
        public decimal Dividend { get; set; }

        /// <summary>
        /// 股东分红来源
        /// </summary>
        public EStockholderDividendSource Source { get; set; }

        /// <summary>
        /// 交易订单ID
        /// </summary>
        public Guid? TradeOrderID { get; set; }

        /// <summary>
        /// 交易订单类型
        /// </summary>
        public ETradeOrderType OrderType { get; set; }

        /// <summary>
        /// 交易单号
        /// </summary>
        public string TradeNo { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedDate { get; set; }
    }
}
