using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.VIP.Domain.Enums;
using YEF.Core.Data;
using YEF.Core.Enums;

namespace VVCar.VIP.Domain.Entities
{
    /// <summary>
    /// 人才招聘
    /// </summary>
    public class Recruitment : EntityBase
    {
        /// <summary>
        /// 招聘单位
        /// </summary>
        [Display(Name = "招聘单位")]
        public string Recruiter { get; set; }

        /// <summary>
        /// 招聘岗位
        /// </summary>
        [Display(Name = "招聘岗位")]
        public string Position { get; set; }

        /// <summary>
        /// 招聘开始时间
        /// </summary>
        [Display(Name = "招聘开始时间")]
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 招聘结束时间
        /// </summary>
        [Display(Name = "招聘结束时间")]
        public DateTime EndDate { get; set; }

        /// <summary>
        /// 联系人姓名
        /// </summary>
        [Display(Name = "联系人姓名")]
        public string HRName { get; set; }

        /// <summary>
        /// HR联系人电话
        /// </summary>
        [Display(Name = "HR联系人电话")]
        public string HRPhoneNo { get; set; }

        /// <summary>
        /// 学历要求
        /// </summary>
        [Display(Name = "学历要求")]
        public EDegreeType DegreeType { get; set; }

        /// <summary>
        /// 性别要求
        /// </summary>
        [Display(Name = "性别要求")]
        public ESex Sex { get; set; }

        /// <summary>
        /// 工作地点
        /// </summary>
        [Display(Name = "工作地点")]
        public string Address { get; set; }

        /// <summary>
        /// 上班时间
        /// </summary>
        [Display(Name = "上班时间")]
        public string WorkTime { get; set; }

        /// <summary>
        /// 招聘人数
        /// </summary>
        [Display(Name = "招聘人数")]
        public int RecruitNumber { get; set; }

        /// <summary>
        /// 职位要求
        /// </summary>
        [Display(Name = "职位要求")]
        public string Requirement { get; set; }

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
        /// 最后修改人名称
        /// </summary>
        [Display(Name = "最后修改人名称")]
        public string LastUpdateUser { get; set; }

        /// <summary>
        /// 最后修改日期
        /// </summary>
        [Display(Name = "最后修改日期")]
        public DateTime? LastUpdateDate { get; set; }
    }
}
