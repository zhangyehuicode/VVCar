using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEF.Core.Data;
using System.ComponentModel.DataAnnotations;
using VVCar.VIP.Domain.Enums;
using VVCar.BaseData.Domain.Entities;

namespace VVCar.VIP.Domain.Entities
{
    /// <summary>
    /// 会员历史记录
    /// </summary>
    public abstract class MemberHistoryEntity : NormalEntityBase
    {
        /// <summary>
        /// 交易流水号
        /// </summary>
        [Display(Name = "交易流水号")]
        public string TradeNo { get; set; }

        /// <summary>
        /// 外部交易流水号
        /// </summary>
        [Display(Name = "外部交易流水号")]
        public string OutTradeNo { get; set; }

        /// <summary>
        /// 会员卡ID
        /// </summary>
        [Display(Name = "会员卡ID")]
        public Guid CardID { get; set; }

        /// <summary>
        /// 会员卡
        /// </summary>
        [Display(Name = "会员卡")]
        public virtual MemberCard Card { get; set; }

        /// <summary>
        /// 会员ID
        /// </summary>
        [Display(Name = "会员ID")]
        public Guid? MemberID { get; set; }

        /// <summary>
        /// 会员信息
        /// </summary>
        [Display(Name = "会员信息")]
        public virtual Member Member { get; set; }

        /// <summary>
        /// 会员卡号
        /// </summary>
        [Display(Name = "会员卡号")]
        public string CardNumber { get; set; }

        /// <summary>
        /// 会员卡余额，消费后卡内余额
        /// </summary>
        [Display(Name = "会员卡余额")]
        public decimal CardBalance { get; set; }

        /// <summary>
        /// 交易金额
        /// </summary>
        [Display(Name = "交易金额")]
        public decimal TradeAmount { get; set; }

        /// <summary>
        /// 交易来源
        /// </summary>
        [Display(Name = "交易来源")]
        public ETradeSource TradeSource { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Display(Name = "备注")]
        public string Remark { get; set; }

        /// <summary>
        /// 交易门店ID
        /// </summary>
        [Display(Name = "交易门店ID")]
        public Guid TradeDepartmentID { get; set; }

        /// <summary>
        /// 交易门店
        /// </summary>
        [Display(Name = "交易门店")]
        public virtual Department TradeDepartment { get; set; }

        /// <summary>
        /// 业务类型
        /// </summary>
        [Display(Name = "业务类型")]
        public EBusinessType BusinessType { get; set; }

        /// <summary>
        /// 创建人ID
        /// </summary>
        [Display(Name = "创建人ID")]
        public Guid CreatedUserID { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [Display(Name = "创建人")]
        public String CreatedUser { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Display(Name = "创建时间")]
        public DateTime CreatedDate { get; set; }

    }
}
