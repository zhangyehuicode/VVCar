using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVCar.BaseData.Domain.Dtos
{
    /// <summary>
    /// 门店 Lite
    /// </summary>
    public class DepartmentLiteDto
    {
        /// <summary>
        /// 主键
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// 门店代码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 门店名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 地区分区ID
        /// </summary>
        public Guid DistrictRegionID { get; set; }
    }
}
