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
    /// 投放的优惠券
    /// </summary>
    public class Coupon : NormalEntityBase
    {
        public Coupon()
        {
            CouponItemList = new List<CouponItem>();
        }

        /// <summary>
        /// 会员ID
        /// </summary>
        [Display(Name = "会员ID")]
        public Guid? MemberID { get; set; }

        /// <summary>
        /// 会员
        /// </summary>
        public virtual Member Member { get; set; }

        /// <summary>
        ///  优惠券编号
        /// </summary>
        [Display(Name = "优惠券编号")]
        public string CouponCode { get; set; }

        /// <summary>
        ///  优惠券模板ID
        /// </summary>
        [Display(Name = "优惠券模板ID")]
        public Guid TemplateID { get; set; }

        /// <summary>
        ///  优惠券模板
        /// </summary>
        [Display(Name = "优惠券模板")]
        public virtual CouponTemplate Template { get; set; }

        /// <summary>
        ///  券面值，抵用券时为抵用金额，代金券时为减免金额，折扣券时为折扣比例
        /// </summary>
        [Display(Name = "抵用金额")]
        public decimal CouponValue { get; set; }

        /// <summary>
        /// 生效时间，会精确到 00:00:00
        /// </summary>
        [Display(Name = "生效时间")]
        public DateTime EffectiveDate { get; set; }

        /// <summary>
        /// 截止时间, 会精确到 23:59:59
        /// </summary>
        [Display(Name = "截止时间")]
        public DateTime ExpiredDate { get; set; }

        /// <summary>
        ///  使用状态
        /// </summary>
        [Display(Name = "使用状态")]
        public ECouponStatus Status { get; set; }

        /// <summary>
        /// 领取人OpenID
        /// </summary>
        [Display(Name = "领取人OpenID")]
        public string OwnerOpenID { get; set; }

        /// <summary>
        /// 小程序OpenID
        /// </summary>
        public string MinProOpenID { get; set; }

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
        /// 领取人电话
        /// </summary>
        [Display(Name = "领取人电话")]
        public string OwnerPhoneNo { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        [Display(Name = "创建日期")]
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// 是否可以重复使用
        /// </summary>
        [Display(Name = "是否可以重复使用")]
        public bool IsCanReuse { get; set; }

        /// <summary>
        /// 领取渠道
        /// </summary>
        [Display(Name = "领取渠道")]
        public string ReceiveChannel { get; set; }

        /// <summary>
        /// 卡券子项
        /// </summary>
        public ICollection<CouponItem> CouponItemList { get; set; }

        /// <summary>
        /// 是否优先抵扣
        /// </summary>
        //[Display(Name = "是否优先抵扣")]
        //public bool IsDeductionFirst { get; set; }
    }
}
