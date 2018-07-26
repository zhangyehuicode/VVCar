using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEF.Core.Data;

namespace VVCar.BaseData.Domain.Entities
{
    /// <summary>
    /// 门店标签关联表
    /// </summary>
    public class AgentDepartmentTag : EntityBase
    {
        /// <summary>
        /// 门店ID
        /// </summary>
        [Display(Name = "门店ID")]
        public Guid AgentDepartmentID { get; set; }

        /// <summary>
        /// 门店
        /// </summary>
        public virtual AgentDepartment AgentDepartment { get; set; }

        /// <summary>
        /// 标签ID
        /// </summary>
        [Display(Name = "标签ID")]
        public Guid TagID { get; set; }

        /// <summary>
        /// 标签
        /// </summary>
        public virtual Tag Tag { get; set; }

        /// <summary>
        /// 创建人ID
        /// </summary>
        [Display(Name = "创建人ID")]
        public Guid CreatedUserID { get; set; }

        /// <summary>
        /// 创建人名称
        /// </summary>
        [Display(Name = "创建人名称")]
        public string CreatedUser { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Display(Name = "创建时间")]
        public DateTime CreatedDate { get; set; }
    }
}
