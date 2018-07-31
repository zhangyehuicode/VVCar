using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.Shop.Domain.Entities;
using VVCar.Shop.Domain.Enums;

namespace VVCar.Shop.Domain.Dtos
{
    /// <summary>
    /// 订单Dto
    /// </summary>
    public class OrderDto
    {
        public OrderDto()
        {
            OrderItemList = new List<OrderItem>();
        }

        /// <summary>
        /// ID
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// 会员ID
        /// </summary>
        public Guid? MemberID { get; set; }

        /// <summary>
        /// OpenID
        /// </summary>
        public string OpenID { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        public string LinkMan { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 收货地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 快递单号
        /// </summary>
        public string ExpressNumber { get; set; }

        /// <summary>
        /// /物流公司
        /// </summary>
        public string LogisticsCompany { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public EOrderStatus Status { get; set; }

        /// <summary>
        /// 总金额
        /// </summary>
        public decimal Money { get; set; }

        /// <summary>
        /// 已收款金额
        /// </summary>
        public decimal ReceivedMoney { get; set; }

        /// <summary>
        /// 尚欠金额
        /// </summary>
        public decimal StillOwedMoney { get; set; }

        /// <summary>
        /// 兑换积分
        /// </summary>
        public int Points { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 回访时间(天)
        /// </summary>
        public int RevisitDays { get; set; }

        /// <summary>
        /// 回访提示
        /// </summary>
        public string RevisitTips{ get; set; }

        /// <summary>
        /// 回访状态
        /// </summary>
        public ERevisitStatus RevisitStatus { get; set; }

        /// <summary>
        /// 业务员ID
        /// </summary>
        public Guid? UserID { get; set; }

        /// <summary>
        /// 业务员名称
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 发货人ID
        /// </summary>
        public Guid? ConsignerID { get; set; }
        
        /// <summary>
        /// 发货人
        /// </summary>
        public string Consigner { get; set; }

        /// <summary>
        /// 发货提醒(业务员)
        /// </summary>
        public string DeliveryTips { get; set; }

        /// <summary>
        /// 发货时间
        /// </summary>
        public DateTime? DeliveryDate { get; set; }

        /// <summary>
        /// 商品数量
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// 订单子项
        /// </summary>
        public ICollection<OrderItem> OrderItemList { get; set; }
    }
}
