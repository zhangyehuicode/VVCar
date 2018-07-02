using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEF.Core.Dtos;

namespace VVCar.VIP.Domain.Filters
{
    /// <summary>
    /// 人才招聘过滤条件
    /// </summary>
    public class RecruitmentFilter : BasePageFilter
    {
        /// <summary>
        /// 招聘单位
        /// </summary>
        [Display(Name = "招聘单位")]
        public string Recruiter { get; set; }
    }
}
