using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEF.Core.Data;
using System.ComponentModel.DataAnnotations;
using VVCar.BaseData.Domain.Enums;

namespace VVCar.BaseData.Domain.Entities
{
    /// <summary>
    /// 充值记录
    /// </summary>
    public class RechargeHistory : MemberHistoryEntity
    {
        /// <summary>
        /// 赠送金额
        /// </summary>
        [Display(Name = "赠送金额")]
        public decimal GiveAmount { get; set; }

        /// <summary>
        /// 支付方式
        /// </summary>
        [Display(Name = "支付方式")]
        public EPaymentType PaymentType { get; set; }

        /// <summary>
        /// 储值方案ID
        /// </summary>
        [Display(Name = "储值方案ID")]
        public Guid? RechargePlanId { get; set; }

        /// <summary>
        /// 是否已开发票
        /// </summary>
        [Display(Name = "是否已开发票")]
        public bool HasDrawReceipt { get; set; }

        /// <summary>
        /// 开票金额
        /// </summary>
        [Display(Name = "开票金额")]
        public decimal DrawReceiptMoney { get; set; }

        /// <summary>
        /// 开发票人员ID
        /// </summary>
        [Display(Name = "开发票人员ID")]
        public Guid? DrawReceiptUserId { get; set; }

        /// <summary>
        /// 开发票人员姓名
        /// </summary>
        [Display(Name = "开发票人员姓名")]
        public string DrawReceiptUser { get; set; }

        /// <summary>
        /// 开发票人员部门
        /// </summary>
        [Display(Name = "开发票人员部门")]
        public string DrawReceiptDepartment { get; set; }
    }
}
