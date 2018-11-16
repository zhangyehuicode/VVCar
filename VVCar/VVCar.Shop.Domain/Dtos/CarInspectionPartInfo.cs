using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.Shop.Domain.Entities;

namespace VVCar.Shop.Domain.Dtos
{
    /// <summary>
    /// 车检部位信息
    /// </summary>
    public class CarInspectionPartInfo
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 部位
        /// </summary>
        public ECarInspectionPart Part { get; set; }
    }
}
