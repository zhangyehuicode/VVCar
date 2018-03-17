using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEF.Core.Data;
using System.ComponentModel.DataAnnotations;

namespace VVCar.Shop.Domain.Entities
{
    /// <summary>
    /// 产品类别
    /// </summary>
    public class ProductCategory : EntityBase
    {
        /// <summary>
        /// 产品类别ctor
        /// </summary>
        public ProductCategory()
        {
            ProductList = new List<Product>();
        }

        /// <summary>
        /// 名称
        /// </summary>
        [Display(Name = "名称")]
        public string Name { get; set; }

        /// <summary>
        /// 代码
        /// </summary>
        [Display(Name = "代码")]
        public string Code { get; set; }

        /// <summary>
        /// 产品列表
        /// </summary>
        [Display(Name = "产品列表")]
        public ICollection<Product> ProductList { get; set; }
    }
}
