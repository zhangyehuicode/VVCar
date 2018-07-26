using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEF.Core.Data;
using System.ComponentModel.DataAnnotations;
using VVCar.Shop.Domain.Enums;

namespace VVCar.Shop.Domain.Entities
{
    /// <summary>
    /// 物流
    /// </summary>
    public class Logistics : EntityBase
    {
        /// <summary>
        /// 商城订单ID
        /// </summary>
        [Display(Name = "商城订单ID")]
        public Guid OrderID { get; set; }

        /// <summary>
        /// 商城订单
        /// </summary>
        public virtual Order Order { get; set; }

        /// <summary>
        /// 回访时间
        /// </summary>
        [Display(Name = "回访时间")]
        public DateTime? RevisitDate { get; set; }

        /// <summary>
        /// 回访提示
        /// </summary>
        [Display(Name = "回访提示")]
        public string RevisitTips { get; set; }

        /// <summary>
        /// 回访状态
        /// </summary>
        [Display(Name = "回访状态")]
        public ERevisitStatus RevisitStatus { get; set; }

        /// <summary>
        /// 业务员ID
        /// </summary>
        [Display(Name = "业务员ID")]
        public Guid? UserID { get; set; }

        /// <summary>
        /// 发货提醒（业务员）
        /// </summary>
        [Display(Name = "发货提醒（业务员）")]
        public string DeliveryTips { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Display(Name = "创建时间")]
        public DateTime CreatedDate { get; set; }
    }
}
