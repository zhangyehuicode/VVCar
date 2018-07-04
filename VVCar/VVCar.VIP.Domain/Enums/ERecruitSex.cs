using System.ComponentModel;

namespace VVCar.VIP.Domain.Enums
{
    /// <summary>
    /// 招聘性别要求
    /// </summary>
    public enum ERecruitSex
    {
        /// <summary>
        /// 不限
        /// </summary>
        [Description("不限")]
        NoLimmit = 0,

        /// <summary>
        /// 男
        /// </summary>
        [Description("男")]
        Male = 1,

        /// <summary>
        /// 女
        /// </summary>
        [Description("女")]
        Female = 2,
    }
}
