using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEF.Core.Dtos;

namespace VVCar.Shop.Domain.Filters
{
    /// <summary>
    /// 接车单支付明细过滤条件
    /// </summary>
    public class PickUpOrderPaymentDetailsFilter : BasePageFilter
    {
        /// <summary>
        /// 订单ID
        /// </summary>
        public Guid PickUpOrderID { get; set; }
    }
}
