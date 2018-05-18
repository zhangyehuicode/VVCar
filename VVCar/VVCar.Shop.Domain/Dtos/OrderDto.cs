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
        /// 商品名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 商品数量
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 会员ID
        /// </summary>
        public Guid? MemberID { get; set; }

        /// <summary>
        /// OpenID
        /// </summary>
        public string OpenID { get; set; }

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
        /// 状态
        /// </summary>
        public EOrderStatus Status { get; set; }

        /// <summary>
        /// 总金额
        /// </summary>
        public decimal Money { get; set; }

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
