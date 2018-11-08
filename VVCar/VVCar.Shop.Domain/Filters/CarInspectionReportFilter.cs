using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEF.Core.Dtos;

namespace VVCar.Shop.Domain.Filters
{
    /// <summary>
    /// 车检报告过滤条件
    /// </summary>
    public class CarInspectionReportFilter : BasePageFilter
    {
        /// <summary>
        /// 接车单ID
        /// </summary>
        public Guid? PickUpOrderID { get; set; }

        /// <summary>
        /// 会员ID
        /// </summary>
        public Guid? MemberID { get; set; }

        /// <summary>
        /// 报告编号
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 车牌号
        /// </summary>
        public string PlateNumber { get; set; }
    }
}
