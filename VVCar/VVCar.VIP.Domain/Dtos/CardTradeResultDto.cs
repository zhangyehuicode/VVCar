using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVCar.VIP.Domain.Dtos
{
    /// <summary>
    /// 会员卡交易返回信息
    /// </summary>
    public class CardTradeResultDto
    {
        /// <summary>
        /// 交易流水号
        /// </summary>
        public string TradeNo { get; set; }

        /// <summary>
        /// 卡号
        /// </summary>
        public string CardNumber { get; set; }

        /// <summary>
        /// 会员姓名
        /// </summary>
        public string MemberName { get; set; }

        /// <summary>
        /// 会员卡类型
        /// </summary>
        public string CardType { get; set; }

        /// <summary>
        /// 消费前余额
        /// </summary>
        public decimal BeforeBalance { get; set; }

        /// <summary>
        /// 消费后余额
        /// </summary>
        public decimal AfterBalance { get; set; }

        /// <summary>
        /// 会员余额扣款
        /// </summary>
        public decimal TradeAmount { get; set; }

        /// <summary>
        /// 历史充值总计
        /// </summary>
        public decimal TotalRecharge { get; set; }

        /// <summary>
        /// 历史消费总计
        /// </summary>
        public decimal TotalConsume { get; set; }

        /// <summary>
        /// 会员分组
        /// </summary>
        public string MemberGroup { get; set; }
    }
}
