using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using YEF.Core.Data;

namespace VVCar.BaseData.Domain.Entities
{
    /// <summary>
    /// 门店类别
    /// </summary>
    public class AgentDepartmentCategory : EntityBase
    {
        /// <summary>
        /// ctor
        /// </summary>
        public AgentDepartmentCategory()
        {
            SubAgentDepartments = new List<AgentDepartment>();
        }

        /// <summary>
        /// 父级ID
        /// </summary>
        [Display(Name = "父级ID")]
        public Guid? ParentId { get; set; }

        /// <summary>
        /// 类别编号
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 类别名称
        /// </summary>
        [Display(Name = "类别名称")]
        public string Name { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [Display(Name = "排序")]
        public int Index { get; set; }

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
        /// 创建日期
        /// </summary>
        [Display(Name = "创建日期")]
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// 最后修改人ID
        /// </summary>
        [Display(Name = "最后修改人ID")]
        public Guid? LastUpdateUserID { get; set; }

        /// <summary>
        /// 最后修改人
        /// </summary>
        [Display(Name = "最后修改人")]
        public string LastUpdateUser { get; set; }

        /// <summary>
        /// 最后修改日期
        /// </summary>
        [Display(Name = "最后修改日期")]
        public DateTime? LastUpdateDate { get; set; }

        /// <summary>
        /// 下级子门店
        /// </summary>
        public virtual ICollection<AgentDepartment> SubAgentDepartments { get; set; }
    }
}
