using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace VVCar.Shop.Domain.Enums
{
    /// <summary>
    /// 接车单任务分配人员类型
    /// </summary>
    public enum ETaskDistributionPeopleType
    {
        /// <summary>
        /// 施工员
        /// </summary>
        [Description("施工员")]
        ConstructionCrew = 0,

        /// <summary>
        /// 业务员
        /// </summary>
        [Description("业务员")]
        Salesman = 1,
    }
}
