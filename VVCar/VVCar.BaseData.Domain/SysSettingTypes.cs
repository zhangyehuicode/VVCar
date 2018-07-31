using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVCar.BaseData.Domain
{
    /// <summary>
    /// 系统设置类型
    /// </summary>
    public static class SysSettingTypes
    {
        /// <summary>
        /// 会员储值微信通知消息模板
        /// </summary>
        public const string WXMsg_MemberRecharge = "WXMsg_MemberRecharge";

        /// <summary>
        /// 会员消费微信通知消息模板
        /// </summary>
        public const string WXMsg_MemberConsume = "WXMsg_MemberConsume";

        /// <summary>
        /// 会员账户调整微信通知消息模板
        /// </summary>
        public const string WXMsg_MemberAdjust = "WXMsg_MemberAdjust";

        /// <summary>
        /// 优惠券领取成功微信通知消息模板
        /// </summary>
        public const string WXMsg_CouponReceived = "WXMsg_CouponReceived";

        /// <summary>
        /// 优惠券使用微信通知消息模板
        /// </summary>
        public const string WXMsg_CouponUsed = "WXMsg_CouponUsed";

        /// <summary>
        /// 优惠券即将过期微信通知消息模板
        /// </summary>
        public const string WXMsg_CouponWillExpire = "WXMsg_CouponWillExpire";

        /// <summary>
        /// 会员升级通知消息模板
        /// </summary>
        public const string WXMsg_UpGrade = "WXMsg_UpGrade";

        /// <summary>
        /// 下单成功通知消息模板
        /// </summary>
        public const string WXMsg_OrderSuccess = "WXMsg_OrderSuccess";

        /// <summary>
        /// 新订单通知消息模板
        /// </summary>
        public const string WXMsg_OrderRemind = "WXMsg_OrderRemind";

        /// <summary>
        /// 礼品领取成功通知消息模板
        /// </summary>
        public const string WXMsg_ReceivedSuccess = "WXMsg_ReceivedSuccess";

        /// <summary>
        /// 预约提醒通知消息模板
        /// </summary>
        public const string WXMsg_AppointmentRemind = "WXMsg_AppointmentRemind";

        /// <summary>
        /// 预约成功通知消息模板
        /// </summary>
        public const string WXMsg_AppointmentSuccess = "WXMsg_AppointmentSuccess";

        /// <summary>
        /// 核销成功通知消息模板
        /// </summary>
        public const string WXMsg_VerificationSuccess = "WXMsg_VerificationSuccess";

        /// <summary>
        /// 服务到期提醒消息模板
        /// </summary>
        public const string WXMsg_ServiceExpiredRemind = "WXMsg_ServiceExpiredRemind";

        /// <summary>
        /// 游戏通知消息模板
        /// </summary>
        public const string WXMsg_GameRemind = "WXMsg_GameRemind";

        /// <summary>
        /// 订单发货通知模板
        /// </summary>
        public const string WXMsg_DeliveryRemind = "WXMsg_DeliveryRemind";
    }
}
