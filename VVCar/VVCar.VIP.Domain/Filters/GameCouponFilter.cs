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
    /// 游戏配置过滤器
    /// </summary>
    public class GameCouponFilter : BasePageFilter
    {
        /// <summary>
        /// CouponTemplateID
        /// </summary>
        [Display(Name = "卡券模板ID")]
        public Guid? CouponTemplateID { get; set; }

        /// <summary>
        /// 游戏类型ID
        /// </summary>
        [Display(Name = "游戏类型ID")]
        public Guid? GameSettingID { get; set; }
    }
}
