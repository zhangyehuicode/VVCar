
namespace YEF.Core.Filter
{
    /// <summary>
    /// 过滤方式
    /// </summary>
    public enum FilterOperate
    {
        /// <summary>
        /// 等于
        /// </summary>
        [OperateCode("equal")]
        Equal = 1,

        /// <summary>
        /// 不等于
        /// </summary>
        [OperateCode("notequal")]
        NotEqual = 2,

        /// <summary>
        /// 小于
        /// </summary>
        [OperateCode("less")]
        LessThan = 3,

        /// <summary>
        /// 小于或等于
        /// </summary>
        [OperateCode("lessorequal")]
        LessThanOrEqual = 4,

        /// <summary>
        /// 大于
        /// </summary>
        [OperateCode("greater")]
        GreaterThan = 5,

        /// <summary>
        /// 大于或等于
        /// </summary>
        [OperateCode("greaterorequal")]
        GreaterThanOrEqual = 6,

        /// <summary>
        /// 以……开始，只适用于String
        /// </summary>
        [OperateCode("startwith")]
        StartsWith = 7,

        /// <summary>
        /// 以……结束，只适用于String
        /// </summary>
        [OperateCode("endwith")]
        EndsWith = 8,

        /// <summary>
        /// 包含（相似），只适用于String
        /// </summary>
        [OperateCode("contains")]
        Contains = 9,
    }
}
