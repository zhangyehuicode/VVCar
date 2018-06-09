using System.ComponentModel.DataAnnotations;
using YEF.Core.Dtos;

namespace VVCar.Shop.Domain.Filters
{
    /// <summary>
    /// 服务周期过滤条件
    /// </summary>
    public class ServicePeriodFilter : BasePageFilter
    {
        /// <summary>
        /// 产品编码
        /// </summary>
        [Display(Name = "产品编码")]
        public string ProductCode { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        [Display(Name = "产品名称")]
        public string ProductName { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        [Display(Name = "是否启用")]
        public bool IsAvailable { get; set; }
    }
}
