using System;
using System.ComponentModel.DataAnnotations;
using YEF.Core.Enums;

namespace VVCar.BaseData.Domain.Dtos
{
    /// <summary>
    /// 用户会员Dto
    /// </summary>
    public class UserMemberDto
    {
        /// <summary>
        /// 用户会员ID
        /// </summary>
        [Display(Name = "用户会员ID")]
        public Guid ID { get; set; }

        /// <summary>
        /// 会员分组
        /// </summary>
        [Display(Name = "会员分组")]
        public string MemberGroup { get; set; }

        /// <summary>
        /// 会员名称
        /// </summary>
        [Display(Name = "用户名称")]
        public string MemberName { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        [Display(Name = "性别")]
        public ESex Sex { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        [Display(Name = "创建日期")]
        public DateTime CreatedDate { get; set; }
    }
}
