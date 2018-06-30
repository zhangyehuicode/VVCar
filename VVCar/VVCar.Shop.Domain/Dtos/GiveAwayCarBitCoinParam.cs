using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVCar.Shop.Domain.Dtos
{
    /// <summary>
    /// 车比特赠送参数
    /// </summary>
    public class GiveAwayCarBitCoinParam
    {
        /// <summary>
        /// 车比特会员ID
        /// </summary>
        public Guid CarBitCoinMemberID { get; set; }

        /// <summary>
        /// 赠送的车比特
        /// </summary>
        public decimal CarBitCoin { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
