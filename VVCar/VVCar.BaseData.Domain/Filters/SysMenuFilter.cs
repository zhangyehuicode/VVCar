using System;
using YEF.Core.Dtos;

namespace VVCar.BaseData.Domain.Filters
{
    public class SysMenuFilter : BaseFilter
    {
        /// <summary>
        /// 父级ID
        /// </summary>
        public Guid? ParentID { get; set; }
    }
}
