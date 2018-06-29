using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace VVCar.Shop.Domain.Enums
{
    /// <summary>
    /// 车比特记录类型
    /// </summary>
    public enum ECarBitCoinRecordType
    {
        /// <summary>
        /// 赠送币
        /// </summary>
        [Description("赠送币")]
        Give = -4,

        /// <summary>
        /// 系统分配币
        /// </summary>
        [Description("系统分配币")]
        SystemDistribution = -3,

        /// <summary>
        /// 购买车比特
        /// </summary>
        [Description("购买比特币")]
        BuyBitCoin = -2,

        /// <summary>
        /// 出售车比特
        /// </summary>
        [Description("出售比特币")]
        SaleBitCoin = -1,

        /// <summary>
        /// 未知
        /// </summary>
        [Description("未知")]
        UnKnown = 0,

        /// <summary>
        /// 购买引擎增加马力
        /// </summary>
        [Description("购买引擎增加马力")]
        BuyEngine = 1,

        /// <summary>
        /// 商城下单增加马力
        /// </summary>
        [Description("商城下单增加马力")]
        Order = 2,

        /// <summary>
        /// 接车单消费增加马力
        /// </summary>
        [Description("接车单消费增加马力")]
        PickUpOrder = 3,

        /// <summary>
        /// 员工业绩增加马力
        /// </summary>
        [Description("员工业绩增加马力")]
        Performance = 4,
    }
}
