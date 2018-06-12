using System;
using System.ComponentModel.DataAnnotations;

namespace VVCar.Shop.Domain.Dtos
{
    /// <summary>
    /// 产品零售汇总Dto
    /// </summary>
    public class ProductRetailStatisticsDto
    {
        /// <summary>
        /// 产品ID
        /// </summary>
        [Display(Name = "产品ID")]
        public Guid ProductID { get; set; }

        /// <summary>
        /// 产品类别名称
        /// </summary>
        [Display(Name = "产品类别名称")]
        public string ProductCategoryName { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        [Display(Name = "产品名称")]
        public string ProductName { get; set; }

        /// <summary>
        /// 产品编码
        /// </summary>
        [Display(Name = "产品编码")]
        public string ProductCode { get; set; }

        /// <summary>
        /// 销售数量
        /// </summary>
        [Display(Name = "销售数量")]
        public int Quantity { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        [Display(Name = "单位")]
        public string Unit { get; set; }

        /// <summary>
        /// 销售总额
        /// </summary>
        [Display(Name = "销售总额")]
        public decimal Money { get; set; }

        /// <summary>
        /// 产品类别
        /// </summary>
        [Display(Name = "产品类别")]
        public string ProductType { get; set; }
    }
}
