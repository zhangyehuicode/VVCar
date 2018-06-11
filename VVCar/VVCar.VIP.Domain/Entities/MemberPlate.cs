using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEF.Core.Data;
using System.ComponentModel.DataAnnotations;

namespace VVCar.VIP.Domain.Entities
{
    /// <summary>
    /// 会员车牌号
    /// </summary>
    public class MemberPlate : EntityBase
    {
        /// <summary>
        /// 会员ID
        /// </summary>
        [Display(Name = "会员ID")]
        public Guid MemberID { get; set; }

        /// <summary>
        /// OpenID
        /// </summary>
        [Display(Name = "OpenID")]
        public string OpenID { get; set; }

        /// <summary>
        /// 会员
        /// </summary>
        public virtual Member Member { get; set; }

        /// <summary>
        /// 车牌号
        /// </summary>
        [Display(Name = "车牌号")]
        public string PlateNumber { get; set; }
    }
}
