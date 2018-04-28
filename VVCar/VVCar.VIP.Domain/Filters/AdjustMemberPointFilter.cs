using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.VIP.Domain.Enums;

namespace VVCar.VIP.Domain.Filters
{
    /// <summary>
    /// 调整会员积分参数
    /// </summary>
    public class AdjustMemberPointFilter
    {
        /// <summary>
        /// 会员ID
        /// </summary>
        public Guid MemberID { get; set; }

        /// <summary>
        /// 会员积分类型
        /// </summary>
        public EMemberPointType PointType { get; set; }

        /// <summary>
        /// 调整的积分
        /// </summary>
        public int AdjustPoints { get; set; }
    }
}
