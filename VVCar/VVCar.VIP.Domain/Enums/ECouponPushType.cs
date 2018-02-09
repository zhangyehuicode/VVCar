using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVCar.VIP.Domain.Enums
{
    /// <summary>
    /// 卡券推送类型
    /// </summary>
    public enum ECouponPushType
    {
        /// <summary>
        /// 每月1号推送给当月生日成员
        /// </summary>
        [Description("每月1号推送给当月生日成员")]
        FirstDayOfMonth = 0,

        /// <summary>
        /// 固定日期
        /// </summary>
        [Description("IsFixedDate")]
        FixedDate = 1,

        /// <summary>
        /// 每月某天推送给下个月生日成员
        /// </summary>
        [Description("每月某天推送给下个月生日成员")]
        MonthlyDays = 2,

        /// <summary>
        /// 会员生日前几天
        /// </summary>
        [Description("会员生日前几天")]
        BeforeBirthday = 3,
    }
}
