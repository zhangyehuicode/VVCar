using System;
using System.ComponentModel.DataAnnotations;
using VVCar.VIP.Domain.Enums;

namespace VVCar.VIP.Domain.Dtos
{
    /// <summary>
    /// 卡券推送任务 Dto
    /// </summary>
    public class CouponPushDto
    {
        /// <summary>
        /// 卡券推送任务ID
        /// </summary>
        [Display(Name = "卡券推送任务ID")]
        public Guid ID { get; set; }

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
        /// 创建日期
        /// </summary>
        [Display(Name = "创建日期")]
        public DateTime CreatedDate { get; set; }
    }
}
