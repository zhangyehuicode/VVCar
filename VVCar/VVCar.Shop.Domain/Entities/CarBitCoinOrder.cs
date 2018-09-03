using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.Shop.Domain.Enums;
using YEF.Core.Data;

namespace VVCar.Shop.Domain.Entities
{
    /// <summary>
    /// 车比特订单
    /// </summary>
    public class CarBitCoinOrder : EntityBase
    {
        public CarBitCoinOrder()
        {
            CarBitCoinOrderItemList = new List<CarBitCoinOrderItem>();
        }

        /// <summary>
        /// 订单号
        /// </summary>
        [Display(Name = "订单号")]
        public string Code { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        [Display(Name = "序号")]
        public int Index { get; set; }

        /// <summary>
        /// 会员ID
        /// </summary>
        [Display(Name = "会员ID")]
        public Guid? MemberID { get; set; }

        /// <summary>
        /// OpenID
        /// </summary>
        [Display(Name = "OpenID")]
        public string OpenID { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        [Display(Name = "联系人")]
        public string LinkMan { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        [Display(Name = "联系电话")]
        public string Phone { get; set; }

        /// <summary>
        /// 收货地址
        /// </summary>
        [Display(Name = "收货地址")]
        public string Address { get; set; }

        /// <summary>
        /// 快递单号
        /// </summary>
        [Display(Name = "快递单号")]
        public string ExpressNumber { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [Display(Name = "状态")]
        public EOrderStatus Status { get; set; }

        /// <summary>
        /// 总金额
        /// </summary>
        [Display(Name = "总金额")]
        public decimal Money { get; set; }

        /// <summary>
        /// 已收款金额
        /// </summary>
        [Display(Name = "已收款金额")]
        public decimal ReceivedMoney { get; set; }

        /// <summary>
        /// 尚欠金额
        /// </summary>
        [Display(Name = "尚欠金额")]
        public decimal StillOwedMoney { get; set; }

        /// <summary>
        /// 兑换车比特
        /// </summary>
        [Display(Name = "兑换车比特")]
        public decimal CarBitCoins { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Display(Name = "创建时间")]
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        [Display(Name = "最后修时间")]
        public DateTime? LastUpdatedDate { get; set; }

        /// <summary>
        /// 最后修改人ID
        /// </summary>
        [Display(Name = "最后修改人ID")]
        public Guid? LastUpdatedUserID { get; set; }

        /// <summary>
        /// 最后修改人
        /// </summary>
        [Display(Name = "最后修改人")]
        public string LastUpdatedUser { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Display(Name = "备注")]
        public string Remark { get; set; }

        /// <summary>
        /// 拼单记录ID
        /// </summary>
        [Display(Name = "拼单记录ID")]
        public Guid? CrowdOrderRecordID { get; set; }

        /// <summary>
        /// 商城订单子项
        /// </summary>
        public ICollection<CarBitCoinOrderItem> CarBitCoinOrderItemList { get; set; }
    }
}
