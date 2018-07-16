using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEF.Core.Data;

namespace VVCar.VIP.Domain.Entities
{
    /// <summary>
    /// 会员分组
    /// </summary>
    public class MemberGroup : EntityBase
    {
        /// <summary>
        /// 分组名称
        /// </summary>
        [Display(Name = "分组名称")]
        public string Name { get; set; }

        /// <summary>
        /// 编号
        /// </summary>
        [Display(Name = "编号")]
        public string Code { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [Display(Name = "排序")]
        public int Index { get; set; }

        /// <summary>
        /// 是否成本价
        /// </summary>
        [Display(Name = "是否成本价")]
        public bool IsCostPrice { get; set; }
    }
}
