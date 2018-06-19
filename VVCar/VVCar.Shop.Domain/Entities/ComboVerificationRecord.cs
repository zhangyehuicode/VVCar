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
    /// 套餐核销记录
    /// </summary>
    public class ComboVerificationRecord : EntityBase
    {
        /// <summary>
        /// 套餐子项ID
        /// </summary>
        [Display(Name = "套餐子项ID")]
        public Guid ComboItemID { get; set; }

        /// <summary>
        /// 套餐子项
        /// </summary>
        public virtual ComboItem ComboItem { get; set; }

        /// <summary>
        /// 接车单ID
        /// </summary>
        [Display(Name = "接车单ID")]
        public Guid? PickUpOrderID { get; set; }

        /// <summary>
        /// 接车单
        /// </summary>
        public virtual PickUpOrder PickUpOrder { get; set; }

        /// <summary>
        /// 商城订单ID
        /// </summary>
        [Display(Name = "商城订单ID")]
        public Guid? OrderID { get; set; }

        /// <summary>
        /// 商城订单
        /// </summary>
        public virtual Order Order { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        [Display(Name = "数量")]
        public int Quantity { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Display(Name = "创建时间")]
        public DateTime CreatedDate { get; set; }
    }
}
