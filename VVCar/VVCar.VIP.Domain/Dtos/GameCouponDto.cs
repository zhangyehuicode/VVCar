using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.VIP.Domain.Enums;

namespace VVCar.VIP.Domain.Dtos
{
    /// <summary>
    /// 游戏卡券配置Dto
    /// </summary>
    public class GameCouponDto
    {
        /// <summary>
        /// 卡券配置ID
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// 性质（卡/券）
        /// </summary>
        [Display(Name = "性质")]
        public ENature Nature { get; set; }

        /// <summary>
        ///  类型
        /// </summary>
        [Display(Name = "类型")]
        public ECouponType CouponType { get; set; }

        /// <summary>
        /// 卡券模板ID
        /// </summary>
        [Display(Name = "卡券模板ID")]
        public Guid CouponTemplateID { get; set; }

        /// <summary>
        /// 优惠券模板编号
        /// </summary>
        [Display(Name = "优惠券模板编号")]
        public string TemplateCode { get; set; }

        /// <summary>
        /// 卡券标题
        /// </summary>
        [Display(Name = "卡券标题")]
        public string Title { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Display(Name = "创建时间")]
        public DateTime CreatedDate { get; set; }
    }
}
