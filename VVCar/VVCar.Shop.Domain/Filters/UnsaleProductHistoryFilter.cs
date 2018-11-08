using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.Shop.Domain.Enums;
using YEF.Core.Dtos;

namespace VVCar.Shop.Domain.Filters
{
    public class UnsaleProductHistoryFilter : BaseFilter
    {
        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 产品类型
        /// </summary>
        public EProductType? ProductType { get; set; }

        /// <summary>
        /// 关键字
        /// </summary>
        public string Keyword { get; set; }

        /// <summary>
        /// 销售状态
        /// </summary>
        public EUnsaleProductStatus? Status { get; set; }
    }
}
