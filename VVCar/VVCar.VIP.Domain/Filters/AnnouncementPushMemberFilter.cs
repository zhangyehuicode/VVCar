using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEF.Core.Dtos;

namespace VVCar.VIP.Domain.Filters
{
    /// <summary>
    /// 公告推送会员过滤条件
    /// </summary>
    public class AnnouncementPushMemberFilter : BasePageFilter
    {
        /// <summary>
        /// 公告ID
        /// </summary>
        public Guid AnnouncementID { get; set; }
    }
}
