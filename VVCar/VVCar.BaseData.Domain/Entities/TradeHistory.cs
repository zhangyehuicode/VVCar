using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.BaseData.Domain.Enums;
using YEF.Core.Data;

namespace VVCar.BaseData.Domain.Entities
{
    /// <summary>
    /// 交易记录
    /// </summary>
    public class TradeHistory : MemberHistoryEntity
    {
        /// <summary>
        /// 消费类型
        /// </summary>
        [Display(Name = "消费类型")]
        public EConsumeType ConsumeType { get; set; }

        /// <summary>
        /// 使用会员余额支付金额
        /// </summary>
        [Display(Name = "使用会员余额支付金额")]
        public decimal UseBalanceAmount { get; set; }

        /// <summary>
        /// 会员等级ID
        /// </summary>
        //public Guid? MemberGradeID { get; set; }
    }
}
