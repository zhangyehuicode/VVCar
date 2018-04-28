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
        /// 类别ID
        /// </summary>
        [Display(Name = "类别ID")]
        public Guid? ProductCategoryID { get; set; }

        /// <summary>
        /// 产品类型
        /// </summary>
        [Display(Name = "产品类型")]
        public EProductType? ProductType { get; set; }
    }
}
