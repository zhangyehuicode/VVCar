using System;
using System.ComponentModel.DataAnnotations;
using VVCar.BaseData.Domain.Enums;
using YEF.Core.Dtos;

namespace VVCar.BaseData.Domain.Filters
{
    /// <summary>
    /// 代理商门店过滤器
    /// </summary>
    public class AgentDepartmentFilter : BasePageFilter
    {
        /// <summary>
        /// 商户名称
        /// </summary>
        [Display(Name = "商户名称")]
        public string MerchantName { get; set; }

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

        /// <summary>
        /// 创建时间
        /// </summary>
        [Display(Name = "创建时间")]
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// 代理商门店ID
        /// </summary>
        [Display(Name = "代理商门店ID")]
        public Guid? AgentDepartmentID { get; set; }

        /// <summary>
        /// 销售经理ID
        /// </summary>
        [Display(Name = "销售经理ID")]
        public Guid? UserID { get; set; }
    }
}
