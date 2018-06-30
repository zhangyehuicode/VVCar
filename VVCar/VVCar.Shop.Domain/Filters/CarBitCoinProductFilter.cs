using System;
using System.ComponentModel.DataAnnotations;
using VVCar.Shop.Domain.Enums;
using YEF.Core.Dtos;

namespace VVCar.Shop.Domain.Filters
{
    /// <summary>
    /// 车比特产品过滤条件
    /// </summary>
    public class CarBitCoinProductFilter : BasePageFilter
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Display(Name = "名称")]
        public string Name { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        [Display(Name = "编码")]
        public string Code { get; set; }

        /// <summary>
        /// 名称/编码
        /// </summary>
        [Display(Name = "名称/编码")]
        public string NameCode { get; set; }

        /// <summary>
        /// 类别ID
        /// </summary>
        [Display(Name = "类型ID")]
        public Guid? CarBitCoinProductCategoryID { get; set; }

        /// <summary>
        /// 车比特产品类别
        /// </summary>
        [Display(Name = "车比特产品类别")]
        public ECarBitCoinProductType? CarBitCoinProductType { get; set; }

        /// <summary>
        /// 是否来自库存管理
        /// </summary>
        [Display(Name = "是否来自库存管理")]
        public bool IsFromStockManager { get; set; }

        /// <summary>
        /// 是否上架
        /// </summary>
        [Display(Name = "是否上架")]
        public bool IsPublish { get; set; }
    }
}
