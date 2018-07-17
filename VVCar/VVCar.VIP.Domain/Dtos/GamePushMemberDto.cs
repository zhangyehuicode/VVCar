using System;
using System.ComponentModel.DataAnnotations;

namespace VVCar.VIP.Domain.Dtos
{
    /// <summary>
    /// 游戏推送会员Dto
    /// </summary>
    public class GamePushMemberDto
    {
        /// <summary>
        /// 游戏推送会员ID
        /// </summary>
        [Display(Name = "游戏推送会员ID")]
        public Guid ID { get; set; }

        /// <summary>
        /// 会员ID
        /// </summary>
        [Display(Name = "会员ID")]
        public Guid MemberID { get; set; }

        /// <summary>
        /// 会员名称
        /// </summary>
        [Display(Name = "会员名称")]
        public string Name { get; set; }

        /// <summary>
        /// 会员手机号码
        /// </summary>
        [Display(Name = "会员手机号码")]
        public string MobilePhoneNo { get; set; }

        /// <summary>
        /// 车牌号列表
        /// </summary>
        [Display(Name = "车牌号列表")]
        public string PlateList { get; set; }
    }
}
