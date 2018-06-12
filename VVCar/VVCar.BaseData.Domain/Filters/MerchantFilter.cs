using System;
using System.ComponentModel.DataAnnotations;
using YEF.Core.Dtos;
using YEF.Core.Enums;

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
        [Display(Name = "ID")]
        public Guid? ID { get; set; }

        /// <summary>
        /// 商户号
        /// </summary>
        [Display(Name = "商户号")]
        public string Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Display(Name = "名称")]
        public string Name { get; set; }

        /// <summary>
        /// 商户状态
        /// </summary>
        [Display(Name = "商户状态")]
        public EMerchantStatus? Status { get; set; }
    }
}
