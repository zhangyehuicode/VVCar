using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEF.Core.Dtos;

namespace VVCar.BaseData.Domain.Filters
{
    /// <summary>
    /// 门店标签过滤条件
    /// </summary>
    public class AgentDepartmentTagFilter : BasePageFilter
    {
        /// <summary>
        /// 门店ID
        /// </summary>
        public Guid? AgentDepartmentID { get; set; }

        /// <summary>
        /// 客户标签ID
        /// </summary>
        public Guid? TagID { get; set; }
    }
}
