using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEF.Core.Dtos;

namespace VVCar.BaseData.Domain.Filters
{
    /// <summary>
    /// 商户过滤条件
    /// </summary>
    public class MerchantFilter : BasePageFilter
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid? ID { get; set; }

        /// <summary>
        /// 商户号
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
    }
}
