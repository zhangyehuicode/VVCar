using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEF.Core.Dtos;

namespace VVCar.VIP.Domain.Filters
{
    public class CrowdOrderRecordFilter : BasePageFilter
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid? ID { get; set; }

        /// <summary>
        /// 发起人ID（车比特会员ID）
        /// </summary>
        public Guid? CarBitCoinMemberID { get; set; }
    }
}
