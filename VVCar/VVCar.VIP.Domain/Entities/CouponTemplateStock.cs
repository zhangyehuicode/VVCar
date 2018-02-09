using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEF.Core.Data;

namespace VVCar.VIP.Domain.Entities
{
    /// <summary>
    /// 优惠券库存
    /// </summary>
    public class CouponTemplateStock : NormalEntityBase
    {
        /// <summary>
        ///  库存 / 发行量
        /// </summary>
        [Display(Name = "库存")]
        public int Stock { get; set; }

        /// <summary>
        /// 已领数量
        /// </summary>
        [Display(Name = "已领数量")]
        public int UsedStock { get; set; }

        /// <summary>
        /// 剩余库存
        /// </summary>
        [Display(Name = "剩余库存")]
        public int FreeStock { get { return Stock - UsedStock; } }

        /// <summary>
        /// 领券数量限制
        /// </summary>
        [Display(Name = "领券数量限制")]
        public int CollarQuantityLimit { get; set; }

        /// <summary>
        /// 是否没有领券数量限制
        /// </summary>
        [Display(Name = "是否没有领券数量限制")]
        public bool IsNoCollarQuantityLimit { get; set; }
    }
}
