using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEF.Core.Dtos;

namespace VVCar.Shop.Domain.Filters
{
    /// <summary>
    /// 接车单子项过滤条件
    /// </summary>
    public class PickUpOrderItemFilter : BasePageFilter
    {
        /// <summary>
        /// 接车单ID
        /// </summary>
        public Guid? PickUpOrderID { get; set; }
    }
}
