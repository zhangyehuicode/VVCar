using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.VIP.Domain.Enums;

namespace VVCar.VIP.Domain.Dtos
{
    /// <summary>
    /// 调整余额操作数据对象
    /// </summary>
    public class AdjustBalanceDto
    {
        /// <summary>
        /// 卡号
        /// </summary>
        public string CardNumber { get; set; }

        /// <summary>
        /// 调整方式
        /// </summary>
        public EAdjustType AdjustType { get; set; }

        /// <summary>
        /// 调整余额
        /// </summary>
        public decimal AdjustBalance { get; set; }

        /// <summary>
        /// 调整说明
        /// </summary>
        public string AdjustMark { get; set; }
    }
}
