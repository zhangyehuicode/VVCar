using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.VIP.Domain.Enums;

namespace VVCar.VIP.Domain.Filters
{
    /// <summary>
    /// 储值纪录Filter
    /// </summary>
    public class RechargeHistoryFilter
    {
        /// <summary>
        /// 门店编号
        /// </summary>
        [Required]
        public string DepartmentCode { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? FinishDate { get; set; }

        /// <summary>
        /// 业务类型
        /// </summary>
        public EBusinessType? BusinessType { get; set; }
    }
}
