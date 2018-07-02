using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.Shop.Domain.Enums;
using YEF.Core.Data;

namespace VVCar.Shop.Domain.Entities
{
    /// <summary>
    /// 车比特订单支付明细
    /// </summary>
    public class CarBitCoinOrderPaymentDetails : EntityBase
    {
        /// <summary>
        /// 车比特订单号
        /// </summary>
        [Display(Name = "车比特订单号")]
        public string CarBitCoinOrderCode { get; set; }

        /// <summary>
        /// 订单ID
        /// </summary>
        [Display(Name = "订单ID")]
        public Guid CarBitCoinOrderID { get; set; }

        /// <summary>
        /// 商城订单
        /// </summary>
        public virtual CarBitCoinOrder CarBitCoinOrder { get; set; }

        /// <summary>
        /// 支付类型
        /// </summary>
        [Display(Name = "支付类型")]
        public EPayType PayType { get; set; }

        /// <summary>
        /// 支付金额
        /// </summary>
        [Display(Name = "支付金额")]
        public decimal PayMoney { get; set; }

        /// <summary>
        /// 抵用额度
        /// </summary>
        [Display(Name = "抵用额度")]
        public decimal VoucherAmount { get; set; }

        /// <summary>
        /// 支付信息
        /// </summary>
        [Display(Name = "支付信息")]
        public string PayInfo { get; set; }

        /// <summary>
        /// 会员信息
        /// </summary>
        [Display(Name = "会员信息")]
        public string MemberInfo { get; set; }

        /// <summary>
        /// 收款店员ID
        /// </summary>
        [Display(Name = "收款店员ID")]
        public Guid? StaffID { get; set; }

        /// <summary>
        /// 收款店员姓名
        /// </summary>
        [Display(Name = "收款店员姓名")]
        public string StaffName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Display(Name = "创建时间")]
        public DateTime CreatedDate { get; set; }
    }
}
