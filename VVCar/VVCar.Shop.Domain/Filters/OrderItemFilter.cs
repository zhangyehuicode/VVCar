using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEF.Core.Dtos;

namespace VVCar.Shop.Domain.Filters
{
    /// <summary>
    /// 订单子项过滤器
    /// </summary>
    public class OrderItemFilter : BasePageFilter
    {
        /// <summary>
        /// 订单ID
        /// </summary>
        public Guid? OrderID { get; set; }
    }
}
