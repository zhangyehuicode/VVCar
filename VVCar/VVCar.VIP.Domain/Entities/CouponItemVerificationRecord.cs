using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.Shop.Domain.Entities;
using YEF.Core.Data;

namespace VVCar.VIP.Domain.Entities
{
    /// <summary>
    /// 卡券子项核销记录
    /// </summary>
    public class CouponItemVerificationRecord : NormalEntityBase
    {
        /// <summary>
        /// 卡券ID
        /// </summary>
        [Display(Name = "卡券ID")]
        public Guid CouponID { get; set; }

        /// <summary>
        /// 卡券子项ID
        /// </summary>
        [Display(Name = "卡券子项ID")]
        public Guid CouponItemID { get; set; }

        /// <summary>
        /// 卡券子项
        /// </summary>
        public virtual CouponItem CouponItem { get; set; }

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
        /// 交易订单号
        /// </summary>
        [Display(Name = "交易订单号")]
        public string TradeNo { get; set; }

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
