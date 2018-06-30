using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVCar.Shop.Domain.Dtos
{
    /// <summary>
    /// 车比特产品档案Dto
    /// </summary>
    public class CarBitCoinProductLiteDto
    {
        /// <summary>
        /// 主键
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// 类别编号
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 类别名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// 产品类别ID
        /// </summary>
        public Guid CarBitCoinProductCategoryID { get; set; }

        /// <summary>
        /// 零售价格
        /// </summary>
        public decimal PriceSale { get; set; }
    }
}
