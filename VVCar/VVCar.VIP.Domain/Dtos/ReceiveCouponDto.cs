using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVCar.VIP.Domain.Dtos
{
    /// <summary>
    /// 领取卡券DTO
    /// </summary>
    public class ReceiveCouponDto
    {
        /// <summary>
        ///卡券ID
        /// </summary>
        public Guid? CouponID { get; set; }

        /// <summary>
        /// 领取者OpenID
        /// </summary>
        public string ReceiveOpenID { get; set; }

        /// <summary>
        /// 赠送者OpenID
        /// </summary>
        public string GivenOpenID { get; set; }

        /// <summary>
        /// 领取者昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 领取人微信头像
        /// </summary>
        public string HeadImgUrl { get; set; }

        /// <summary>
        /// 领取者手机号码
        /// </summary>
        public string MobilePhoneNo { get; set; }

        /// <summary>
        /// 领取卡券ID
        /// </summary>
        public IList<Guid> CouponTemplateIDs { get; set; }

        /// <summary>
        /// 是否来源于赠送
        /// </summary>
        public bool IsFromGiven { get; set; }

        /// <summary>
        /// 领取渠道
        /// </summary>
        public string ReceiveChannel { get; set; }

        /// <summary>
        /// 所需的兑换积分
        /// </summary>
        public int ExchangePoint { get; set; }

        /// <summary>
        /// 领券中心兑换设置ID
        /// </summary>
        public Guid PointExchangeCouponID { get; set; }

        /// <summary>
        /// 是否发送微信通知
        /// </summary>
        public bool SendNotify { get; set; }
    }
}
