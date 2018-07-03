using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.BaseData.Domain.Enums;
using YEF.Core.Dtos;
using YEF.Core.Enums;

namespace VVCar.BaseData.Domain.Filters
{
    /// <summary>
    /// 代理商门店过滤器
    /// </summary>
    public class AgentDepartmentFilter : BasePageFilter
    {
        /// <summary>
        /// ID
        /// </summary>
        [Display(Name = "ID")]
        public Guid? ID { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Display(Name = "名称")]
        public string Name { get; set; }

        /// <summary>
        /// 代理门店审核状态
        /// </summary>
        [Display(Name = "代理门店审核状态")]
        public EAgentDepartmentApproveStatus? ApproveStatus { get; set; }
    }
}
