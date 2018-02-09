using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEF.Core.Dtos;

namespace VVCar.VIP.Domain.Filters
{
    /// <summary>
    ///会员分组过滤
    /// </summary>
    public class MemberGroupFilter : BasePageFilter
    {
        /// <summary>
        /// 分组名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 编号
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 会员卡号或姓名
        /// </summary>
        public string CardNumberOrName { get; set; }
    }
}
