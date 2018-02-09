using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVCar.VIP.Domain.Dtos
{
    /// <summary>
    /// 储值方案DTO
    /// </summary>
    public class RechargePlanDto
    {
        /// <summary>
        /// 储值方案ID
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// 方案编号
        /// </summary>
        public String Code { get; set; }

        /// <summary>
        /// 方案名称
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// 充值金额
        /// </summary>
        public Decimal RechargeAmount { get; set; }

        /// <summary>
        /// 赠送金额
        /// </summary>
        public Decimal GiveAmount { get; set; }
    }
}
