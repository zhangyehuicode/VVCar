using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.VIP.Domain.Enums;

namespace VVCar.VIP.Domain.Dtos
{
    /// <summary>
    /// 核销优惠券DTO
    /// </summary>
    public class VerifyCouponDto
    {
        /// <summary>
        /// 优惠券号
        /// </summary>
        [Required]
        public IEnumerable<string> CouponCodes { get; set; }

        /// <summary>
        ///  核销方式
        /// </summary>
        public EVerificationMode VerificationMode { get; set; }

        /// <summary>
        /// 核销码
        /// </summary>
        public string VerifyCode { get; set; }

        /// <summary>
        /// 门店编号
        /// </summary>
        public string DepartmentCode { get; set; }

        /// <summary>
        /// 门店ID
        /// </summary>
        public Guid? DepartmentID { get; set; }

        /// <summary>
        /// 消费金额
        /// </summary>
        public decimal ConsumeMoney { get; set; }

        /// <summary>
        /// 会员卡抵用信息
        /// </summary>
        public List<MemberCardVoucherInfo> MemberCardVoucherInfoList { get; set; }
    }

    public class MemberCardVoucherInfo
    {
        /// <summary>
        /// 卡券号
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 抵用额度
        /// </summary>
        public decimal VoucherAmount { get; set; }
    }
}
