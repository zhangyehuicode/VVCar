using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace VVCar.VIP.Domain.Enums
{
    /// <summary>
    /// 积分类型
    /// </summary>
    public enum EMemberPointType
    {
        /// <summary>
        /// 注册
        /// </summary>
        [Description("注册送积分")]
        Register = 0,

        /// <summary>
        /// 签到
        /// </summary>
        [Description("签到送积分")]
        SignIn = 1,

        /// <summary>
        /// 分享
        /// </summary>
        [Description("分享送积分")]
        Share = 2,

        /// <summary>
        /// 评价
        /// </summary>
        [Description("评价送积分")]
        Appraise = 3,

        /// <summary>
        /// 会员等级升级
        /// </summary>
        [Description("会员等级升级送积分")]
        MemberGradeUpgrade = 4,

        /// <summary>
        /// 会员消费送积分
        /// </summary>
        [Description("会员消费送积分")]
        MemberConsume = 5,

        /// <summary>
        /// 会员储值送积分
        /// </summary>
        [Description("会员储值送积分")]
        MemberRecharge = 6,

        /// <summary>
        /// 管理后台调整积分
        /// </summary>
        [Description("管理后台调整积分")]
        MemberAdjust = 7,

        /// <summary>
        /// 会员卡消费返积分
        /// </summary>
        [Description("会员卡消费返积分")]
        MemberCardConsumeReturn = 8,

        /// <summary>
        /// 会员积分抵扣
        /// </summary>
        [Description("会员积分抵扣")]
        PosDeductionUse = -1,

        /// <summary>
        /// 兑换卡券使用
        /// </summary>
        [Description("兑换卡券使用")]
        ExchangeCouponUse = -2,
    }
}
