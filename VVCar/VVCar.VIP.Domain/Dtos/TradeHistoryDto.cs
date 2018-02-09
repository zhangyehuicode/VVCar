using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.VIP.Domain.Enums;
using YEF.Core;

namespace VVCar.VIP.Domain.Dtos
{
    /// <summary>
    /// 交易记录DTO
    /// </summary>
    public class TradeHistoryDto
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// 交易流水号
        /// </summary>
        public string TradeNo { get; set; }

        /// <summary>
        /// 外部交易流水号
        /// </summary>
        public string OutTradeNo { get; set; }

        /// <summary>
        /// 会员信息
        /// </summary>
        public string MemberName { get; set; }

        /// <summary>
        /// 会员卡号
        /// </summary>
        public string CardNumber { get; set; }

        /// <summary>
        /// 卡片类型描述
        /// </summary>
        public string CardTypeDesc { get; set; }

        /// <summary>
        /// 会员卡余额，消费后卡内余额
        /// </summary>
        public decimal CardBalance { get; set; }

        /// <summary>
        /// 交易金额
        /// </summary>
        public decimal TradeAmount { get; set; }

        /// <summary>
        /// 交易门店
        /// </summary>
        public string TradeDepartment { get; set; }

        /// <summary>
        /// 创建人姓名
        /// </summary>
        public String CreatedUser { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// 赠送金额
        /// </summary>
        public decimal GiveAmount { get; set; }

        /// <summary>
        /// 交易来源
        /// </summary>
        public ETradeSource TradeSource { get; set; }

        /// <summary>
        /// 支付方式
        /// </summary>
        public EPaymentType PaymentType { get; set; }

        /// <summary>
        /// 是否已开发票
        /// </summary>
        public bool HasDrawReceipt { get; set; }

        /// <summary>
        /// 开票金额
        /// </summary>
        public decimal DrawReceiptMoney { get; set; }

        /// <summary>
        /// 开发票人员ID
        /// </summary>
        public Guid? DrawReceiptUserId { get; set; }

        /// <summary>
        /// 开发票人员姓名
        /// </summary>
        public string DrawReceiptUser { get; set; }

        /// <summary>
        /// 开发票人员部门
        /// </summary>
        public string DrawReceiptDepartment { get; set; }

        /// <summary>
        /// 业务类型
        /// </summary>
        public EBusinessType BusinessType { get; set; }

        /// <summary>
        /// 业务类型
        /// </summary>
        public string BusinessTypeDesc => BusinessType.GetDescription();

        /// <summary>
        /// 消费类型
        /// </summary>
        public EConsumeType ConsumeType { get; set; }

        /// <summary>
        /// 消费类型
        /// </summary>
        public string ConsumeTypeDesc
        {
            get { return ConsumeType.GetDescription(); }
        }

        /// <summary>
        /// 支付方式描述
        /// </summary>
        public string PaymentTypeDesc
        {
            get { return PaymentType.GetDescription(); }
        }

        /// <summary>
        /// 卡片备注
        /// </summary>
        public string CardRemark { get; set; }

        /// <summary>
        /// 使用会员余额支付金额
        /// </summary>
        public decimal UseBalanceAmount { get; set; }
    }
}
