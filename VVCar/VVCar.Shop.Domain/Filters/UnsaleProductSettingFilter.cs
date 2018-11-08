using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEF.Core.Dtos;

namespace VVCar.Shop.Domain.Filters
{
    /// <summary>
    /// 滞销产品参数设置过滤条件
    /// </summary>
    public class UnsaleProductSettingFilter : BasePageFilter
    {
        /// <summary>
        /// 关键字
        /// </summary>
        public string Keyword { get; set; }

        /// <summary>
        /// 是否滞销,真为滞销，假为畅销
        /// </summary>
        public bool? IsUnsale { get; set; }
    }
}
