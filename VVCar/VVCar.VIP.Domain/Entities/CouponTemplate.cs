using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.VIP.Domain.Enums;
using YEF.Core.Data;

namespace VVCar.VIP.Domain.Entities
{
    /// <summary>
    /// 优惠券模板
    /// </summary>
    public class CouponTemplate : EntityBase
    {
        /// <summary>
        /// 优惠券模板
        /// </summary>
        public CouponTemplate()
        {
            UseTimeList = new List<CouponTemplateUseTime>();
        }

        /// <summary>
        /// 优惠券模板编号
        /// </summary>
        [Display(Name = "优惠券模板编号")]
        public string TemplateCode { get; set; }

        /// <summary>
        /// 性质
        /// </summary>
        [Display(Name = "性质")]
        public ENature Nature { get; set; }

        /// <summary>
        /// 是否推荐
        /// </summary>
        [Display(Name = "是否推荐")]
        public bool IsRecommend { get; set; }

        /// <summary>
        ///  类型
        /// </summary>
        [Display(Name = "类型")]
        public ECouponType CouponType { get; set; }

        /// <summary>
        ///  颜色
        /// </summary>
        [Display(Name = "颜色")]
        public string Color { get; set; }

        /// <summary>
        ///  券面值，抵用券时为抵用金额，代金券时为减免金额，折扣券时为折扣比例
        /// </summary>
        [Display(Name = "抵用金额")]
        public decimal CouponValue { get; set; }

        /// <summary>
        ///  标题
        /// </summary>
        [Display(Name = "标题")]
        public string Title { get; set; }

        /// <summary>
        ///  副标题
        /// </summary>
        [Display(Name = "副标题")]
        public string SubTitle { get; set; }

        /// <summary>
        ///  是否限制最低消费
        /// </summary>
        [Display(Name = "是否限制最低消费")]
        public bool IsMinConsumeLimit { get; set; }

        /// <summary>
        ///  最低消费金额，单位(元)
        /// </summary>
        [Display(Name = "最低消费金额")]
        public decimal MinConsume { get; set; }

        /// <summary>
        ///  不与其他优惠共享
        /// </summary>
        [Display(Name = "不与其他优惠共享")]
        public bool IsExclusive { get; set; }

        /// <summary>
        ///  适用商品编码，以,分隔
        /// </summary>
        [Display(Name = "适用商品")]
        public string IncludeProducts { get; set; }

        /// <summary>
        ///  不适用商品编码，以,分隔
        /// </summary>
        [Display(Name = "不适用商品")]
        public string ExcludeProducts { get; set; }

        /// <summary>
        /// 是否固定有效期
        /// </summary>
        [Display(Name = "是否固定有效期")]
        public bool IsFiexedEffectPeriod { get; set; }

        /// <summary>
        /// 生效日期
        /// </summary>
        [Display(Name = "生效日期")]
        public DateTime? EffectiveDate { get; set; }

        /// <summary>
        /// 截止日期
        /// </summary>
        [Display(Name = "截止日期")]
        public DateTime? ExpiredDate { get; set; }

        /// <summary>
        ///投放开始时间
        /// </summary>
        [Display(Name = "投放开始时间")]
        public DateTime? PutInStartDate { get; set; }

        /// <summary>
        ///投放结束时间
        /// </summary>
        [Display(Name = "投放结束时间")]
        public DateTime? PutInEndDate { get; set; }

        /// <summary>
        ///  领取后多少天生效
        /// </summary>
        [Display(Name = "领取后多少天生效")]
        public int? EffectiveDaysAfterReceived { get; set; }

        /// <summary>
        ///  有效天数
        /// </summary>
        [Display(Name = "有效天数")]
        public int? EffectiveDays { get; set; }

        /// <summary>
        ///  是否全时段(使用优惠券)
        /// </summary>
        [Display(Name = "是否全时段(使用优惠券)")]
        public bool IsUseAllTime { get; set; }

        /// <summary>
        ///  是否全时段(投放)
        /// </summary>
        [Display(Name = "是否全时段(投放)")]
        public bool PutInIsUseAllTime { get; set; }

        /// <summary>
        ///  可用的日期星期，周日到周六为0..6递增以,分隔
        /// </summary>
        [Display(Name = "可用的日期")]
        public string UseDaysOfWeek { get; set; }

        /// <summary>
        ///  投放可用的日期星期，周日到周六为0..6递增以,分隔
        /// </summary>
        [Display(Name = "投放可用的日期")]
        public string PutInUseDaysOfWeek { get; set; }

        ///// <summary>
        ///// 投放开始日期
        ///// </summary>
        //[Display(Name = "投放开始日期")]
        //public DateTime DeliveryStartDate { get; set; }

        ///// <summary>
        ///// 投放结束日期
        ///// </summary>
        //[Display(Name = "投放结束日期")]
        //public DateTime DeliveryFinishDate { get; set; }

        /// <summary>
        ///  封面图片
        /// </summary>
        [Display(Name = "封面图片")]
        public string CoverImage { get; set; }

        /// <summary>
        ///  封面简介
        /// </summary>
        [Display(Name = "封面简介")]
        public string CoverIntro { get; set; }

        /// <summary>
        ///  使用须知
        /// </summary>
        [Display(Name = "使用须知")]
        public string UseInstructions { get; set; }

        /// <summary>
        ///  图文介绍
        /// </summary>
        [Display(Name = "图文介绍")]
        public string IntroDetail { get; set; }

        /// <summary>
        ///  商户电话
        /// </summary>
        [Display(Name = "商户电话")]
        public string MerchantPhoneNo { get; set; }

        /// <summary>
        ///  商户服务
        /// </summary>
        [Display(Name = "商户服务")]
        public EMerchantService MerchantService { get; set; }

        /// <summary>
        /// 用户可分享链接
        /// </summary>
        [Display(Name = "用户可分享链接")]
        public bool CanShareByPeople { get; set; }

        /// <summary>
        /// 用户可以赠送优惠券
        /// </summary>
        [Display(Name = "用户可以赠送优惠券")]
        public bool CanGiveToPeople { get; set; }

        /// <summary>
        ///  核销方式
        /// </summary>
        [Display(Name = "核销方式")]
        public EVerificationMode VerificationMode { get; set; }

        /// <summary>
        /// 全部门店适用
        /// </summary>
        [Display(Name = "全部门店适用")]
        public bool IsApplyAllStore { get; set; }

        /// <summary>
        ///  适用门店编码，以,分隔
        /// </summary>
        [Display(Name = "适用门店编码")]
        public string ApplyStores { get; set; }

        /// <summary>
        ///  操作提示
        /// </summary>
        [Display(Name = "操作提示")]
        public string OperationTips { get; set; }

        /// <summary>
        ///  审核状态
        /// </summary>
        [Display(Name = "审核状态")]
        public EApproveStatus ApproveStatus { get; set; }

        /// <summary>
        ///  投放方式
        /// </summary>
        [Display(Name = "投放方式")]
        public EDeliveryMode DeliveryMode { get; set; }

        /// <summary>
        ///  是否为特殊代销券
        /// </summary>
        [Display(Name = "是否为特殊代销券")]
        public bool IsSpecialCoupon { get; set; }

        /// <summary>
        /// 是否可用
        /// </summary>
        [Display(Name = "是否可用")]
        public bool IsAvailable { get; set; }

        /// <summary>
        /// 创建人ID
        /// </summary>
        [Display(Name = "创建人ID")]
        public Guid CreatedUserID { get; set; }

        /// <summary>
        /// 创建人名称
        /// </summary>
        [Display(Name = "创建人名称")]
        public string CreatedUser { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        [Display(Name = "创建日期")]
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// 审核人ID
        /// </summary>
        [Display(Name = "审核人ID")]
        public Guid? ApprovedUserID { get; set; }

        /// <summary>
        /// 审核人名称
        /// </summary>
        [Display(Name = "审核人名称")]
        public string ApprovedUser { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        [Display(Name = "创建日期")]
        public DateTime? ApprovedDate { get; set; }

        /// <summary>
        ///是否在微信领券中心显示(是否上架)
        /// </summary>
        public bool IsPutaway { get; set; }

        /// <summary>
        /// 上架时间
        /// </summary>
        public DateTime? PutawayTime { get; set; }

        /// <summary>
        /// 下架时间
        /// </summary>
        public DateTime? SoldOutTime { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Display(Name = "备注")]
        public string Remark { get; set; }

        /// <summary>
        /// 是否优先抵扣
        /// </summary>
        [Display(Name = "是否优先抵扣")]
        public bool IsDeductionFirst { get; set; }

        /// <summary>
        /// 优惠券库存
        /// </summary>
        public virtual CouponTemplateStock Stock { get; set; }

        /// <summary>
        /// 可用部分时段
        /// </summary>
        public ICollection<CouponTemplateUseTime> UseTimeList { get; set; }

        /// <summary>
        /// 获取生效日期
        /// </summary>
        /// <returns></returns>
        public DateTime GetEffectiveDate()
        {
            if (IsFiexedEffectPeriod)
                return EffectiveDate.Value.Date;
            else
                return DateTime.Today.AddDays(EffectiveDaysAfterReceived.GetValueOrDefault());
        }

        /// <summary>
        /// 获取截止日期
        /// </summary>
        /// <returns></returns>
        public DateTime GetExpiredDate()
        {
            DateTime expiredDate;
            if (IsFiexedEffectPeriod)
                expiredDate = ExpiredDate.Value.Date;
            else
                expiredDate = DateTime.Today.AddDays((EffectiveDaysAfterReceived.GetValueOrDefault() - 1) + EffectiveDays.GetValueOrDefault());
            return expiredDate.AddDays(1).AddSeconds(-1);
        }
    }
}
