using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEF.Core.Data;
using System.ComponentModel.DataAnnotations;
using VVCar.VIP.Domain.Enums;

namespace VVCar.VIP.Domain.Entities
{
    /// <summary>
    /// 会员卡
    /// </summary>
    public class MemberCard : NormalEntityBase
    {
        /// <summary>
        /// 卡片编号
        /// </summary>
        [Display(Name = "卡片编号")]
        public string Code { get; set; }

        /// <summary>
        /// 校验码
        /// </summary>
        [Display(Name = "校验码")]
        public string VerifyCode { get; set; }

        /// <summary>
        /// 批次代码
        /// </summary>
        [Display(Name = "批次代码")]
        public string BatchCode { get; set; }

        /// <summary>
        /// 卡片类型ID
        /// </summary>
        [Display(Name = "卡片类型ID")]
        public Guid? CardTypeID { get; set; }

        /// <summary>
        /// 卡片类型
        /// </summary>
        [Display(Name = "卡片类型")]
        public virtual MemberCardType CardType { get; set; }

        /// <summary>
        /// 卡片状态
        /// </summary>
        [Display(Name = "卡片状态")]
        public ECardStatus Status { get; set; }

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
        /// 余额
        /// </summary>
        [Display(Name = "余额")]
        public decimal CardBalance { get; set; }

        /// <summary>
        /// 历史储值总额(用户实际支付)
        /// </summary>
        [Display(Name = "历史储值总额(用户实际支付)")]
        public decimal TotalRecharge { get; set; }

        /// <summary>
        /// 历史赠送总额
        /// </summary>
        [Display(Name = "历史赠送总额")]
        public decimal TotalGive { get; set; }

        /// <summary>
        /// 历史消费总额
        /// </summary>
        [Display(Name = "历史消费总额")]
        public decimal TotalConsume { get; set; }

        /// <summary>
        /// 是否虚拟卡
        /// </summary>
        [Display(Name = "是否虚拟卡")]
        public bool IsVirtual { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Display(Name = "备注")]
        public string Remark { get; set; }

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
        /// 最后修改人
        /// </summary>
        [Display(Name = "最后修改人")]
        public string LastUpdateUser { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        [Display(Name = "最后修改时间")]
        public DateTime? LastUpdateDate { get; set; }

        /// <summary>
        /// 会员分组
        /// </summary>
        //[Display(Name = "会员分组")]
        //public Guid? MemberGroupID { get; set; }

        /// <summary>
        /// 会员等级
        /// </summary>
        //[Display(Name = "会员等级")]
        //public Guid? MemberGradeID { get; set; }
    }
}
