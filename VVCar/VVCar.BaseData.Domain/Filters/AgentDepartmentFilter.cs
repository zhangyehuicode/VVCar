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
    }
}
