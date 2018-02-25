using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.Shop.Domain.Enums;

namespace VVCar.Shop.Domain.Dtos
{
    /// <summary>
    /// 调整索引参数
    /// </summary>
    public class AdjustIndexParam
    {
        /// <summary>
        /// ID
        /// </summary>
        [Display(Name = "ID")]
        public Guid ID { get; set; }

        /// <summary>
        /// 调整方向
        /// </summary>
        [Display(Name = "调整方向")]
        public EAdjustDirection Direction { get; set; }
    }
}
