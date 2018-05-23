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
    /// 接车单子项
    /// </summary>
    public class PickUpOrderItem : EntityBase
    {
        /// <summary>
        /// 接车单ID
        /// </summary>
        [Display(Name = "接车单ID")]
        public Guid PickUpOrderID { get; set; }

        /// <summary>
        /// 接车单
        /// </summary>
        public virtual PickUpOrder PickUpOrder { get; set; }

        /// <summary>
        /// 服务ID
        /// </summary>
        [Display(Name = "服务ID")]
        public Guid ProductID { get; set; }

        /// <summary>
        /// 服务
        /// </summary>
        public virtual Product Product { get; set; }

        /// <summary>
        /// 服务名称
        /// </summary>
        [Display(Name = "服务名称")]
        public string ProductName { get; set; }

        /// <summary>
        /// 服务总额
        /// </summary>
        [Display(Name = "服务总额")]
        public decimal Money { get; set; }

        /// <summary>
        /// 服务单价
        /// </summary>
        [Display(Name = "服务单价")]
        public decimal PriceSale { get; set; }

        /// <summary>
        /// 服务次数
        /// </summary>
        [Display(Name = "服务次数")]
        public int Quantity { get; set; }
    }
}
