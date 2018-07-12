using System;
using YEF.Core.Dtos;
using System.ComponentModel;

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

        /// <summary>
        /// 排序方向
        /// </summary>
        public EUserSortDirection SortDirection { get; set; }
    }

    /// <summary>
    /// 用户数据排序方向
    /// </summary>
    public enum EUserSortDirection
    {
        /// <summary>
        /// 月营业额
        /// </summary>
        [Description("月营业额")]
        MonthPerformance = 0,

        /// <summary>
        /// 月开发门店数
        /// </summary>
        [Description("月开发门店数")]
        MonthOpenAccountCount = 1,
    }
}
