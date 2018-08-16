using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.Shop.Domain.Enums;
using YEF.Core.Dtos;

namespace VVCar.Shop.Domain.Filters
{
    public class ProductFilter : BasePageFilter
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
        [Display(Name = "类别ID")]
        public Guid? ProductCategoryID { get; set; }

        /// <summary>
        /// 产品类型
        /// </summary>
        [Display(Name = "产品类型")]
        public EProductType? ProductType { get; set; }

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

        /// <summary>
        /// 是否套餐
        /// </summary>
        [Display(Name = "是否套餐")]
        public bool? IsCombo { get; set; }

        /// <summary>
        /// 是否员工内部领取
        /// </summary>
        [Display(Name = "是否员工内部领取")]
        public bool? IsInternaCollection { get; set; }
    }
}
