using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEF.Core.Dtos;

namespace VVCar.VIP.Domain.Filters
{
    /// <summary>
    /// 寻客侠广告设置
    /// </summary>
    public class AdvisementSettingFilter : BasePageFilter
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid? ID { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 微信端访问
        /// </summary>
        public bool? IsFromBackground { get; set; }
    }
}
