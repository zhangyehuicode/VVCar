using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.Shop.Domain.Enums;
using YEF.Core.Data;

namespace VVCar.Shop.Domain.Entities
{
    /// <summary>
    /// 购物车子项
    /// </summary>
    public class ShoppingCartItem : NormalEntityBase
    {
        /// <summary>
        /// 购物车ID
        /// </summary>
        [Display(Name = "购物车ID")]
        public Guid ShoppingCartID { get; set; }

        /// <summary>
        /// 购物车
        /// </summary>
        [Display(Name = "购物车")]
        public virtual ShoppingCart ShoppingCart { get; set; }

        /// <summary>
        /// 商品ID
        /// </summary>
        [Display(Name = "商品ID")]
        public Guid GoodsID { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        [Display(Name = "产品名称")]
        public string ProductName { get; set; }

        /// <summary>
        /// 产品类型
        /// </summary>
        [Display(Name = "产品类型")]
        public EProductType ProductType { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        [Display(Name = "数量")]
        public int Quantity { get; set; }

        /// <summary>
        /// 销售单价
        /// </summary>
        [Display(Name = "销售单价")]
        public decimal PriceSale { get; set; }

        /// <summary>
        /// 销售总价
        /// </summary>
        [Display(Name = "销售总价")]
        public decimal Money { get; set; }

        /// <summary>
        /// 兑换积分
        /// </summary>
        [Display(Name = "兑换积分")]
        public int Points { get; set; }

        /// <summary>
        /// 图片路径
        /// </summary>
        [Display(Name = "图片路径")]
        public string ImgUrl { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Display(Name = "创建时间")]
        public DateTime CreatedDate { get; set; }
    }
}
