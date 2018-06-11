using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVCar.Shop.Domain.Dtos
{
    /// <summary>
    /// 接车单历史数据分析参数
    /// </summary>
    public class HistoryDataAnalysisParam
    {
        /// <summary>
        /// 会员ID
        /// </summary>
        public Guid? MemberID { get; set; }

        /// <summary>
        /// OpenID
        /// </summary>
        public string OpenID { get; set; }
    }
}
