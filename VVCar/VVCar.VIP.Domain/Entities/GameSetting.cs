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
    /// 游戏设置
    /// </summary>
    public class GameSetting : EntityBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public GameSetting()
        {
            GameCoupons = new List<GameCoupon>();
        }

        /// <summary>
        /// 游戏类型
        /// </summary>
        [Display(Name = "游戏类型")]
        public EGameType GameType { get; set; }

        /// <summary>
        /// 推送子项
        /// </summary>
        [Display(Name = "推送子项")]
        public virtual ICollection<GameCoupon> GameCoupons { get; set; }

        /// <summary>
        /// 游戏开始时间
        /// </summary>
        [Display(Name = "游戏开始时间")]
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 游戏结束时间
        /// </summary>
        [Display(Name = "游戏结束时间")]
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 周期天
        /// </summary>
        [Display(Name = "周期天")]
        public int PeriodDays { get; set; }

        /// <summary>
        /// 周期次
        /// </summary>
        [Display(Name = "周期次")]
        public int PeriodCounts { get; set; }

        /// <summary>
        /// 上限
        /// </summary>
        [Display(Name = "上限")]
        public int Limit { get; set; }

        /// <summary>
        /// 是否可分享
        /// </summary>
        [Display(Name = "是否可分享")]
        public bool IsShare { get; set; }

        /// <summary>
        /// 分享标题
        /// </summary>
        [Display(Name = "分享标题")]
        public string ShareTitle { get; set; }

        /// <summary>
        /// 商城订单支付完成是否显示游戏链接
        /// </summary>
        [Display(Name = "商城订单支付完成是否显示游戏链接")]
        public bool IsOrderShow { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        [Display(Name = "是否启用")]
        public bool IsAvailable { get; set; }

        /// <summary>
        /// 创建人ID
        /// </summary>
        [Display(Name = "创建人ID")]
        public Guid? CreatedUserID { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string CreatedUser { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Display(Name = "创建时间")]
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// 最后修改人ID
        /// </summary>
        [Display(Name = "最后修改人ID")]
        public Guid? LastUpdateUserID { get; set; }

        /// <summary>
        /// 最后修改人
        /// </summary>
        [Display(Name = "最后修改人")]
        public string LastUpdateUser { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        [Display(Name = "最后修改时间")]
        public DateTime? LastUpdateDate { get; set; }
    }
}
