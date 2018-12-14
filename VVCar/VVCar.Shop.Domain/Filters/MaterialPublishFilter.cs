using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.Shop.Domain.Enums;
using YEF.Core.Dtos;

namespace VVCar.Shop.Domain.Filters
{
    public class MaterialPublishFilter : BasePageFilter
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 信息发布名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 发布状态
        /// </summary>
        public EMaterialPublishStatus? Status { get; set; }
    }
}
