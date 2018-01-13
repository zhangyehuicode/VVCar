using System;
using YEF.Core.Dtos;

namespace VVCar.BaseData.Domain.Filters
{
    /// <summary>
    /// 用户过滤条件
    /// </summary>
    public class UserFilter : BasePageFilter
    {
        /// <summary>
        /// 代码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 门店
        /// </summary>
        public Guid? Department { get; set; }
    }
}
