using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.VIP.Domain.Enums;

namespace VVCar.VIP.Domain.Dtos
{
    /// <summary>
    /// 游戏推送子项Dto
    /// </summary>
    public class GamePushItemDto
    {
        /// <summary>
        /// 游戏推送子项ID
        /// </summary>
        [Display(Name = "游戏推送子项ID")]
        public Guid ID { get; set; }

        /// <summary>
        /// 游戏类型
        /// </summary>
        [Display(Name = "游戏类型")]
        public EGameType GameType { get; set; }

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
    }
}
