using System;
using System.ComponentModel.DataAnnotations;
using YEF.Core.Data;

namespace VVCar.BaseData.Domain.Entities
{
    /// <summary>
    /// 门店标签(客户标签)
    /// </summary>
    public class Tag : EntityBase
    {
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
        /// 创建人ID
        /// </summary>
        [Display(Name = "创建人ID")]
        public Guid CreatedUserID { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [Display(Name = "创建人")]
        public string CreatedUser { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Display(Name = "创建时间")]
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// 最后修改人ID
        /// </summary>
        [Display(Name = "最后修改人ID")]
        public Guid? LastUpdatedUserID { get; set; }

        /// <summary>
        /// 最后修改人
        /// </summary>
        [Display(Name = "最后修改人")]
        public string LastUpdatedUser { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        [Display(Name = "最后修改时间")]
        public DateTime? LastUpdatedDate { get; set; }
    }
}
