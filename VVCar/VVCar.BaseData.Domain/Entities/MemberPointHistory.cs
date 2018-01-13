using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEF.Core.Data;
using System.ComponentModel.DataAnnotations;
using VVCar.BaseData.Domain.Enums;

namespace VVCar.BaseData.Domain.Entities
{
    /// <summary>
    /// 会员积分历史
    /// </summary>
    public class MemberPointHistory : EntityBase
    {
        /// <summary>
        /// 会员ID
        /// </summary>
        [Display(Name = "会员ID")]
        public Guid MemberID { get; set; }

        /// <summary>
        /// 更改的积分
        /// </summary>
        [Display(Name = "更改的积分")]
        public int Point { get; set; }

        /// <summary>
        /// 会员积分类型
        /// </summary>
        [Display(Name = "会员积分类型")]
        public EMemberPointType Source { get; set; }

        /// <summary>
        /// 外部交易流水号
        /// </summary>
        [Display(Name = "外部交易流水号")]
        public string OutTradeNo { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Display(Name = "备注")]
        public string Remark { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Display(Name = "创建时间")]
        public DateTime CreatedDate { get; set; }
    }
}
