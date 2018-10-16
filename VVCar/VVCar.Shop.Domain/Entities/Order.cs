using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEF.Core.Data;
using System.ComponentModel.DataAnnotations;
using VVCar.Shop.Domain.Enums;
using VVCar.BaseData.Domain.Entities;

namespace VVCar.Shop.Domain.Entities
{
    /// <summary>
    /// 订单
    /// </summary>
    public class Order : EntityBase
    {
        public Order()
        {
            OrderItemList = new List<OrderItem>();
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
        /// 代理商门店（客户）ID
        /// </summary>
        [Display(Name = "代理商门店（客户）ID")]
        public Guid? AgentDepartmentID { get; set; }

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
        /// 物流公司
        /// </summary>
        [Display(Name = "物流公司")]
        public string LogisticsCompany { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [Display(Name = "状态")]
        public EOrderStatus Status { get; set; }

        /// <summary>
        /// 订单来源
        /// </summary>
        [Display(Name = "订单来源")]
        public EOrderSource Source { get; set; }

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
        /// 兑换积分
        /// </summary>
        [Display(Name = "兑换积分")]
        public int Points { get; set; }

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
        /// 是否需要回访
        /// </summary>
        [Display(Name = "是否需要回访")]
        public bool IsRevisit { get; set; }

        /// <summary>
        /// 回访时间（天）
        /// </summary>
        [Display(Name = "回访时间（天）")]
        public int RevisitDays { get; set; }

        /// <summary>
        /// 回访提示
        /// </summary>
        [Display(Name = "回访提示")]
        public string RevisitTips { get; set; }

        /// <summary>
        /// 回访状态
        /// </summary>
        [Display(Name = "回访状态")]
        public ERevisitStatus RevisitStatus { get; set; }

        /// <summary>
        /// 业务员ID
        /// </summary>
        [Display(Name = "业务员ID")]
        public Guid? UserID { get; set; }

        /// <summary>
        /// 业务员
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// 发货人ID
        /// </summary>
        public Guid? ConsignerID { get; set; }

        /// <summary>
        /// 发货人
        /// </summary>
        public virtual User Consigner { get; set; }

        /// <summary>
        /// 发货提醒（业务员）
        /// </summary>
        [Display(Name = "发货提醒（业务员）")]
        public string DeliveryTips { get; set; }

        /// <summary>
        /// 发货时间
        /// </summary>
        [Display(Name = "发货时间")]
        public DateTime? DeliveryDate { get; set; }

        /// <summary>
        /// 拼单记录ID
        /// </summary>
        public Guid? MerchantCrowdOrderRecordID { get; set; }

        /// <summary>
        /// 砍价记录ID
        /// </summary>
        public Guid? MerchantBargainOrderRecordID { get; set; }

        /// <summary>
        /// 商城订单子项
        /// </summary>
        public ICollection<OrderItem> OrderItemList { get; set; }
    }
}
