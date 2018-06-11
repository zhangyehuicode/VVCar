using System.ComponentModel;

namespace YEF.Core.Enums
{
    /// <summary>
    /// 商户状态
    /// </summary>
    public enum EMerchantStatus
    {
        /// <summary>
        /// 未激活
        /// </summary>
        [Description("未激活")]
        UnActivate = 0,

        /// <summary>
        /// 已激活
        /// </summary>
        [Description("已激活")]
        Activated = 1,

        /// <summary>
        /// 冻结
        /// </summary>
        [Description("冻结")]
        Freeze = -1,
    }
}
