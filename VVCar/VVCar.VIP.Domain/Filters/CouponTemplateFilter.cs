using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.VIP.Domain.Enums;
using YEF.Core.Dtos;

namespace VVCar.VIP.Domain.Filters
{
    /// <summary>
    /// 卡券报表过滤条件
    /// </summary>
    public class CouponTemplateFilter : BasePageFilter
    {
        /// <summary>
        ///  类型
        /// </summary>
        [Display(Name = "类型")]
        public int CouponType { get; set; }

        /// <summary>
        /// 审核状态
        /// </summary>
        public int AproveStatus { get; set; }

        /// <summary>
        ///  标题
        /// </summary>
        [Display(Name = "标题")]
        public string Title { get; set; }

        /// <summary>
        /// 优惠券模板编码或标题
        /// </summary>
        [Display(Name = "优惠券模板编码或标题")]
        public string TemplateCodeOrTitle { get; set; }

        /// <summary>
        /// 是否不显示已经过了投放日期的券
        /// </summary>
        public bool HiddenExpirePutInDate { get; set; }

        /// <summary>
        /// 是否为非特殊券
        /// </summary>
        public bool IsNotSpecialCoupon { get; set; }

        /// <summary>
        /// 性质
        /// </summary>
        public int Nature { get; set; }

        /// <summary>
        /// 是否是股东卡
        /// </summary>
        public bool? IsStockholderCard { get; set; }

        /// <summary>
        /// 是否在小程序中显示
        /// </summary>
        public bool? IsPutApplet { get; set; }
    }
}
