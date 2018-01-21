using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVCar.VIP.Domain.Dtos
{
    /// <summary>
    /// 会员基本信息
    /// </summary>
    public class MemberBaseInfoDto
    {
        /// <summary>
        /// 会员卡状态
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 生效期
        /// </summary>
        public string EffectiveDate { get; set; }

        /// <summary>
        /// 截止期
        /// </summary>
        public string ExpiredDate { get; set; }

        /// <summary>
        /// 总充值金额
        /// </summary>
        public string TotalRecharge { get; set; }

        /// <summary>
        /// 总消费金额
        /// </summary>
        public string TotalConsume { get; set; }

        /// <summary>
        /// 余额
        /// </summary>
        public string CardBalance { get; set; }

        /// <summary>
        /// 最后充值金额
        /// </summary>
        public string LastRechargeMoney { get; set; }
    }
}
