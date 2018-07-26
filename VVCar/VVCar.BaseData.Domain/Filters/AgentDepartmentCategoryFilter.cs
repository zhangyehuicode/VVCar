using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEF.Core.Dtos;

namespace VVCar.BaseData.Domain.Filters
{
    /// <summary>
    /// 门店分类过滤条件
    /// </summary>
    public class AgentDepartmentCategoryFilter : BasePageFilter
    {
        /// <summary>
        /// 类别名称
        /// </summary>
        [Display(Name = "类别名称")]
        public string Name { get; set; }

        /// <summary>
        /// 类别编码
        /// </summary>
        [Display(Name = "类别编码")]
        public string Code { get; set; }

        /// <summary>
        /// 名称或者编号
        /// </summary>
        [Display(Name = "名称或者编号")]
        public string NameOrCode { get; set; }
    }
}
