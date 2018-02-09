using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVCar.VIP.Domain.Enums
{
    /// <summary>
    /// 客户服务
    /// </summary>
    [Flags]
    public enum EMerchantService
    {
        /// <summary>
        /// 免费Wifi
        /// </summary>
        [Description("免费Wifi")]
        FreeWifi = 1,

        /// <summary>
        /// 可带宠物
        /// </summary>
        [Description("可带宠物")]
        AllowPets = 2,

        /// <summary>
        /// 免费停车
        /// </summary>
        [Description("免费停车")]
        FreePark = 4,

        /// <summary>
        /// 外卖
        /// </summary>
        [Description("外卖")]
        TakeOut = 8,
    }
}
