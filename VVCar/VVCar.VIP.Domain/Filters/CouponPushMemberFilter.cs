using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEF.Core.Dtos;

namespace VVCar.VIP.Domain.Filters
{
    /// <summary>
    /// 推送会员过滤条件
    /// </summary>
    public class CouponPushMemberFilter : BasePageFilter
    {
        /// <summary>
        /// 卡券推送ID
        /// </summary>
        [Display(Name = "卡券推送ID")]
        public Guid CouponPushID { get; set; }
    }
}
