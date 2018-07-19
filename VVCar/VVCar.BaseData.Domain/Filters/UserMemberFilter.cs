using System;
using YEF.Core.Dtos;

namespace VVCar.BaseData.Domain.Filters
{
    /// <summary>
    /// 用户会员过滤条件
    /// </summary>
    public class UserMemberFilter : BasePageFilter
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid? UserID { get; set; }
    }
}
