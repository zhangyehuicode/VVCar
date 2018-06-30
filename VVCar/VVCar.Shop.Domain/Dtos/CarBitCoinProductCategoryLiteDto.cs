using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVCar.Shop.Domain.Dtos
{
    /// <summary>
    /// 产品类别Dto
    /// </summary>
    public class CarBitCoinProductCategoryLiteDto
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
        /// 产品列表
        /// </summary>
        public IList<CarBitCoinProductLiteDto> CarBitCoinProducts { get; set; }
    }
}
