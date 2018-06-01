using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.Shop.Domain.Enums;
using YEF.Core.Data;
using System.ComponentModel.DataAnnotations;

namespace VVCar.Shop.Domain.Entities
{
    /// <summary>
    /// 接车单支付明细
    /// </summary>
    public class PickUpOrderPaymentDetails : EntityBase
    {
        /// <summary>
        /// 接车单订单号
        /// </summary>
        [Display(Name = "接车单订单号")]
        public string PickUpOrderCode { get; set; }

        /// <summary>
        /// 接车单ID
        /// </summary>
        [Display(Name = "接车单ID")]
        public Guid PickUpOrderID { get; set; }

        /// <summary>
        /// 接车单
        /// </summary>
        public virtual PickUpOrder PickUpOrder { get; set; }

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
