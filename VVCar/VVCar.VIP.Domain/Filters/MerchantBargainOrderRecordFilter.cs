using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEF.Core.Dtos;

namespace VVCar.VIP.Domain.Filters
{
    /// <summary>
    /// 发起砍价记录过滤条件
    /// </summary>
    public class MerchantBargainOrderRecordFilter : BasePageFilter
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid? ID { get; set; }

        /// <summary>
        /// 发起人ID(会员ID)
        /// </summary>
        public Guid? MemberID { get; set; }
    }
}
