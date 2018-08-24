using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVCar.VIP.Domain.Dtos
{
    /// <summary>
    /// 会员卡DTO
    /// </summary>
    public class MemberCardDto
    {
        /// <summary>
        /// 卡片ID
        /// </summary>
        public Guid CardID { get; set; }

        /// <summary>
        /// 卡片编号
        /// </summary>
        public string CardNumber { get; set; }

        /// <summary>
        /// 校验码
        /// </summary>
        public string VerifyCode { get; set; }

        /// <summary>
        /// 卡片类型
        /// </summary>
        public string CardType { get; set; }

        /// <summary>
        /// 卡片类型ID
        /// </summary>
        public string CardTypeID { get; set; }

        /// <summary>
        /// 允许折扣
        /// </summary>
        public bool AllowDiscount { get; set; }

        /// <summary>
        /// 允许折扣
        /// </summary>
        public bool AllowRecharge { get; set; }

        /// <summary>
        /// 卡片状态
        /// </summary>
        public string CardStatus { get; set; }

        /// <summary>
        /// 生效日期
        /// </summary>
        public string EffectiveDate { get; set; }

        /// <summary>
        /// 截止日期
        /// </summary>
        public string ExpiredDate { get; set; }

        /// <summary>
        /// 余额
        /// </summary>
        public decimal CardBalance { get; set; }

        /// <summary>
        /// 会员ID
        /// </summary>
        public Guid? MemberID { get; set; }

        /// <summary>
        /// 微信OpenID
        /// </summary>
        public string WeChatOpenID { get; set; }

        /// <summary>
        /// 小程序OpenID
        /// </summary>
        public string MinProOpenID { get; set; }

        /// <summary>
        /// 会员姓名
        /// </summary>
        public string MemberName { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string MobilePhoneNo { get; set; }

        /// <summary>
        /// 会员生日
        /// </summary>
        public string Birthday { get; set; }

        /// <summary>
        /// 会员性别
        /// </summary>
        public string Sex { get; set; }

        /// <summary>
        /// 会员分组
        /// </summary>
        public string MemberGroup { get; set; }

        /// <summary>
        ///是否已激活
        /// </summary>
        public bool IsActivate { get; set; }

        /// <summary>
        /// 会员等级
        /// </summary>
        public string MemberGrade { get; set; }

        /// <summary>
        /// 会员积分
        /// </summary>
        public double MemberPoint { get; set; }

        /// <summary>
        /// 会员卡数量
        /// </summary>
        public int CardCount { get; set; }

        /// <summary>
        /// 车牌号
        /// </summary>
        public string Plate { get; set; }

        /// <summary>
        /// 是否支持积分支付
        /// </summary>
        public bool IsAllowPointPayment { get; set; }

        /// <summary>
        /// 1积分抵扣x元
        /// </summary>
        public decimal? PonitExchangeValue { get; set; }

        /// <summary>
        /// 会员折扣系数
        /// </summary>
        public decimal? DiscountRate { get; set; }

        ///// <summary>
        ///// 会员折扣权益
        ///// </summary>
        //public List<MemberPosRightDto> MemberDiscountRight { get; set; }

        ///// <summary>
        ///// 会员商品权益
        ///// </summary>
        //public List<MemberPosRightDto> MemberProductRight { get; set; }

        /// <summary>
        /// 卡片主题图片路径
        /// </summary>
        public string CardThemeImgUrl { get; set; }

        /// <summary>
        /// 规则说明
        /// </summary>
        public string RuleDescription { get; set; }

        /// <summary>
        /// 是否虚拟卡
        /// </summary>
        public bool IsVirtual { get; set; }

        /// <summary>
        /// 适用门店
        /// </summary>
        public string UserCode { get; set; }

        /// <summary>
        /// 适用开始日期
        /// </summary>
        public DateTime? UserGiftCardStartTime { get; set; }

        /// <summary>
        /// 截止日期
        /// </summary>
        public DateTime? UserGiftCardEndTime { get; set; }

        /// <summary>
        /// 适用是否启用
        /// </summary>
        public bool UserIsAvailable { get; set; }

        /// <summary>
        /// 时间段(是否全部)
        /// </summary>
        public bool UserTimeSlot { get; set; }

        /// <summary>
        /// 适用星期
        /// </summary>
        public string UserWeek { get; set; }

        /// <summary>
        /// 购买后几天生效
        /// </summary>
        public int EffectiveDaysOfAfterBuy { get; set; }

        /// <summary>
        /// 有效天数
        /// </summary>
        public int EffectiveDays { get; set; }

        /// <summary>
        /// 是否为自定义日期
        /// </summary>
        public bool IsNotFixationDate { get; set; }

        /// <summary>
        /// 是否对外开放
        /// </summary>
        public bool IsNotOpen { get; set; }

        /// <summary>
        /// 适用时间段
        /// </summary>
        public List<GiftCardUserTime> GiftCardUserTimeList { get; set; }

        /// <summary>
        /// 当前等级消费金额
        /// </summary>
        public decimal? ConsumeAmountOfCurrentGrade { get; set; }

        /// <summary>
        /// 当前等级消费次数
        /// </summary>
        public int? ConsumeCountOfCurrentGrade { get; set; }

        /// <summary>
        /// 达到下一个等级所需消费金额
        /// </summary>
        public decimal? ConsumeAmountOfReachNextGrade { get; set; }

        /// <summary>
        /// 达到下一个等级所需消费次数
        /// </summary>
        public int? ConsumeCountOfReachNextGrade { get; set; }

        /// <summary>
        /// 当前等级消费金额占比（/达到下一等级消费金额）
        /// </summary>
        public decimal CurrentConsumeAmountRate { get; set; }

        /// <summary>
        /// 当前等级消费次数占比（/达到下一等级消费次数）
        /// </summary>
        public decimal CurrentConsumeCountRate { get; set; }
    }

    public class GiftCardUserTime
    {
        /// <summary>
        /// 
        /// </summary>
        public string BeginTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string EndTime { get; set; }
    }
}
