using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVCar.VIP.Domain.Dtos
{
    /// <summary>
    /// 立即推送Dto
    /// </summary>
    public class ImmediatePushDto
    {
        /// <summary>
        /// 卡券IDs
        /// </summary>
        public Guid[] CouponTemplateIDs { get; set; }

        /// <summary>
        /// 会员IDs
        /// </summary>
        public Guid[] MemberIDs { get; set; }
    }
}
