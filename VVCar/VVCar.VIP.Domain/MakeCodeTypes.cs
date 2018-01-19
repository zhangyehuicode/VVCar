using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVCar.VIP.Domain
{
    /// <summary>
    /// 编号类型
    /// </summary>
    public static class MakeCodeTypes
    {
        /// <summary>
        /// 微信会员卡
        /// </summary>
        public const string WeChatMemberCard = "WeChatMemberCard";

        /// <summary>
        /// 储值订单
        /// </summary>
        public const string RechargeBill = "RechargeBill";

        /// <summary>
        /// 消费订单
        /// </summary>
        public const string ConsumeBill = "ConsumeBill";

        /// <summary>
        /// 卡片批次代码
        /// </summary>
        public const string MemberCardBatchCode = "MemberCardBatchCode";

        /// <summary>
        /// 优惠券模板代码
        /// </summary>
        public const string CouponTemplateCode = "CouponTemplateCode";
    }
}
