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
    /// 系统参数过滤条件
    /// </summary>
    public class SystemSettingFilter : BasePageFilter
    {
        /// <summary>
        /// 餐厅名称
        /// </summary>
        [Display(Name = "参数名称")]
        public string Name { get; set; }
    }
}
