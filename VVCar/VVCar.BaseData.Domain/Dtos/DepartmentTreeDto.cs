using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEF.Core.Dtos;

namespace VVCar.BaseData.Domain.Dtos
{
    /// <summary>
    /// 门店区域树型DTO
    /// </summary>
    public class DepartmentTreeDto : TreeNodeModel<DepartmentTreeDto>
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public Guid? ID { get; set; }

        /// <summary>
        /// 上级ID
        /// </summary>
        public Guid? ParentId { get; set; }
    }
}
