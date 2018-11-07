using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEF.Core.Dtos;

namespace VVCar.VIP.Domain.Filters
{
    /// <summary>
    /// 寻客侠浏览历史记录过滤条件
    /// </summary>
    public class AdvisementBrowseHistoryFilter : BasePageFilter
    {
        /// <summary>
        /// 广告ID
        /// </summary>
        public Guid? AdvisementSettingID { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 会员昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 间隔时间
        /// </summary>
        public decimal? Period { get; set; }
    }   
}
