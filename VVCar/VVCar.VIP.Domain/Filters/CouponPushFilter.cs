using System.ComponentModel.DataAnnotations;
using VVCar.VIP.Domain.Enums;
using YEF.Core.Dtos;

namespace VVCar.VIP.Domain.Filters
{
    /// <summary>
    /// 卡券推送过滤条件
    /// </summary>
    public class CouponPushFilter : BasePageFilter
    {
        /// <summary>
        /// 标题
        /// </summary>
        [Display(Name = "标题")]
        public string Title { get; set; }

        /// <summary>
        /// 推送状态
        /// </summary>
        [Display(Name = "推送状态")]
        public ECouponPushStatus Status { get; set; }

        /// <summary>
        /// 是否显示全部数据
        /// </summary>
        [Display(Name = "是否显示全部数据")]
        public bool ShowAll { get; set; }
    }
}
