using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEF.Core.Dtos;

namespace VVCar.VIP.Domain.Filters
{
    /// <summary>
    /// 卡片类型过滤条件
    /// </summary>
    public class MemberCardTypeFilter : BasePageFilter
    {
        /// <summary>
        /// 类型名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 允许门店激活
        /// </summary>
        public bool? AllowStoreActivate { get; set; }

        /// <summary>
        /// 允许折扣
        /// </summary>
        public bool? AllowDiscount { get; set; }

        /// <summary>
        /// 允许储值
        /// </summary>
        public bool? AllowRecharge { get; set; }
    }
}
