using System.ComponentModel.DataAnnotations;
using YEF.Core.Dtos;

namespace VVCar.BaseData.Domain.Filters
{
    /// <summary>
    /// 门店标签(客户标签)过滤条件
    /// </summary>
    public class TagFilter : BasePageFilter
    {
        /// <summary>
        /// 标签编码
        /// </summary>
        [Display(Name = "标签编码")]
        public string Code { get; set; }

        /// <summary>
        /// 标签名称
        /// </summary>
        [Display(Name = "标签名称")]
        public string Name { get; set; }
    }
}
