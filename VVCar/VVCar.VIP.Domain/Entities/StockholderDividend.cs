using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEF.Core.Data;
using System.ComponentModel.DataAnnotations;
using VVCar.VIP.Domain.Enums;

namespace VVCar.VIP.Domain.Entities
{
    /// <summary>
    /// 股东分红
    /// </summary>
    public class StockholderDividend : EntityBase
    {
        /// <summary>
        /// 会员ID（股东ID）
        /// </summary>
        [Display(Name = "会员ID（股东ID）")]
        public Guid MemberID { get; set; }

        /// <summary>
        /// 会员（股东）
        /// </summary>
        [Display(Name = "会员（股东）")]
        public virtual Member Member { get; set; }

        /// <summary>
        /// 下级会员ID
        /// </summary>
        [Display(Name = "下级会员ID")]
        public Guid SubMemberID { get; set; }

        /// <summary>
        /// 消费返额度比例(返回额度=下级会员消费*比例)
        /// </summary>
        [Display(Name = "消费返额度比例(返回额度=下级会员消费*比例)")]
        public decimal ConsumePointRate { get; set; }

        /// <summary>
        /// 下级会员消费金额
        /// </summary>
        [Display(Name = "下级会员消费金额")]
        public decimal Money { get; set; }

        /// <summary>
        /// 分红
        /// </summary>
        [Display(Name = "分红")]
        public decimal Dividend { get; set; }

        /// <summary>
        /// 股东分红来源
        /// </summary>
        [Display(Name = "股东分红来源")]
        public EStockholderDividendSource Source { get; set; }

        /// <summary>
        /// 交易订单ID
        /// </summary>
        [Display(Name = "交易订单ID")]
        public Guid? TradeOrderID { get; set; }

        /// <summary>
        /// 交易订单类型
        /// </summary>
        [Display(Name = "交易订单类型")]
        public ETradeOrderType OrderType { get; set; }

        /// <summary>
        /// 交易单号
        /// </summary>
        [Display(Name = "交易单号")]
        public string TradeNo { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Display(Name = "创建时间")]
        public DateTime CreatedDate { get; set; }
    }
}
