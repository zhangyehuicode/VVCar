using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVCar.VIP.Domain.Dtos
{
    /// <summary>
    /// 领取记录Dto
    /// </summary>
    public class CouponDto
    {
        /// <summary>
        /// 卡券ID
        /// </summary>
        [Display(Name = "卡券ID")]
        public Guid? CouponID { get; set; }

        /// <summary>
        ///  优惠券编号
        /// </summary>
        [Display(Name = "优惠券编号")]
        public string CouponCode { get; set; }

        /// <summary>
        ///  优惠券模板编号
        /// </summary>
        [Display(Name = "优惠券模板编号")]
        public string TemplateCode { get; set; }

        /// <summary>
        ///  标题
        /// </summary>
        [Display(Name = "标题")]
        public string Title { get; set; }

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
        /// 创建日期
        /// </summary>
        [Display(Name = "创建日期")]
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// 用户可以赠送优惠券
        /// </summary>
        [Display(Name = "用户可以赠送优惠券")]
        public bool CanGiveToPeople { get; set; }
    }
}
