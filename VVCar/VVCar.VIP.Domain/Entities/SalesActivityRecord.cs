using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.VIP.Domain.Enums;
using YEF.Core.Data;

namespace VVCar.VIP.Domain.Entities
{
    /// <summary>
    /// 营销活动参与记录
    /// </summary>
    public class SalesActivityRecord : NormalEntityBase
    {
        /// <summary>
        /// 活动类型
        /// </summary>
        public ESalesActivityType ActivityType { get; set; }

        /// <summary>
        /// 微信OpenID
        /// </summary>
        public string WeChatOpenID { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        public string RetailBillCode { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string MobilePhoneNo { get; set; }

        /// <summary>
        /// 奖品
        /// </summary>
        public string Prize { get; set; }

        /// <summary>
        /// 参与时间
        /// </summary>
        public DateTime CreatedDate { get; set; }
    }
}
