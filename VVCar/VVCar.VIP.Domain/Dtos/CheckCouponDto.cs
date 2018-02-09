using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVCar.VIP.Domain.Dtos
{
    /// <summary>
    /// 检查券是否可用DTO
    /// </summary>
    public class CheckCouponDto
    {
        /// <summary>
        /// 优惠券号
        /// </summary>
        [Required]
        public string CouponCode { get; set; }

        /// <summary>
        /// 门店编号
        /// </summary>
        [Required]
        public string DepartmentCode { get; set; }
    }
}
