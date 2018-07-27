using System;
using System.ComponentModel.DataAnnotations;

namespace VVCar.BaseData.Domain.Dtos
{
    /// <summary>
    /// 客户标签Dto
    /// </summary>
    public class TagDto
    {
        /// <summary>
        /// 客户标签ID
        /// </summary>
        [Display(Name = "客户标签ID")]
        public Guid ID { get; set; }

        /// <summary>
        /// 客户标签编码
        /// </summary>
        [Display(Name = "客户标签编码")]
        public string Code { get; set; }

        /// <summary>
        /// 客户标签名称
        /// </summary>
        [Display(Name = "客户标签名称")]
        public string Name { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Display(Name = "创建时间")]
        public DateTime CreatedDate { get; set; }
    }
}
