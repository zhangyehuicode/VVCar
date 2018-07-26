using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.Shop.Domain.Enums;

namespace VVCar.Shop.Domain.Dtos
{
    /// <summary>
    /// 物流Dto
    /// </summary>
    public class LogisticsDto
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// 商城订单ID
        /// </summary>
        public Guid OrderID { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderCode { get; set; }

        /// <summary>
        /// 回访时间
        /// </summary>
        public DateTime? RevisitDate { get; set; }

        /// <summary>
        /// 回访提示
        /// </summary>
        public string RevisitTips { get; set; }

        /// <summary>
        /// 回访状态
        /// </summary>
        public ERevisitStatus RevisitStatus { get; set; }

        /// <summary>
        /// 业务员ID
        /// </summary>
        public Guid? UserID { get; set; }

        /// <summary>
        /// 发货提醒（业务员）
        /// </summary>
        public string DeliveryTips { get; set; }

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
        /// 物流公司
        /// </summary>
        public string LogisticsCompany { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedDate { get; set; }
    }
}
