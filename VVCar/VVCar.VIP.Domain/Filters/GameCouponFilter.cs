using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEF.Core.Dtos;

namespace VVCar.VIP.Domain.Filters
{
    /// <summary>
    /// 游戏配置过滤器
    /// </summary>
    public class GameCouponFilter: BasePageFilter
    {
        /// <summary>
        /// CouponTemplateID
        /// </summary>
        public Guid? CouponTemplateID { get; set; }
    }
}
