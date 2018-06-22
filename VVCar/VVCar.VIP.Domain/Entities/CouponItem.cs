using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.Shop.Domain.Entities;
using YEF.Core.Data;

namespace VVCar.VIP.Domain.Entities
{
    /// <summary>
    /// 卡券子项
    /// </summary>
    public class CouponItem : EntityBase
    {
        /// <summary>
        /// 卡券ID
        /// </summary>
        [Display(Name = "卡券ID")]
        public Guid CouponID { get; set; }

        /// <summary>
        /// 卡券
        /// </summary>
        public virtual Coupon Coupon { get; set; }

        /// <summary>
        /// 套餐ID
        /// </summary>
        [Display(Name = "套餐ID")]
        public Guid? ComboID { get; set; }

        /// <summary>
        /// 产品ID
        /// </summary>
        [Display(Name = "产品ID")]
        public Guid ProductID { get; set; }

        /// <summary>
        /// 产品
        /// </summary>
        public virtual Product Product { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        [Display(Name = "产品名称")]
        public string ProductName { get; set; }

        /// <summary>
        /// 产品代码
        /// </summary>
        [Display(Name = "产品代码")]
        public string ProductCode { get; set; }

        /// <summary>
        /// 原单价
        /// </summary>
        [Display(Name = "原单价")]
        public decimal BasePrice { get; set; }

        /// <summary>
        /// 销售单价
        /// </summary>
        [Display(Name = "销售单价")]
        public decimal PriceSale { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        [Display(Name = "数量")]
        public int Quantity { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Display(Name = "创建时间")]
        public DateTime CreatedDate { get; set; }
    }
}
