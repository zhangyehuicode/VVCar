using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVCar.VIP.Domain.Dtos
{
    /// <summary>
    /// 优惠券状态Dto
    /// </summary>
    public class CouponStatusInfoDto
    {
        /// <summary>
        /// 优惠券编号
        /// </summary>
        public string CouponCode { get; set; }

        /// <summary>
        /// 优惠券名称
        /// </summary>
        public string CouponTitle { get; set; }

        /// <summary>
        /// 领取人
        /// </summary>
        public string Receiver { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 领取时间
        /// </summary>
        public DateTime ReceiveDate { get; set; }

        /// <summary>
        /// 生效日期
        /// </summary>
        public DateTime EffectiveDate { get; set; }

        /// <summary>
        /// 截止日期
        /// </summary>
        public DateTime ExpiredDate { get; set; }
    }
}
