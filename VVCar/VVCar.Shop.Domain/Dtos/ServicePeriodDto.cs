using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.Shop.Domain.Entities;

namespace VVCar.Shop.Domain.Dtos
{
    /// <summary>
    /// 服务周期设置Dto
    /// </summary>
    public class ServicePeriodSettingDto
    {
        /// <summary>
        /// 服务周期设置ID
        /// </summary>
        [Display(Name = "服务周期设置ID")]
        public Guid ID { get; set; }

        /// <summary>
        /// 产品服务ID
        /// </summary>
        [Display(Name = "产品服务ID")]
        public Guid ProductID { get; set; }

        /// <summary>
        /// 产品编码
        /// </summary>
        [Display(Name = "产品编码")]
        public string ProductCode { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        [Display(Name = "产品名称")]
        public string ProductName { get; set; }

        /// <summary>
        /// 服务周期（天）
        /// </summary>
        [Display(Name = "服务周期（天）")]
        public int PeriodDays { get; set; }

        /// <summary>
        /// 到期提示语
        /// </summary>
        [Display(Name = "到期提示语")]
        public string ExpirationNotice { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        [Display(Name = "是否启用")]
        public bool IsAvailable { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Display(Name = "创建时间")]
        public DateTime CreatedDate { get; set; }
    }
}
