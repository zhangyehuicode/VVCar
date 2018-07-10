using System;
using System.ComponentModel.DataAnnotations;
using VVCar.BaseData.Domain.Entities;
using VVCar.VIP.Domain.Enums;
using YEF.Core.Data;

namespace VVCar.VIP.Domain.Entities
{
    /// <summary>
    /// 业务报销
    /// </summary>
    public class Reimbursement : EntityBase
    {
        /// <summary>
        /// 报销人ID
        /// </summary>
        [Display(Name = "报销人ID")]
        public Guid? UserID { get; set; }

        /// <summary>
        /// 项目
        /// </summary>
        [Display(Name = "项目")]
        public string Project { get; set; }

        /// <summary>
        /// 发票图片路径
        /// </summary>
        [Display(Name = "发票图片路径")]
        public string ImgUrl { get; set; }

        /// <summary>
        /// 报销金额
        /// </summary>
        [Display(Name = "报销金额")]
        public decimal Money { get; set; }

        /// <summary>
        /// 业务报销审核状态
        /// </summary>
        [Display(Name = "业务报销审核状态")]
        public EReimbursementApproveStatus Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Display(Name = "备注")]
        public string Remark { get; set; }

        /// <summary>
        /// 创建人ID
        /// </summary>
        [Display(Name = "创建人ID")]
        public Guid? CreatedUserID { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [Display(Name = "创建人")]
        public string CreatedUser { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Display(Name = "创建时间")]
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
        /// 会员来源
        /// </summary>
        [Display(Name = "会员来源")]
        public EMemberSource MemberSource { get; set; }

        /// <summary>
        /// 报销人
        /// </summary>
        public virtual User User { get; set; }
    }
}
