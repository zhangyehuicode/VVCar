using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVCar.VIP.Domain.Dtos
{
    /// <summary>
    /// 官网礼品卡查询
    /// </summary>
    public class GiftCardDtoOfOW
    {
        /// <summary>
        /// 卡号
        /// </summary>
        public string CardNumber { get; set; }

        /// <summary>
        /// 余额
        /// </summary>
        public decimal CardBalance { get; set; }

        /// <summary>
        /// 截止日期
        /// </summary>
        public DateTime? ExpiredDate { get; set; }
    }
}
