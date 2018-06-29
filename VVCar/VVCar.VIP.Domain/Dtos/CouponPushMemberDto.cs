using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVCar.VIP.Domain.Dtos
{
    /// <summary>
    /// 推送会员Dto
    /// </summary>
    public class CouponPushMemberDto
    {
        /// <summary>
        /// 推送会员ID
        /// </summary>
        [Display(Name = "推送会员ID")]
        public Guid ID { get; set; }

        /// <summary>
        /// 会员ID
        /// </summary>
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
        public string PlateList { get; set; }
    }
}
