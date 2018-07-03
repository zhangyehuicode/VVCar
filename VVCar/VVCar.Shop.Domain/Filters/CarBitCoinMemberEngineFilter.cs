using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEF.Core.Dtos;

namespace VVCar.Shop.Domain.Filters
{
    /// <summary>
    /// 车比特会员引擎过滤器
    /// </summary>
    public class CarBitCoinMemberEngineFilter : BasePageFilter
    {
        /// <summary>
        /// 车比特会员ID
        /// </summary>
        public Guid? CarBitCoinMemberID { get; set; }
    }
}
