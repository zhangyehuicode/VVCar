using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEF.Core.Dtos;

namespace VVCar.Shop.Domain.Filters
{
    /// <summary>
    /// 滞销产品参数设置子项过滤条件
    /// </summary>
    public class UnsaleProductSettingItemFilter: BasePageFilter
    {
        /// <summary>
        /// 滞销产品通知设置ID
        /// </summary>
        public Guid? UnsaleProductSettingID { get; set; }
    }
}
