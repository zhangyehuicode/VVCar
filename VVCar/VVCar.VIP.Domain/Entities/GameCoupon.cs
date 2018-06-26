using System;
using YEF.Core.Data;
using System.ComponentModel.DataAnnotations;

namespace VVCar.VIP.Domain.Entities
{
    /// <summary>
    /// 游戏卡券
    /// </summary>
    public class GameCoupon : EntityBase
    {
        /// <summary>
        /// 游戏类型ID
        /// </summary>
        [Display(Name = "游戏类型ID")]
        public Guid GameSettingID { get; set; }

        /// <summary>
        /// 卡券模板ID
        /// </summary>
        [Display(Name = "卡券模板ID")]
        public Guid CouponTemplateID { get; set; }

        /// <summary>
        /// 卡券模板
        /// </summary>
        public virtual CouponTemplate CouponTemplate { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Display(Name = "创建时间")]
        public DateTime CreatedDate { get; set; }
    }
}
