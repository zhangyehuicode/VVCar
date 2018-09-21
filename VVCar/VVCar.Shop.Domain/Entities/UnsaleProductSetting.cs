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
    /// 滞销产品通知设置
    /// </summary>
    public class UnsaleProductSetting : EntityBase
    {
        /// <summary>
        /// 配置编码
        /// </summary>
        [Display(Name = "配置编码")]
        public string Code { get; set; }

        /// <summary>
        /// 配置名称
        /// </summary>
        [Display(Name = "配置名称")]
        public string Name { get; set; }

        /// <summary>
        /// 滞销产品提醒周期参数(天)
        /// </summary>
        [Display(Name = "滞销产品提醒周期参数(天)")]
        public int PeriodDays { get; set; }

        /// <summary>
        /// 滞销产品提醒数量参数
        /// </summary>
        [Display(Name = "滞销产品提醒数量参数")]
        public int Quantities { get; set; }

        /// <summary>
        /// 滞销产品提醒营业额参数
        /// </summary>
        [Display(Name = "滞销产品提醒营业额参数")]
        public decimal Performence { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        [Display(Name = "是否启用")]
        public bool IsAvailable { get; set; }

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
