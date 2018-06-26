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
    /// 游戏设置过滤条件
    /// </summary>
    public class GameSettingFilter : BasePageFilter
    {
        /// <summary>
        /// 游戏类型
        /// </summary>
        [Display(Name = "游戏类型")]
        public EGameType? GameType { get; set; }
    }
}
