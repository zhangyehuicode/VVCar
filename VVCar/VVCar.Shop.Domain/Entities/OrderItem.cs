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
    /// 订单子项
    /// </summary>
    public class OrderItem : EntityBase
    {
        /// <summary>
        /// 订单ID
        /// </summary>
        [Display(Name = "订单ID")]
        public Guid OrderID { get; set; }

        /// <summary>
        /// 订单
        /// </summary>
        [Display(Name = "订单")]
        public virtual Order Order { get; set; }

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
        /// 产品编码
        /// </summary>
        public string ProductCode { get; set; }

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

        ///// <summary>
        ///// 产品
        ///// </summary>
        //[Display(Name = "产品")]
        //public virtual Product Product { get; set; }

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
        /// 是否抽成比例
        /// </summary>
        [Display(Name = "是否抽成比例")]
        public bool IsCommissionRate { get; set; }

        /// <summary>
        /// 抽成比例
        /// </summary>
        [Display(Name = "抽成比例")]
        public decimal CommissionRate { get; set; }

        /// <summary>
        /// 抽成金额
        /// </summary>
        [Display(Name = "抽成金额")]
        public decimal CommissionMoney { get; set; }

        /// <summary>
        /// 抽成
        /// </summary>
        [Display(Name = "抽成")]
        public decimal Commission { get; set; }
    }
}
