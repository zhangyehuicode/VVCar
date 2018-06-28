using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.VIP.Domain.Enums;
using YEF.Core.Dtos;

namespace VVCar.VIP.Domain.Filters
{
    /// <summary>
    /// 游戏卡券记录过滤条件
    /// </summary>
    public class GameCouponRecordFilter : BasePageFilter
    {
        /// <summary>
        /// 游戏类型
        /// </summary>
        [Display(Name = "游戏类型")]
        public EGameType? GameType { get; set; }

        /// <summary>
        /// 领取人OpenID
        /// </summary>
        [Display(Name = "领取人OpenID")]
        public string ReceiveOpenID { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        [Display(Name = "开始时间")]
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        [Display(Name = "结束时间")]
        public DateTime? EndTime { get; set; }
    }
}
