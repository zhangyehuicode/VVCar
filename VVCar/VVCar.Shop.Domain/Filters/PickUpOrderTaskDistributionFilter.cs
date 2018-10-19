using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.Shop.Domain.Enums;
using YEF.Core.Dtos;

namespace VVCar.Shop.Domain.Filters
{
    /// <summary>
    /// 接车单任务分配过滤条件
    /// </summary>
    public class PickUpOrderTaskDistributionFilter : BasePageFilter
    {
        /// <summary>
        /// 项目ID
        /// </summary>
        public Guid? PickUpOrderItemID { get; set; }

        /// <summary>
        /// 人员类型
        /// </summary>
        public ETaskDistributionPeopleType? PeopleType { get; set; }
    }
}
