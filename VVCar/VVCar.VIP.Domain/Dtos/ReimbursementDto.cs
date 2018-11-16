using System;
using System.ComponentModel.DataAnnotations;
using VVCar.VIP.Domain.Enums;

namespace VVCar.VIP.Domain.Dtos
{
    /// <summary>
    /// 业务报销Dto
    /// </summary>
    public class ReimbursementDto
    {
        /// <summary>
        /// 编号
        /// </summary>
        [Display(Name = "编号")]
        public string Code { get; set; }

        /// <summary>
        /// 业务报销ID
        /// </summary>
        [Display(Name = "业务报销ID")]
        public Guid ID { get; set; }

        /// <summary>
        /// 报销人姓名
        /// </summary>
        [Display(Name = "报销人姓名")]
        public string UserName { get; set; }

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
        /// 创建时间
        /// </summary>
        [Display(Name = "创建时间")]
        public DateTime CreatedDate { get; set; }
    }
}
