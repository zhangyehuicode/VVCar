using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVCar.Shop.Domain.Dtos
{
    /// <summary>
    /// 营业报表
    /// </summary>
    public class OperationStatementDto
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 总收入
        /// </summary>
        public decimal TotalInCome { get; set; }

        /// <summary>
        /// 总支出
        /// </summary>
        public decimal TotalOutCome { get; set; }
    }

    /// <summary>
    /// 营业报表明细
    /// </summary>
    public class OperationStatementDetailDto
    {
        
        /// <summary>
        /// 编码
        /// </summary>
        public string TradeNo { get; set; }

        /// <summary>
        /// 收支类型
        /// </summary>
        public EBudgetType BudgetType { get; set; }

        /// <summary>
        /// 成本/售价
        /// </summary>
        public decimal Money { get; set; }

        /// <summary>
        /// 数据来源
        /// </summary>
        public EResourceType TradeOrderType { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedDate { get; set; }
    }

    /// <summary>
    /// 数据来源类型
    /// </summary>
    public enum EResourceType
    {
        /// <summary>
        /// 报销单
        /// </summary>
        [Description("报销单")]
        ReceiptOrder = 0,

        /// <summary>
        /// 商城订单
        /// </summary>
        [Description("商城订单")]
        Order = 1,

        /// <summary>
        /// 接车单
        /// </summary>
        [Description("接车单")]
        PickupOrder = 2,
    }

    /// <summary>
    /// 收支类型
    /// </summary>
    public enum EBudgetType
    {
        /// <summary>
        /// 收入
        /// </summary>
        [Description("收入")]
        InCome = 0,

        /// <summary>
        /// 支出
        /// </summary>
        [Description("支出")]
        OutCome = 1,
    }
}