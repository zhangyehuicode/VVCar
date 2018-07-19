using System;
using System.ComponentModel.DataAnnotations;
using YEF.Core.Data;

namespace VVCar.BaseData.Domain.Entities
{
    /// <summary>
    /// 用户会员关联表
    /// </summary>
    public class UserMember : EntityBase
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        [Display(Name = "用户ID")]
        public Guid UserID { get; set; }

        /// <summary>
        /// 会员ID
        /// </summary>
        [Display(Name = "会员ID")]
        public Guid MemberID { get; set; }

        /// <summary>
        /// 会员微信OpenID
        /// </summary>
        [Display(Name = "会员微信OpenID")]
        public string WeChatOpenID { get; set; }

        /// <summary>
        /// 创建人ID
        /// </summary>
        [Display(Name = "创建人ID")]
        public Guid CreatedUserID { get; set; }

        /// <summary>
        /// 创建人名称
        /// </summary>
        [Display(Name = "创建人名称")]
        public string CreatedUser { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        [Display(Name = "创建日期")]
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// 用户
        /// </summary>
        public virtual User User { get; set; }

    }
}
