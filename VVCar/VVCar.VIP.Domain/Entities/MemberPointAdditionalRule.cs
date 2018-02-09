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
    /// 会员积分设置附加规则
    /// </summary>
    public class MemberPointAdditionalRule : NormalEntityBase
    {
        /// <summary>
        /// 会员积分设置ID
        /// </summary>
        [Display(Name = "会员积分设置ID")]
        public Guid MemberPointID { get; set; }

        /// <summary>
        /// 附加积分
        /// </summary>
        [Display(Name = "附加积分")]
        public int Point { get; set; }

        /// <summary>
        /// 次数(满几次送额外积分)
        /// </summary>
        [Display(Name = "次数(满几次送额外积分)")]
        public int Count { get; set; }
    }
}
