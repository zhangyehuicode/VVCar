using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.Shop.Domain.Enums;
using YEF.Core.Dtos;

namespace VVCar.Shop.Domain.Filters
{
    public class LogisticsFilter : BasePageFilter
    {
        /// <summary>
        /// 商城订单号
        /// </summary>
        public string OrderCode { get; set; }

        /// <summary>
        /// 回访状态
        /// </summary>
        public ERevisitStatus? RevisitStatus { get; set; }

        /// <summary>
        /// 业务员ID
        /// </summary>
        public Guid? UserID { get; set; }
    }
}
