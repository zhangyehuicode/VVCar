using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVCar.VIP.Domain
{
    /// <summary>
    /// 卡片类型，默认
    /// </summary>
    public static class MemberCardTypes
    {
        /// <summary>
        /// 储值卡
        /// </summary>
        public static Guid PrePaidCard = new Guid("00000000-0000-0000-0000-000000000001");

        /// <summary>
        /// 折扣卡
        /// </summary>
        public static Guid DiscountCard = new Guid("00000000-0000-0000-0000-000000000002");

        /// <summary>
        /// 礼品卡
        /// </summary>
        public static Guid GiftCard = new Guid("00000000-0000-0000-0000-000000000003");
    }
}
