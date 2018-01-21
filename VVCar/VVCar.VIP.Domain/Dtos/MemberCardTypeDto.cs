using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVCar.VIP.Domain.Dtos
{
    /// <summary>
    /// 卡片类型DTO
    /// </summary>
    public class MemberCardTypeDto
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// 卡片名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 允许门店激活
        /// </summary>
        public bool AllowStoreActivate { get; set; }

        /// <summary>
        /// 允许储值
        /// </summary>
        public bool AllowRecharge { get; set; }
    }
}
