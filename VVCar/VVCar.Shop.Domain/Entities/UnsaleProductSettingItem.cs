using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEF.Core.Data;

namespace VVCar.Shop.Domain.Entities
{
    /// <summary>
    /// 滞销产品通知设置子项
    /// </summary>
    public class UnsaleProductSettingItem : EntityBase
    {
        /// <summary>
        /// 滞销产品通知设置ID
        /// </summary>
        [Display(Name = "滞销产品通知设置ID")]
        public Guid UnsaleProductSettingID { get; set; }

        /// <summary>
        /// 滞销产品设置
        /// </summary>
        public UnsaleProductSetting UnsaleProductSetting { get; set; }

        /// <summary>
        /// 产品ID
        /// </summary>
        [Display(Name = "产品ID")]
        public Guid ProductID { get; set; }

        /// <summary>
        /// 产品
        /// </summary>
        public virtual Product Product { get; set; }

        /// <summary>
        /// 创建人ID
        /// </summary>
        [Display(Name = "创建人ID")]
        public Guid? CreatedUserID { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [Display(Name = "创建人")]
        public string CreatedUser { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Display(Name = "创建时间")]
        public DateTime CreatedDate { get; set; }
    }
}
