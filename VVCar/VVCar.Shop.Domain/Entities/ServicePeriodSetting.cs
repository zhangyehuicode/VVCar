using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEF.Core.Data;
using System.ComponentModel.DataAnnotations;

namespace VVCar.Shop.Domain.Entities
{
    /// <summary>
    /// 服务周期设置
    /// </summary>
    public class ServicePeriodSetting : EntityBase
    {
        public ServicePeriodSetting()
        {
            ServicePeriodCouponList = new List<ServicePeriodCoupon>();
        }

        /// <summary>
        /// 产品服务ID
        /// </summary>
        [Display(Name = "产品服务ID")]
        public Guid ProductID { get; set; }

        /// <summary>
        /// 产品服务
        /// </summary>
        [Display(Name = "产品服务")]
        public Product Product { get; set; }

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
        /// 创建日期
        /// </summary>
        [Display(Name = "创建日期")]
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// 创建人ID
        /// </summary>
        [Display(Name = "创建人ID")]
        public Guid CreatedUserID { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [Display(Name = "创建人")]
        public string CreatedUser { get; set; }

        /// <summary>
        /// 服务周期 到期推送券
        /// </summary>
        public ICollection<ServicePeriodCoupon> ServicePeriodCouponList { get; set; }
    }
}
