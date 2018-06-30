using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEF.Core.Dtos;

namespace VVCar.Shop.Domain.Filters
{
    /// <summary>
    /// 车比特产品过滤条件
    /// </summary>
    public class CarBitCoinProductCategoryFilter : BasePageFilter
    {
        /// <summary>
        /// 类别名称
        /// </summary>
        [Display(Name = "类别名称")]
        public string Name { get; set; }

        /// <summary>
        /// 类别编号
        /// </summary>
        [Display(Name = "类别编号")]
        public string Code { get; set; }

        /// <summary>
        /// 名称或编号
        /// </summary>
        [Display(Name = "名称或编号")]
        public string NameOrCode { get; set; }

        /// <summary>
        /// 是否来自接车单
        /// </summary>
        [Display(Name = "是否来自接车单")]
        public bool IsFromPickUpOrder { get; set; }

        /// <summary>
        /// 是否来自库存管理
        /// </summary>
        [Display(Name = "是否来自库存管理")]
        public bool IsFromStockManager { get; set; }
    }
}
