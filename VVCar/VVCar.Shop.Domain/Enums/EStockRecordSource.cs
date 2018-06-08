using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace VVCar.Shop.Domain.Enums
{
    /// <summary>
    /// 库存记录来源
    /// </summary>
    public enum EStockRecordSource
    {
        /// <summary>
        /// 微信
        /// </summary>
        [Description("微信")]
        WeChat = 0,

        /// <summary>
        /// 后台
        /// </summary>
        [Description("后台")]
        Background = 1,
    }
}
