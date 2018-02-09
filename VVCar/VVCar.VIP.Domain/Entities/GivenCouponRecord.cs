using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEF.Core.Data;

namespace VVCar.VIP.Domain.Entities
{
    /// <summary>
    /// 赠送卡券记录
    /// </summary>
    public class GivenCouponRecord : NormalEntityBase
    {
        /// <summary>
        /// 卡券ID
        /// </summary>
        [Display(Name = "卡券ID")]
        public Guid CouponID { get; set; }

        /// <summary>
        /// 领取人OpenID
        /// </summary>
        [Display(Name = "领取人OpenID")]
        public string OwnerOpenID { get; set; }

        /// <summary>
        /// 领取人昵称
        /// </summary>
        [Display(Name = "领取人昵称")]
        public string OwnerNickName { get; set; }

        /// <summary>
        /// 领取人微信头像
        /// </summary>
        [Display(Name = "领取人微信头像")]
        public string OwnerHeadImgUrl { get; set; }

        /// <summary>
        ///赠送人OpenID
        /// </summary>
        [Display(Name = "赠送人OpenID")]
        public string DonorOpenID { get; set; }

        /// <summary>
        /// 领取人昵称
        /// </summary>
        [Display(Name = "赠送人昵称")]
        public string DonorNickName { get; set; }

        /// <summary>
        /// 赠送人微信头像
        /// </summary>
        [Display(Name = "赠送人微信头像")]
        public string DonorHeadImgUrl { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        [Display(Name = "创建日期")]
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// 赠送人领取日期
        /// </summary>
        [Display(Name = "赠送人领取日期")]
        public DateTime DonorReceivedDate { get; set; }
    }
}
