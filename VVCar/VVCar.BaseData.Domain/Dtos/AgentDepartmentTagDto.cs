using System;
using System.ComponentModel.DataAnnotations;

namespace VVCar.BaseData.Domain.Dtos
{
    /// <summary>
    /// 门店标签Dto
    /// </summary>
    public class AgentDepartmentTagDto
    {
        /// <summary>
        /// 门店标签ID
        /// </summary>
        [Display(Name = "门店标签ID")]
        public Guid ID { get; set; }

        // <summary>
        /// 门店ID
        /// </summary>
        [Display(Name = "门店ID")]
        public Guid AgentDepartmentID { get; set; }

        /// <summary>
        /// 标签ID
        /// </summary>
        [Display(Name = "标签ID")]
        public Guid TagID { get; set; }

        /// <summary>
        /// 标签编码
        /// </summary>
        [Display(Name = "标签编码")]
        public string TagCode { get; set; }

        /// <summary>
        /// 标签名称
        /// </summary>
        [Display(Name = "标签名称")]
        public string TagName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Display(Name = "创建时间")]
        public DateTime CreatedDate { get; set; }
    }
}
