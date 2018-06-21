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
    /// 套餐子项过滤条件
    /// </summary>
    public class ComboItemFilter : BasePageFilter
    {
        /// <summary>
        /// 套餐ID
        /// </summary>
        [Display(Name = "套餐ID")]
        public Guid? ComboID { get; set; }

        /// <summary>
        /// 产品ID
        /// </summary>
        [Display(Name = "产品ID")]
        public Guid? ProductID { get; set; }
    }
}
