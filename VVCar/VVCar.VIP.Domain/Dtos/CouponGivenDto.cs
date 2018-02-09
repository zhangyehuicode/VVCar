using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVCar.VIP.Domain.Dtos
{
    /// <summary>
    ///赠送卡券Dto
    /// </summary>
    public class CouponGivenDto
    {
        /// <summary>
        /// 卡券ID
        /// </summary>
        [Required]
        public Guid CouponID { get; set; }

        /// <summary>
        /// 领取人OpenID
        /// </summary>
        [Required]
        public string OwnerOpenID { get; set; }

        /// <summary>
        /// 领取人昵称
        /// </summary>
        public string OwnerNickName { get; set; }

        /// <summary>
        /// 领取人微信头像
        /// </summary>
        public string OwnerHeadImgUrl { get; set; }

        /// <summary>
        /// 领取人电话
        /// </summary>
        public string OwnerPhoneNo { get; set; }
    }
}
