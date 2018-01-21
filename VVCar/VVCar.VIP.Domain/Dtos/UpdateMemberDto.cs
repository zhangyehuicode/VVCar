using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using YEF.Core.Enums;

namespace VVCar.VIP.Domain.Dtos
{
    /// <summary>
    /// 更新会员信息Dto
    /// </summary>
    public class UpdateMemberDto
    {
        /// <summary>
        /// 会员ID
        /// </summary>
        [Required]
        public Guid MemberID { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public ESex Sex { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public DateTime? Birthday { get; set; }
    }
}
