using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVCar.VIP.Domain.Dtos
{
    /// <summary>
    /// 消费信息
    /// </summary>
    public class ConsumeInfoDto
    {
        /// <summary>
        /// 会员卡号
        /// </summary>
        public string CardNumber { get; set; }

        /// <summary>
        /// 会员密码
        /// </summary>
        public string MemberPassword { get; set; }

        /// <summary>
        /// 外部交易流水号
        /// </summary>
        public string OutTradeNo { get; set; }

        /// <summary>
        /// 交易金额
        /// </summary>
        public decimal TradeAmount { get; set; }

        /// <summary>
        /// 使用会员余额支付金额
        /// </summary>
        public decimal UseBalanceAmount { get; set; }

        /// <summary>
        /// 使用礼品卡余额支付金额
        /// </summary>
        public decimal UseGiftCardBalanceAmount { get; set; }

        /// <summary>
        /// 使用会员积分
        /// </summary>
        public int UseMemberPoint { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        public string OperateUser { get; set; }

        /// <summary>
        /// 支付明细
        /// </summary>
        public string PaymentDetail { get; set; }

        /// <summary>
        /// 是否是礼品卡
        /// </summary>
        public bool IsGiftCard { get; set; }

        /// <summary>
        /// 是否结账
        /// </summary>
        public bool IsCheckOut { get; set; }
    }
}
