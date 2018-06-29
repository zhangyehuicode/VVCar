using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.Shop.Domain.Enums;
using YEF.Core.Dtos;

namespace VVCar.Shop.Domain.Filters
{
    /// <summary>
    /// 车比特分配过滤器
    /// </summary>
    public class CarBitCoinDistributionFilter : BasePageFilter
    {
        /// <summary>
        /// 车比特会员ID
        /// </summary>
        public Guid? CarBitCoinMemberID { get; set; }

        /// <summary>
        /// 电话号码
        /// </summary>
        public string MobilePhoneNo { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public ECarBitCoinDistributionStatus? Status { get; set; }
    }
}
