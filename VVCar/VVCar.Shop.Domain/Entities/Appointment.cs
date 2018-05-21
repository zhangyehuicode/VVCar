using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEF.Core.Data;
using System.ComponentModel.DataAnnotations;
using VVCar.Shop.Domain.Enums;

namespace VVCar.Shop.Domain.Entities
{
    /// <summary>
    /// 预约
    /// </summary>
    public class Appointment : EntityBase
    {
        /// <summary>
        /// 姓名
        /// </summary>
        [Display(Name = "姓名")]
        public string Name { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        [Display(Name = "联系电话")]
        public string MobilePhoneNo { get; set; }

        /// <summary>
        /// OpenID
        /// </summry>
        [Display(Name = "OpenID")]
        public string OpenID { get; set; }

        /// <summary>
        /// 会员ID
        /// </summary>
        [Display(Name = "会员ID")]
        public Guid? MemberID { get; set; }

        /// <summary>
        /// 服务ID
        /// </summary>
        [Display(Name = "服务ID")]
        public Guid ServiceID { get; set; }

        /// <summary>
        /// 服务名称
        /// </summary>
        [Display(Name = "服务名称")]
        public string ServiceName { get; set; }

        /// <summary>
        /// 预约日期
        /// </summary>
        [Display(Name = "预约日期")]
        public string AppointmentDate { get; set; }

        /// <summary>
        /// 预约时间
        /// </summary>
        [Display(Name = "预约时间")]
        public string AppointmentTime { get; set; }

        /// <summary>
        /// 预约时间
        /// </summary>
        [Display(Name = "预约时间")]
        public DateTime Date { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [Display(Name = "状态")]
        public EAppointmentStatus Status { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Display(Name = "创建时间")]
        public DateTime CreatedDate { get; set; }
    }
}
