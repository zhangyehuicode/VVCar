using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVCar.Shop.Domain.Enums
{
    /// <summary>
    /// 畅销/滞销产品状态
    /// </summary>
    public enum EUnsaleProductStatus
    {
        /// <summary>
        /// 一般
        /// </summary>
        [Description("一般")]
        Normal = 0,

        /// <summary>
        /// 滞销
        /// </summary>
        [Description("滞销")]
        Unsale = 1,

        /// <summary>
        /// 畅销
        /// </summary>
        [Description("畅销")]
        SaleWell = 2,
    }
}
