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
    /// 卡券推送子表
    /// </summary>
    public class CouponPushItem : NormalEntityBase
    {
        /// <summary>
        /// 卡券推送ID
        /// </summary>
        [Display(Name = "卡券推送ID")]
        public Guid CouponPushID { get; set; }

        /// <summary>
        /// 被推送ID(人员ID/分组ID)
        /// </summary>
        [Display(Name = "被推送ID(人员ID/分组ID)")]
        public Guid? PushedID { get; set; }

        /// <summary>
        /// 类型(0:人员,1:分组)
        /// </summary>
        [Display(Name = "类型")]
        public ECouponPushItemType Type { get; set; }
    }
}
