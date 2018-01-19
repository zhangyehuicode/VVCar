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
    /// 卡片类型
    /// </summary>
    public class MemberCardType : NormalEntityBase
    {
        /// <summary>
        /// 卡片名称
        /// </summary>
        [Display(Name = "卡片名称")]
        public string Name { get; set; }

        /// <summary>
        /// 允许门店激活
        /// </summary>
        [Display(Name = "允许门店激活")]
        public bool AllowStoreActivate { get; set; }

        /// <summary>
        /// 允许折扣
        /// </summary>
        [Display(Name = "允许折扣")]
        public bool AllowDiscount { get; set; }

        /// <summary>
        /// 允许储值
        /// </summary>
        [Display(Name = "允许储值")]
        public bool AllowRecharge { get; set; }

        /// <summary>
        /// 储值上限金额
        /// </summary>
        [Display(Name = "储值上限金额")]
        public decimal? MaxRecharge { get; set; }

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
        /// 最后修改人
        /// </summary>
        [Display(Name = "最后修改人")]
        public string LastUpdateUser { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        [Display(Name = "最后修改时间")]
        public DateTime? LastUpdateDate { get; set; }
    }
}
