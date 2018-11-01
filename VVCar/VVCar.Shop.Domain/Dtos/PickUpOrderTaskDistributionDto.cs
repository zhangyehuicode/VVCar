using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.Shop.Domain.Enums;

namespace VVCar.Shop.Domain.Dtos
{
    /// <summary>
    /// 接车单任务分配DTO
    /// </summary>
    public class PickUpOrderTaskDistributionDto
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// 员工编号
        /// </summary>
        public string UserCode { get; set; }

        /// <summary>
        /// 员工姓名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 抽成比例
        /// </summary>
        public decimal CommissionRate { get; set; }

        /// <summary>
        /// 抽成
        /// </summary>
        public decimal Commission { get; set; }

        /// <summary>
        /// 人员类型
        /// </summary>
        public ETaskDistributionPeopleType PeopleType { get; set; }
    }
}
