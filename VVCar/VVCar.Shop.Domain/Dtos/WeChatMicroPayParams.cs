using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVCar.Shop.Domain.Dtos
{
    /// <summary>
    /// 微信刷卡支付参数（付款码支付）
    /// </summary>
    public class WeChatMicroPayParams
    {
        /// <summary>
        /// 授权码
        /// </summary>
        public string auth_code { get; set; }

        /// <summary>
        /// 订单总额
        /// </summary>
        public double total_fee { get; set; }

        /// <summary>
        /// 商品描述
        /// </summary>
        public string body { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        public string out_trade_no { get; set; }

        /// <summary>
        /// 设备信息（门店编码）
        /// </summary>
        public string device_info { get; set; }
    }
}
