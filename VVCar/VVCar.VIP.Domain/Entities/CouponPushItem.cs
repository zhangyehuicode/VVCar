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
    public class CouponPushItem : EntityBase
    {
        /// <summary>
        /// 卡券模板ID
        /// </summary>
        [Display(Name = "卡券模板ID")]
        public Guid CouponTemplateID { get; set; }

        /// <summary>
        /// 优惠券模板标题
        /// </summary>
        [Display(Name = "优惠券模板标题")]
        public string CouponTemplateTitle { get; set; }

        ///// <summary>
        ///// 被推送ID(人员ID/分组ID)
        ///// </summary>
        //[Display(Name = "被推送ID(人员ID/分组ID)")]
        //public Guid? PushedID { get; set; }

        ///// <summary>
        ///// 类型(0:人员,1:分组)
        ///// </summary>
        //[Display(Name = "类型")]
        //public ECouponPushItemType Type { get; set; }
    }
}
