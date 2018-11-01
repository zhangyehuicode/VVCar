using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEF.Core.Data;
using System.ComponentModel.DataAnnotations;
using VVCar.Shop.Domain.Enums;

namespace VVCar.Shop.Domain.Entities
{
    /// <summary>
    /// 接车单
    /// </summary>
    public class PickUpOrder : EntityBase
    {
        public PickUpOrder()
        {
            PickUpOrderItemList = new List<PickUpOrderItem>();
        }

        /// <summary>
        /// 会员ID
        /// </summary>
        [Display(Name = "会员ID")]
        public Guid? MemberID { get; set; }

        /// <summary>
        /// 订单号 
        /// </summary>
        [Display(Name = "订单号")]
        public string Code { get; set; }

        /// <summary>
        /// 车牌号
        /// </summary>
        [Display(Name = "车牌号")]
        public string PlateNumber { get; set; }

        /// <summary>
        /// 店员ID
        /// </summary>
        [Display(Name = "店员ID")]
        public Guid StaffID { get; set; }

        /// <summary>
        /// 店员姓名
        /// </summary>
        [Display(Name = "店员姓名")]
        public string StaffName { get; set; }

        /// <summary>
        /// 订单总额
        /// </summary>
        [Display(Name = "订单总额")]
        public decimal Money { get; set; }

        /// <summary>
        /// 已收金额
        /// </summary>
        [Display(Name = "已收金额")]
        public decimal ReceivedMoney { get; set; }

        /// <summary>
        /// 尚欠金额
        /// </summary>
        [Display(Name = "尚欠金额")]
        public decimal StillOwedMoney { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [Display(Name = "状态")]
        public EPickUpOrderStatus Status { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Display(Name = "创建时间")]
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// 接车单子项
        /// </summary>
        public ICollection<PickUpOrderItem> PickUpOrderItemList { get; set; }

        /// <summary>
        /// 商户
        /// </summary>
        public virtual Merchant Merchant { get; set; }
    }
}
