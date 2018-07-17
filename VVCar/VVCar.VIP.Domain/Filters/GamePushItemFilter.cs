using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEF.Core.Dtos;

namespace VVCar.VIP.Domain.Filters
{
    /// <summary>
    /// 游戏推送子项过滤条件
    /// </summary>
    public class GamePushItemFilter : BasePageFilter
    {
        /// <summary>
        /// 游戏推送ID
        /// </summary>
        [Display(Name = "游戏推送ID")]
        public Guid GamePushID { get; set; }

        /// <summary>
        /// 游戏设置ID
        /// </summary>
        public Guid GameSettingID { get; set; }
    }
}
