using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace VVCar.BaseData.Domain.Enums
{
    /// <summary>
    /// 业务类型
    /// </summary>
    public enum EBusinessType
    {
        /// <summary>
        /// 普通消费
        /// </summary>
        [Description("普通交易")]
        Default = 0,

        /// <summary>
        /// 调整余额
        /// </summary>
        [Description("调整金额")]
        AdjustBalance = 1
    }
}
