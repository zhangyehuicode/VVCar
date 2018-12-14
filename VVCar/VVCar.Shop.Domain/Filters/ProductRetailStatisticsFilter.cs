using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.Shop.Domain.Enums;
using YEF.Core.Dtos;
using System.ComponentModel;

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

        /// <summary>
        /// 商户号
        /// </summary>
        public string MerchantCode { get; set; }

        /// <summary>
        ///所有子商户数据
        /// </summary>
        public bool AllSubMerchantData { get; set; }

        /// <summary>
        /// 排序类型
        /// </summary>
        public EProductRetailStatisticsFilterOrderType OrderType { get; set; }
    }

    /// <summary>
    /// 产品零售汇总统图表数据计过滤器
    /// </summary>
    public class ProductRetailStatisticsChartDataFilter : BasePageFilter
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
        /// 产品名称/编码
        /// </summary>
        public Guid? ProductID { get; set; }
    }

    /// <summary>
    /// 产品零售汇总统计过滤排序类型
    /// </summary>
    public enum EProductRetailStatisticsFilterOrderType
    {
        /// <summary>
        /// 销售总额
        /// </summary>
        [Description("销售总额")]
        Money = 0,

        /// <summary>
        /// 销售总数
        /// </summary>
        [Description("销售总数")]
        Quantity = 1,
    }
}
