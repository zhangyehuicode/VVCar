using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.Shop.Domain.Enums;
using YEF.Core.Dtos;

namespace VVCar.Shop.Domain.Filters
{
    /// <summary>
    /// 产品零售汇总统计过滤器
    /// </summary>
    public class ProductRetailStatisticsFilter : BasePageFilter
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 产品编码
        /// </summary>
        public string ProductCode { get; set; }

        /// <summary>
        /// 产品名称/编码
        /// </summary>
        public string ProductCodeName { get; set; }

        /// <summary>
        /// 产品类别
        /// </summary>
        public EProductType? ProductType { get; set; }

        /// <summary>
        /// 车牌号
        /// </summary>
        public string PlateNumber { get; set; }

        /// <summary>
        /// 是否畅销
        /// </summary>
        public bool? IsSaleWell { get; set; }
    }
}
