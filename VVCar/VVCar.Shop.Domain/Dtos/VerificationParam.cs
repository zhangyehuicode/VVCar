using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVCar.Shop.Domain.Dtos
{
    /// <summary>
    /// 核销参数
    /// </summary>
    public class VerificationParam
    {
        /// <summary>
        /// 接车单ID
        /// </summary>
        public Guid PickUpOrderID { get; set; }

        /// <summary>
        /// 优惠券号
        /// </summary>
        public List<string> CouponCodes { get; set; }

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
        /// 收款店员ID
        /// </summary>
        public Guid? StaffID { get; set; }

        /// <summary>
        /// 收款店员姓名
        /// </summary>
        public string StaffName { get; set; }
    }

    /// <summary>
    /// 核销方式
    /// </summary>
    public enum EVerificationMode
    {
        /// <summary>
        /// 扫码核销
        /// </summary>
        [Description("扫码核销")]
        ScanCode = 0,

        /// <summary>
        /// 验证码 
        /// </summary>
        [Description("验证码")]
        VerifyCode = 1,
    }
}
