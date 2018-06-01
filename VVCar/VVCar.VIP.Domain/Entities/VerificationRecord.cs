using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.VIP.Domain.Enums;
using YEF.Core.Data;

namespace VVCar.VIP.Domain.Entities
{
    /// <summary>
    /// 核销记录
    /// </summary>
    public class VerificationRecord : NormalEntityBase
    {
        /// <summary>
        ///  优惠券编号
        /// </summary>
        [Display(Name = "优惠券编号")]
        public string CouponCode { get; set; }

        /// <summary>
        /// 优惠券ID
        /// </summary>
        public Guid CouponID { get; set; }

        /// <summary>
        ///  优惠券
        /// </summary>
        [Display(Name = "优惠券")]
        public virtual Coupon Coupon { get; set; }

        /// <summary>
        ///  核销方式
        /// </summary>
        [Display(Name = "核销方式")]
        public EVerificationMode VerificationMode { get; set; }

        /// <summary>
        /// 核销码
        /// </summary>
        [Display(Name = "核销码")]
        public string VerificationCode { get; set; }

        /// <summary>
        ///  门店编号
        /// </summary>
        [Display(Name = "门店编号")]
        public string DepartmentCode { get; set; }

        /// <summary>
        ///  核销时间
        /// </summary>
        [Display(Name = "核销时间")]
        public DateTime VerificationDate { get; set; }

        /// <summary>
        /// 性质
        /// </summary>
        [Display(Name = "性质")]
        public ENature Nature { get; set; }

        /// <summary>
        /// 抵用金额
        /// </summary>
        [Display(Name = "抵用金额")]
        public decimal VoucherAmount { get; set; }
    }
}
