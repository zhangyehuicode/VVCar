using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.Shop.Domain.Enums;
using YEF.Core;

namespace VVCar.Shop.Domain.Dtos
{
    /// <summary>
    /// 订单分红Dto
    /// </summary>
    public class OrderDividendDto
    {
        /// <summary>
        /// 交易单号
        /// </summary>
        public string TradeNo { get; set; }

        /// <summary>
        /// 交易订单ID(包含 PickUpOrderID OrderID)
        /// </summary>
        public Guid TradeOrderID { get; set; }

        /// <summary>
        /// 编码(包含 ProductCode TemplateCode)
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 名称(包含 ProductName Title)
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 车牌号
        /// </summary>
        public string PlateNumber { get; set; }

        /// <summary>
        /// 用户编码
        /// </summary>
        public string UserCode { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid? UserID { get; set; }

        /// <summary>
        /// 业绩
        /// </summary>
        public decimal Money { get; set; }

        /// <summary>
        /// 抽成
        /// </summary>
        public decimal Commission { get; set; }

        /// <summary>
        /// 交易订单类型
        /// </summary>
        public EShopTradeOrderType OrderType { get; set; }

        /// <summary>
        /// 订单类型文本
        /// </summary>
        public string OrderTypeText { get { return OrderType.GetDescription(); }  }

        /// <summary>
        /// 人员类型
        /// </summary>
        public ETaskDistributionPeopleType PeopleType { get; set; }

        /// <summary>
        /// 人员类型文本
        /// </summary>
        public string PeopleTypeText { get { return PeopleType.GetDescription(); } }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedDate { get; set; }
    }
}
