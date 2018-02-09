using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVCar.VIP.Domain.Dtos
{
    /// <summary>
    /// 储值统计信息DTO
    /// </summary>
    public class RechargeTotalDataDto
    {
        /// <summary>
        /// 门店编号
        /// </summary>
        public string DepartmentCode { get; set; }

        /// <summary>
        /// 现金
        /// </summary>
        public decimal Cash { get; set; }

        /// <summary>
        /// 银行卡
        /// </summary>
        public decimal BankCard { get; set; }

        /// <summary>
        /// 微信
        /// </summary>
        public decimal WeChat { get; set; }

        /// <summary>
        /// 支付宝
        /// </summary>
        public decimal Alipay { get; set; }
    }
}
