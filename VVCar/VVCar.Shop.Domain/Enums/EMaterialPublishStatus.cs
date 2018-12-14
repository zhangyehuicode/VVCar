using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVCar.Shop.Domain.Enums
{
    /// <summary>
    /// 信息发布状态
    /// </summary>
    public enum EMaterialPublishStatus
    {
        /// <summary>
        /// 未发布
        /// </summary>
        [Description("未发布")]
        NotPublish = 0,

        /// <summary>
        /// 已发布
        /// </summary>
        [Description("已发布")]
        Published = 1,

        /// <summary>
        /// 当前发布
        /// </summary>
        [Description("当前发布")]
        Publishing = 2,

        /// <summary>
        /// 取消发布
        /// </summary>
        [Description("取消发布")]
        CancelPublish = -1,
    }
}
