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
    /// 游戏推送过滤条件
    /// </summary>
    public class GamePushFilter : BasePageFilter
    {
        /// <summary>
        /// 标题
        /// </summary>
        [Display(Name = ("标题"))]
        public string Title { get; set; }

        /// <summary>
        /// 推送状态
        /// </summary>
        [Display(Name = "推送状态")]
        public EGamePushStatus? Status { get; set; }
    }
}
