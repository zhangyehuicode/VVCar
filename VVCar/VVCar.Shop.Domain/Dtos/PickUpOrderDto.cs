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
    /// 接车单Dto
    /// </summary>
    public class PickUpOrderDto
    {
        public PickUpOrderDto()
        {
            PickUpOrderItemList = new List<PickUpOrderItem>();
        }

        /// <summary>
        /// ID
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// 门店名称
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// 门店地址
        /// </summary>
        public string DepartmentAddress { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string MobilePhoneNo { get; set; }

        /// <summary>
        /// 订单号 
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 车牌号
        /// </summary>
        public string PlateNumber { get; set; }

        /// <summary>
        /// 店员ID
        /// </summary>
        public Guid StaffID { get; set; }

        /// <summary>
        /// 店员姓名
        /// </summary>
        public string StaffName { get; set; }

        /// <summary>
        /// 订单总额
        /// </summary>
        public decimal Money { get; set; }

        /// <summary>
        /// 已收金额
        /// </summary>
        public decimal ReceivedMoney { get; set; }

        /// <summary>
        /// 尚欠金额
        /// </summary>
        public decimal StillOwedMoney { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public EPickUpOrderStatus Status { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// 接车单子项
        /// </summary>
        public List<PickUpOrderItem> PickUpOrderItemList { get; set; }
    }
}
