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
    ///  车比特订单子项
    /// </summary>
    public class CarBitCoinOrderItem : EntityBase
    {
        /// <summary>
        /// 订单ID
        /// </summary>
        [Display(Name = "订单ID")]
        public Guid CarBitCoinOrderID { get; set; }

        /// <summary>
        /// 订单
        /// </summary>
        [Display(Name = "订单")]
        public virtual CarBitCoinOrder Order { get; set; }

        /// <summary>
        /// 商品ID
        /// </summary>
        [Display(Name = "商品ID")]
        public Guid GoodsID { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        [Display(Name = "图片")]
        public string ImgUrl { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        [Display(Name = "产品名称")]
        public string ProductName { get; set; }

        /// <summary>
        /// 产品类型
        /// </summary>
        [Display(Name = "产品类型")]
        public ECarBitCoinProductType ProductType { get; set; }

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
        /// 兑换车比特
        /// </summary>
        [Display(Name = "兑换车比特")]
        public decimal CarBitCoins { get; set; }

        /// <summary>
        /// 马力
        /// </summary>
        [Display(Name = "马力")]
        public int Horsepower { get; set; }
    }
}
