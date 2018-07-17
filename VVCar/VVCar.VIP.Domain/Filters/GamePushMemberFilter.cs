using System;
using System.ComponentModel.DataAnnotations;
using YEF.Core.Dtos;

namespace VVCar.VIP.Domain.Filters
{
    /// <summary>
    /// 游戏推送会员过滤条件
    /// </summary>
    public class GamePushMemberFilter : BasePageFilter
    {
        /// <summary>
        /// 游戏推送ID
        /// </summary>
        [Display(Name = "游戏推送ID")]
        public Guid GamePushID { get; set; }
    }
}
