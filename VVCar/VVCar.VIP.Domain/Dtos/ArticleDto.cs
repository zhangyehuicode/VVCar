using System;
using System.ComponentModel.DataAnnotations;

namespace VVCar.VIP.Domain.Dtos
{
    /// <summary>
    /// 图文消息Dto
    /// </summary>
    public class ArticleDto
    {
        /// <summary>
        /// 图文消息ID
        /// </summary>
        [Display(Name = "图文消息")]
        public Guid ID { get; set; }

        /// <summary>
        /// 图文编码
        /// </summary>
        [Display(Name = "图文编码")]
        public string Code { get; set; }

        /// <summary>
        /// 图文标题
        /// </summary>
        [Display(Name = "图文标题")]
        public string Name { get; set; }

        /// <summary>
        /// 推送时间
        /// </summary>
        [Display(Name = "推送时间")]
        public DateTime PushDate { get; set; }

        /// <summary>
        /// 是否推送所有会员
        /// </summary>
        [Display(Name = "是否推送所有会员")]
        public bool IsPushAllMembers { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [Display(Name = "创建人")]
        public string CreatedUser { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        [Display(Name = "创建日期")]
        public DateTime CreatedDate { get; set; }
    }
}
