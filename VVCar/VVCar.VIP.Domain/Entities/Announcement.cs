using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VVCar.VIP.Domain.Enums;
using YEF.Core.Data;

namespace VVCar.VIP.Domain.Entities
{
    /// <summary>
    /// 公告
    /// </summary>
    public class Announcement : EntityBase
    {
        /// <summary>
        /// ctor
        /// </summary>
        public Announcement()
        {
            AnnouncementPushMembers = new List<AnnouncementPushMember>();
        }

        /// <summary>
        /// 通知标题
        /// </summary>
        [Display(Name = "通知标题")]
        public string Title { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        [Display(Name = "项目名称")]
        public string Name { get; set; }

        /// <summary>
        /// 项目进展
        /// </summary>
        [Display(Name = "项目进展")]
        public string Process { get; set; }

        /// <summary>
        /// 项目备注
        /// </summary>
        [Display(Name = "项目备注")]
        public string Remark { get; set; }

        /// <summary>
        /// 详细内容
        /// </summary>
        [Display(Name = "详细内容")]
        public string Content { get; set; }

        /// <summary>
        /// 推送状态
        /// </summary>
        [Display(Name = "推送状态")]
        public EAnnouncementStatus Status { get; set; } 

        /// <summary>
        /// 推送时间
        /// </summary>
        [Display(Name = "推送时间")]
        public DateTime? PushDate { get; set; }

        /// <summary>
        /// 是否推送所有会员
        /// </summary>
        [Display(Name = "是否推送所有会员")]
        public bool PushAllMembers { get; set; }

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
        /// 最后修改人ID
        /// </summary>
        [Display(Name = "最后修改人ID")]
        public Guid? LastUpdateUserID { get; set; }

        /// <summary>
        /// 最后修改人姓名
        /// </summary>
        [Display(Name = "最后修改人")]
        public String LastUpdateUser { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        [Display(Name = "最后修改时间")]
        public DateTime? LastUpdateDate { get; set; }

        /// <summary>
        /// 推送会员
        /// </summary>
        public virtual ICollection<AnnouncementPushMember> AnnouncementPushMembers { get; set; }
    }
}
