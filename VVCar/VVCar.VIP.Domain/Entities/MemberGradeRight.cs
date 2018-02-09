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
    /// 会员等级权益
    /// </summary>
    public class MemberGradeRight : NormalEntityBase
    {
        /// <summary>
        /// 会员等级ID
        /// </summary>
        [Display(Name = "会员等级ID")]
        public Guid MemberGradeID { get; set; }

        /// <summary>
        /// 关联会员等级
        /// </summary>
        [Display(Name = "关联会员等级")]
        public virtual MemberGrade MemberGrade { get; set; }

        /// <summary>
        /// 等级权益类型
        /// </summary>
        [Display(Name = "权益类型")]
        public EGradeRightType RightType { get; set; }

        /// <summary>
        /// 权益ID
        /// </summary>
        [Display(Name = "权益ID")]
        public Guid PosRightID { get; set; }

        /// <summary>
        /// 权益编号
        /// </summary>
        [Display(Name = "权益编号")]
        public string PosRightCode { get; set; }

        /// <summary>
        /// 权益名称
        /// </summary>
        [Display(Name = "权益名称")]
        public string PosRightName { get; set; }

        /// <summary>
        /// 折扣系数
        /// </summary>
        [Display(Name = "折扣系数")]
        public decimal PosRightDiscount { get; set; }
    }
}
