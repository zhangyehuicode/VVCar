using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVCar.VIP.Domain.Dtos
{
    /// <summary>
    /// 礼品卡接收Dto
    /// </summary>
    public class GiftCardReceiveDto
    {
        /// <summary>
        /// 礼品卡拥有者OpenID
        /// </summary>
        public string OwnerOpenID { get; set; }

        /// <summary>
        /// 礼品卡接收者OpenID
        /// </summary>
        public string ReceiverOpenID { get; set; }

        /// <summary>
        /// 礼品卡编号
        /// </summary>
        public List<string> GiftCardCodes { get; set; }
    }
}
