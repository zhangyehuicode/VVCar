using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.VIP.Domain.Enums;
using YEF.Core.Dtos;

namespace VVCar.VIP.Domain.Filters
{
    /// <summary>
    /// 业务报销过滤条件
    /// </summary>
    public class ReimbursementFilter : BasePageFilter
    {
        /// <summary>
        /// 报销人ID
        /// </summary>
        [Display(Name = "报销人ID")]
        public Guid? UserID { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        [Display(Name = "项目名称")]
        public string Project { get; set; }

        /// <summary>
        /// 业务报销审核状态
        /// </summary>
        [Display(Name = "业务报销审核状态")]
        public EReimbursementApproveStatus? Status { get; set; }
    }
}
