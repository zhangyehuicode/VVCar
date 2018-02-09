using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.VIP.Domain.Enums;
using YEF.Core.Dtos;

namespace VVCar.VIP.Domain.Filters
{
    /// <summary>
    /// 会员等级过滤
    /// </summary>
    public class MemberGradeFilter : BasePageFilter
    {
        /// <summary>
        /// 是否关闭
        /// </summary>
        public EMemberGradeStatus? Status { get; set; }
    }
}
