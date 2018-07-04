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
    /// 卡券推送
    /// </summary>
    public class CouponPush : EntityBase
    {
        /// <summary>
        /// 卡券推送
        /// </summary>
        public CouponPush()
        {
            CouponPushItems = new List<CouponPushItem>();
        }

        /// <summary>
        /// 标题
        /// </summary>
        [Display(Name = "标题")]
        public string Title { get; set; }

        /// <summary>
        /// 推送时间
        /// </summary>
        [Display(Name = "推送时间")]
        public DateTime? PushDate { get; set; }

        /// <summary>
        /// 推送状态
        /// </summary>
        [Display(Name = "推送状态")]
        public ECouponPushStatus Status { get; set; }

        /// <summary>
        /// 是否推送所有会员
        /// </summary>
        [Display(Name = "是否推送所有会员")]
        public bool PushAllMembers { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        [Display(Name = "创建日期")]
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// 创建人ID
        /// </summary>
        [Display(Name = "创建人ID")]
        public Guid CreatedUserID { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [Display(Name = "创建人")]
        public string CreatedUser { get; set; }

        /// <summary>
        /// 最后修改人ID
        /// </summary>
        [Display(Name = "最后修改人ID")]
        public Guid? LastUpdateUserID { get; set; }

        /// <summary>
        /// 最后修改人名称
        /// </summary>
        [Display(Name = "最后修改人名称")]
        public string LastUpdateUser { get; set; }

        /// <summary>
        /// 最后修改日期
        /// </summary>
        [Display(Name = "最后修改日期")]
        public DateTime? LastUpdateDate { get; set; }

        /// <summary>
        /// 推送子项
        /// </summary>
        [Display(Name = "推送子项")]
        public virtual ICollection<CouponPushItem> CouponPushItems { get; set; }

        /// <summary>
        /// 推送会员
        /// </summary>
        public virtual ICollection<CouponPushMember> CouponPushMembers { get; set; }

        ///// <summary>
        ///// 优惠券模板ID
        ///// </summary>
        //[Display(Name = "优惠券模板ID")]
        //public Guid CouponTemplateID { get; set; }

        ///// <summary>
        ///// 优惠券模板标题
        ///// </summary>
        //[Display(Name = "优惠券模板标题")]
        //public string CouponTemplateTitle { get; set; }

        ///// <summary>
        ///// 推送截止时间
        ///// </summary>
        //[Display(Name = "推送截止时间")]
        //public DateTime? PushFinishDate { get; set; }

        ///// <summary>
        ///// 每月某天推送给下个月生日成员
        ///// </summary>
        //[Display(Name = "每月某天推送给下个月生日成员")]
        //public int MonthlyDays { get; set; }

        ///// <summary>
        ///// 会员生日前几天/每月某天推送给下个月生日成员(PushType:3/PushType:2)
        ///// </summary>
        //[Display(Name = "会员生日前几天/每月某天推送给下个月生日成员")]
        //public int PushDays { get; set; }

        ///// <summary>
        ///// 是否短信推送
        ///// </summary>
        //[Display(Name = "是否短信推送")]
        //public bool IsPushSMS { get; set; }

        ///// <summary>
        /////推送类别
        ///// </summary>
        //[Display(Name = "推送类别")]
        //public ECouponPushType PushType { get; set; }

        ///// <summary>
        ///// 人员分类
        ///// </summary>
        //public EPeopleSort PeopleSort { get; set; }
    }
}
