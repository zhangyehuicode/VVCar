using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVCar.VIP.Domain.Dtos
{
    /// <summary>
    /// 浏览分析
    /// </summary>
    public class BrowseAnalyseDto
    {
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 停留时间
        /// </summary>
        public decimal StayPeriod { get; set; }

        /// <summary>
        /// 点击次数
        /// </summary>
        public int ClickCount { get; set; }
    }
}
