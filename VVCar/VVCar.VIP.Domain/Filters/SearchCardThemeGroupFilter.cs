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
    /// ID过滤
    /// </summary>
    public class SearchCardThemeGroupFilter : BasePageFilter
    {
        /// <summary>
        /// ID
        /// </summary>
        [Display(Name = "分组ID")]
        public Guid? ID { get; set; }
    }
}
