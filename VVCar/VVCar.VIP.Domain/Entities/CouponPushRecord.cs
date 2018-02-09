using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEF.Core.Data;

namespace VVCar.VIP.Domain.Entities
{
    /// <summary>
    /// 卡券推送记录
    /// </summary>
    public class CouponPushRecord : NormalEntityBase
    {
        /// <summary>
        /// 卡券推送ID
        /// </summary>
        public Guid CouponPushID { get; set; }

        /// <summary>
        /// 推送的人员ID
        /// </summary>
        public Guid PushedUserID { get; set; }

        /// <summary>
        /// 微信OpenID
        /// </summary>
        public string WeChatOpenID { get; set; }

        /// <summary>
        /// 推送年份，用于识别会员生日类推送是否有执行
        /// </summary>
        public int PushYear { get; set; }

        /// <summary>
        /// 推送时间
        /// </summary>
        public DateTime PushDate { get; set; }
    }
}
