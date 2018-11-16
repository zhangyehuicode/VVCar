using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEF.Core.Dtos;

namespace VVCar.VIP.Domain.Filters
{
    public class BrowseAnalyseFilter : BasePageFilter
    {
        /// <summary>
        /// 广告ID
        /// </summary>
        public Guid? AdvisementSettingID { get; set; }

        /// <summary>
        /// 停留时间
        /// </summary>
        public decimal? StayPeriod { get; set; }

        /// <summary>
        /// 点击次数
        /// </summary>
        public int? ClickCount { get; set; }

        public string ShareOpenID { get; set; }
    }
}
