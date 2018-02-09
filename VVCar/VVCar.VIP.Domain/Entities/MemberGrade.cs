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
    /// 会员等级
    /// </summary>
    public class MemberGrade : NormalEntityBase
    {
        /// <summary>
        /// 会员等级
        /// </summary>
        public MemberGrade()
        {
            GradeRights = new List<MemberGradeRight>();
        }

        /// <summary>
        /// 等级名称
        /// </summary>
        [Display(Name = "等级名称")]
        public string Name { get; set; }

        /// <summary>
        /// 是否设置为默认等级
        /// </summary>
        [Display(Name = "是否设置为默认等级")]
        public bool IsDefault { get; set; }

        /// <summary>
        /// 等级排序，数值越大，等级越高
        /// </summary>
        [Display(Name = "等级排序")]
        public int Level { get; set; }

        /// <summary>
        /// 是否永久有效
        /// </summary>
        [Display(Name = "是否永久有效")]
        public bool IsNeverExpires { get; set; }

        /// <summary>
        /// 发卡/升级后？天（按自然日计算含当日）
        /// </summary>
        [Display(Name = "发卡/升级后失效天数")]
        public int? ExpireAfterJoinDays { get; set; }

        /// <summary>
        /// 是否通过消费获得资格
        /// </summary>
        [Display(Name = "是否通过消费获得资格")]
        public bool IsQualifyByConsume { get; set; }

        /// <summary>
        /// 累计消费x元，获得资格
        /// </summary>
        [Display(Name = "累计消费x元，获得资格")]
        public decimal? QualifyByConsumeTotalAmount { get; set; }

        /// <summary>
        /// 一次性消费x元，获得资格
        /// </summary>
        [Display(Name = "一次性消费x元，获得资格")]
        public decimal? QualifyByConsumeOneOffAmount { get; set; }

        /// <summary>
        /// 累计x个月内，累计消费达 QualifyByConsumeTotalCount 次，获得资格
        /// </summary>
        [Display(Name = "累计x个月内，获得资格")]
        public int? QualifyByConsumeLimitedMonths { get; set; }

        /// <summary>
        /// 累计 QualifyByConsumeLimitedMonths 个月内，累计消费达 ？ 次，获得资格
        /// </summary>
        [Display(Name = "累计消费达x次，获得资格")]
        public int? QualifyByConsumeTotalCount { get; set; }

        /// <summary>
        /// 是否通过储值获得资格
        /// </summary>
        [Display(Name = "是否通过储值获得资格")]
        public bool IsQualifyByRecharge { get; set; }

        /// <summary>
        /// 累计储值x元，获得资格
        /// </summary>
        [Display(Name = "累计储值x元，获得资格")]
        public decimal? QualifyByRechargeTotalAmount { get; set; }

        /// <summary>
        /// 一次性储值x元，获得资格
        /// </summary>
        [Display(Name = "一次性储值x元，获得资格")]
        public decimal? QualifyByRechargeOneOffAmount { get; set; }

        /// <summary>
        /// 是否通过购买获得资格
        /// </summary>
        [Display(Name = "是否通过购买获得资格")]
        public bool IsQualifyByPurchase { get; set; }

        /// <summary>
        /// 直接花费x元，获得资格
        /// </summary>
        [Display(Name = "直接花费x元，获得资格")]
        public decimal? QualifyByPurchaseAmount { get; set; }

        /// <summary>
        /// 是否支持差价购买
        /// </summary>
        [Display(Name = "是否支持差价购买")]
        public bool IsAllowDiffPurchaseAmount { get; set; }

        /// <summary>
        /// 等级初始积分
        /// </summary>
        [Display(Name = "等级初始积分")]
        public int GradePoint { get; set; }

        /// <summary>
        /// 每消费x元，送 ConsumeGiftPoint 积分
        /// </summary>
        [Display(Name = "每消费x元，送x积分")]
        public decimal? GiftPointByConsumeAmount { get; set; }

        /// <summary>
        /// 每消费 GiftPointByConsumeAmount 元，送x积分
        /// </summary>
        [Display(Name = "消费，送x积分")]
        public int? ConsumeGiftPoint { get; set; }

        /// <summary>
        /// 每储值x元，送 RechargeGiftPoint 积分
        /// </summary>
        [Display(Name = "每储值x元，送x积分")]
        public decimal? GiftPointByRechargeAmount { get; set; }

        /// <summary>
        /// 每消费 GiftPointByRechargeAmount 元，送x积分
        /// </summary>
        [Display(Name = "储值，送x积分")]
        public int? RechargeGiftPoint { get; set; }

        /// <summary>
        /// 是否关联YunPos
        /// </summary>
        [Display(Name = "是否关联YunPos")]
        public bool IsYunPosIntegration { get; set; }

        /// <summary>
        /// 折扣系数，0-1 保留2位小数
        /// </summary>
        [Display(Name = "折扣系数")]
        public decimal? DiscountRate { get; set; }

        /// <summary>
        /// 是否支持积分支付
        /// </summary>
        [Display(Name = "是否支持积分支付")]
        public bool IsAllowPointPayment { get; set; }

        /// <summary>
        /// 1积分抵扣x元
        /// </summary>
        [Display(Name = "1积分抵扣x元")]
        public decimal? PonitExchangeValue { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [Display(Name = "状态")]
        public EMemberGradeStatus Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Display(Name = "备注")]
        public string Remark { get; set; }

        /// <summary>
        /// 是否不对外开放
        /// </summary>
        [Display(Name = "是否不对外开放")]
        public bool IsNotOpen { get; set; }

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
        /// 最后修改人ID
        /// </summary>
        [Display(Name = "最后修改人ID")]
        public Guid? LastUpdateUserID { get; set; }

        /// <summary>
        /// 最后修改人名称
        /// </summary>
        [Display(Name = "最后修改人名称")]
        public string LastUpdateUser { get; set; }

        /// <summary>
        /// 最后修改日期
        /// </summary>
        [Display(Name = "最后修改日期")]
        public DateTime? LastUpdateDate { get; set; }

        /// <summary>
        /// 会员权益
        /// </summary>
        public virtual ICollection<MemberGradeRight> GradeRights { get; set; }
    }
}
