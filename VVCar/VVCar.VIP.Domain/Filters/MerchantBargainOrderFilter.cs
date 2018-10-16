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
    /// 门店砍价过滤条件
    /// </summary>
    public class MerchantBargainOrderFilter : BasePageFilter
    {
        /// <summary>
        /// 拼单名称
        /// </summary>
        [Display(Name = "拼单名称")]
        public string Name { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        [Display(Name = "是否启用")]
        public bool? IsAvailable { get; set; }
    }
}
