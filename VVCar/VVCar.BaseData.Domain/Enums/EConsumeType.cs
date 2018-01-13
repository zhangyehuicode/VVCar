using System.ComponentModel;

namespace VVCar.BaseData.Domain.Enums
{
    /// <summary>
    /// 消费类型
    /// </summary>
    public enum EConsumeType
    {
        /// <summary>
        /// 扣减余额
        /// </summary>
        [Description("扣减余额")]
        CardBalance = 0,

        /// <summary>
        /// 打折
        /// </summary>
        [Description("打折")]
        Discount = 1
    }
}
