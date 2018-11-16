using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.Shop.Domain.Dtos;
using YEF.Core.Dtos;

namespace VVCar.Shop.Domain.Filters
{
    /// <summary>
    /// 收支报表过滤条件
    /// </summary>
    public class OperationStatementFilter : BasePageFilter
    {
        /// <summary>
        /// 单号
        /// </summary>
        public string TradeNo { get; set; }

        /// <summary>
        /// 收支类型
        /// </summary>
        public EBudgetType? BudgetType { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndDate { get; set; }
    }
}
