using YEF.Core.Dtos;

namespace VVCar.BaseData.Domain.Filters
{
    /// <summary>
    /// 权限过虑条件
    /// </summary>
    public class PermissionFilter : BasePageFilter
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// code
        /// </summary>
        public string Code { get; set; }

    }
}
