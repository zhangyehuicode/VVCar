using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVCar.VIP.Domain.Dtos
{
    /// <summary>
    /// 批量优惠券领取DTO
    /// </summary>
    public class BulkReceiveCouponDto
    {
        /// <summary>
        /// 领取劵会员信息
        /// </summary>
        public IList<ReceiveCouponMemberInfo> Members { get; set; }
        /// <summary>
        /// 领取卡券ID
        /// </summary>
        public IList<Guid> CouponTemplateIDs { get; set; }

    }

    /// <summary>
    /// 领取劵会员信息
    /// </summary>
    public class ReceiveCouponMemberInfo
    {
        /// <summary>
        /// 领取者OpenID
        /// </summary>
        public string ReceiveOpenID { get; set; }

        /// <summary>
        /// 领取者昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 领取者手机号码
        /// </summary>
        public string MobilePhoneNo { get; set; }
    }
}
