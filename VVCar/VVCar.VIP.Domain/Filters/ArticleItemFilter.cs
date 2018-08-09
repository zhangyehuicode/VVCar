using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEF.Core.Dtos;

namespace VVCar.VIP.Domain.Filters
{
    /// <summary>
    /// 图文消息子项过滤条件
    /// </summary>
    public class ArticleItemFilter : BasePageFilter
    {
        /// <summary>
        /// 图文消息ID
        /// </summary>
        public Guid ArticleID { get; set; }   
    }
}
