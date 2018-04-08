﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.VIP.Domain.Enums;

namespace VVCar.VIP.Domain.Dtos
{
    /// <summary>
    /// 新增储值方案Dto
    /// </summary>
    public class NewUpdateRechargePlanDto
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NewUpdateRechargePlanDto"/> class.
        /// </summary>
        public NewUpdateRechargePlanDto()
        {
            //RechargePlanCouponTemplates = new List<RechargePlanCouponTemplate>();
        }

        /// <summary>
        /// ID
        /// </summary>
        [Display(Name = "ID")]
        public Guid ID { get; set; }

        /// <summary>
        /// 方案编号
        /// </summary>
        [Display(Name = "方案编号")]
        public String Code { get; set; }

        /// <summary>
        /// 方案名称
        /// </summary>
        [Display(Name = "方案名称")]
        public String Name { get; set; }

        /// <summary>
        /// 优惠类型
        /// </summary>
        [Display(Name = "优惠类型")]
        public EPlanType PlanType { get; set; }

        /// <summary>
        /// 是否可用
        /// </summary>
        [Display(Name = "可用")]
        public bool IsAvailable { get; set; }

        /// <summary>
        /// 生效日期
        /// </summary>
        [Display(Name = "生效日期")]
        public DateTime EffectiveDate { get; set; }

        /// <summary>
        /// 截止日期
        /// </summary>
        [Display(Name = "截止日期")]
        public DateTime ExpiredDate { get; set; }

        /// <summary>
        /// 储值金额
        /// </summary>
        [Display(Name = "储值金额")]
        public Decimal RechargeAmount { get; set; }

        /// <summary>
        /// 赠送金额
        /// </summary>
        [Display(Name = "赠送金额")]
        public Decimal GiveAmount { get; set; }

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
        public Nullable<Guid> LastUpdateUserID { get; set; }

        /// <summary>
        /// 最后修改人名称
        /// </summary>
        [Display(Name = "最后修改人名称")]
        public string LastUpdateUser { get; set; }

        /// <summary>
        /// 最后修改日期
        /// </summary>
        [Display(Name = "最后修改日期")]
        public Nullable<DateTime> LastUpdateDate { get; set; }

        /// <summary>
        /// 最多允许储值次数
        /// </summary>
        [Display(Name = "最多允许储值次数")]
        public int MaxRechargeCount { get; set; }

        /// <summary>
        /// 管理后台可见
        /// </summary>
        [Display(Name = "管理后台可见")]
        public bool VisibleAtPortal { get; set; }

        /// <summary>
        /// 微信前端可见
        /// </summary>
        [Display(Name = "微信前端可见")]
        public bool VisibleAtWeChat { get; set; }

        /// <summary>
        /// 适配储值卡
        /// </summary>
        [Display(Name = "适配储值卡")]
        public bool MatchRechargeCard { get; set; }

        /// <summary>
        /// 适配折扣卡
        /// </summary>
        [Display(Name = "适配折扣卡")]
        public bool MatchDiscountCard { get; set; }

        /// <summary>
        /// 适配礼品卡
        /// </summary>
        [Display(Name = "适配礼品卡")]
        public bool MatchGiftCard { get; set; }

        ///// <summary>
        ///// 优惠券
        ///// </summary>
        //[Display(Name = "优惠券")]
        //public ICollection<RechargePlanCouponTemplate> RechargePlanCouponTemplates { get; set; }
    }
}