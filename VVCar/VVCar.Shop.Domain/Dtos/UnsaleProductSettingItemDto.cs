using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVCar.Shop.Domain.Dtos
{
    /// <summary>
    /// 滞销产品参数设置子项Dto
    /// </summary>
    public class UnsaleProductSettingItemDto
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// 滞销产品参数设置ID
        /// </summary>
        public Guid UnsaleProductSettingID { get; set; }

        /// <summary>
        /// 产品ID
        /// </summary>
        public Guid ProductID { get; set; }

        /// <summary>
        /// 产品编码
        /// </summary>
        public string ProductCode { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; }

    }
}
