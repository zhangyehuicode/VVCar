using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEF.Core.Dtos;

namespace VVCar.Shop.Domain.Filters
{
    /// <summary>
    /// 门店开发业绩过滤器
    /// </summary>
    public class DepartmentPerformanceFilter : BasePageFilter
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        [Display(Name = "开始时间")]
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        [Display(Name = "结束时间")]
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// 员工姓名
        /// </summary>
        [Display(Name = "员工姓名")]
        public string StaffName { get; set; }
    }
}
