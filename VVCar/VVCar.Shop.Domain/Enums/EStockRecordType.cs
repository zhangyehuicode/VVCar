using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace VVCar.Shop.Domain.Enums
{
    /// <summary>
    /// 库存记录类型
    /// </summary>
    public enum EStockRecordType
    {
        /// <summary>
        /// 出库
        /// </summary>
        [Description("出库")]
        Out = 0,

        /// <summary>
        /// 入库
        /// </summary>
        [Description("入库")]
        In = 1,
    }
}
