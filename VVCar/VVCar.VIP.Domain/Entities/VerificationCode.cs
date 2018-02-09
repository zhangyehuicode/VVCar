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
    /// 核销码管理
    /// </summary>
    public class VerificationCode : NormalEntityBase
    {
        /// <summary>
        ///  核销码
        /// </summary>
        [Display(Name = "核销码")]
        public string Code { get; set; }

        /// <summary>
        ///  店铺编号
        /// </summary>
        [Display(Name = "店铺编号")]
        public string DepartmentCode { get; set; }

        /// <summary>
        ///  店铺名称
        /// </summary>
        [Display(Name = "店铺名称")]
        public string DepartmentName { get; set; }

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
    }
}
