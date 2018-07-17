using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVCar.VIP.Domain.Dtos
{
    /// <summary>
    /// 改变会员所属分组
    /// </summary>
    public class ChangeMemberGroupDto
    {
        /// <summary>
        /// 会员ID
        /// </summary>
        public IEnumerable<Guid> MemberIDList { get; set; }

        /// <summary>
        /// 会员分组ID
        /// </summary>
        public Guid? MemberGroupID { get; set; }
    }
}
