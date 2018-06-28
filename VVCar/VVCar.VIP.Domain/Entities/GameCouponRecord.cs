using System;
using System.ComponentModel.DataAnnotations;
using VVCar.VIP.Domain.Enums;
using YEF.Core.Data;

namespace VVCar.VIP.Domain.Entities
{
    /// <summary>
    /// 游戏卡券记录
    /// </summary>
    public class GameCouponRecord : EntityBase
    {
        /// <summary>
        /// 游戏类型
        /// </summary>
        [Display(Name = "游戏类型")]
        public EGameType GameType { get; set; }

        /// <summary>
        /// 领取人OpenID
        /// </summary>
        [Display(Name = "领取人OpenID")]
        public string ReceiveOpenID { get; set; }

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
        /// 游戏卡券标题
        /// </summary>
        [Display(Name = "游戏卡券标题")]
        public string CouponTitle { get; set; }

        /// <summary>
        /// 领取游戏卡券数量
        /// </summary>
        [Display(Name = "领取游戏卡券数量")]
        public int ReceiveCount { get; set; }

        /// <summary>
        /// 领取人昵称
        /// </summary>
        [Display(Name = "领取人昵称")]
        public string NickName { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        [Display(Name = "订单号")]
        public string OutTradeNo { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Display(Name = "创建时间")]
        public DateTime CreatedDate { get; set; }
    }
}
