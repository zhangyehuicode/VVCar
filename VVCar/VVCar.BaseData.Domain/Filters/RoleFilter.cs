using YEF.Core.Dtos;

namespace VVCar.BaseData.Domain.Filters
{
    /// <summary>
    /// 角色过滤条件
    /// </summary>
    public class RoleFilter : BaseFilter
    {
        /// <summary>
        /// 角色代码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 角色类别
        /// </summary>
        public string RoleType { get; set; }
    }
}
