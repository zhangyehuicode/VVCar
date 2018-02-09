using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEF.Core.Data;

namespace VVCar.VIP.Domain.Entities
{
    /// <summary>
    /// 会员等级变更记录
    /// </summary>
    public class MemberGradeHistory : NormalEntityBase
    {
        /// <summary>
        /// 会员ID
        /// </summary>
        [Display(Name = "会员ID")]
        public Guid MemberID { get; set; }

        /// <summary>
        /// 会员
        /// </summary>
        public virtual Member Member { get; set; }

        /// <summary>
        /// 变更前等级
        /// </summary>
        [Display(Name = "变更前等级")]
        public Guid BeforeMemberGradeID { get; set; }

        /// <summary>
        /// 变更后等级
        /// </summary>
        [Display(Name = "变更后等级")]
        public Guid AfterMemberGradeID { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        [Display(Name = "创建日期")]
        public DateTime CreatedDate { get; set; }
    }
}
