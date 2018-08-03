using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.VIP.Domain.Enums;

namespace VVCar.VIP.Domain.Dtos
{
    /// <summary>
    /// 会员充值信息
    /// </summary>
    public class RechargeInfoDto
    {
        /// <summary>
        /// 会员卡ID
        /// </summary>
        public Guid CardID { get; set; }

        /// <summary>
        /// 充值方案ID
        /// </summary>
        public Guid RechargePlanID { get; set; }

        /// <summary>
        /// 充值金额
        /// </summary>
        public decimal RechargeAmount { get; set; }

        /// <summary>
        /// 赠送金额
        /// </summary>
        public decimal GiveAmount { get; set; }

        /// <summary>
        /// 外部交易流水号
        /// </summary>
        public string OutTradeNo { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        public string OperateUser { get; set; }

        /// <summary>
        /// 支付方式
        /// </summary>
        public EPaymentType PaymentType { get; set; }
    }
}
