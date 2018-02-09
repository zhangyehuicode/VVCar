using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.VIP.Domain.Enums;

namespace VVCar.VIP.Domain.Dtos
{
    /// <summary>
    /// pos购买礼品卡回调参数
    /// </summary>
    public class BuyGiftCardByPosDto
    {
        /// <summary>
        /// 卡号
        /// </summary>
        public List<string> Numbers { get; set; }

        /// <summary>
        /// 销售描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 外部交易流水号
        /// </summary>
        public string OutTradeNo { get; set; }

        /// <summary>
        /// 支付方式
        /// </summary>
        public EPaymentType PaymentType { get; set; }

        /// <summary>
        /// 门店ID
        /// </summary>
        public string DepartmentCode { get; set; }
    }
}
