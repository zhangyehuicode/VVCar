using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.Shop.Domain.Entities;

namespace VVCar.Shop.Domain.Dtos
{
    /// <summary>
    /// 车检报告DTO
    /// </summary>
    public class CarInspectionReportDto
    {
        public CarInspectionReportDto()
        {
            CarInspectionDetailsList = new List<CarInspectionDetailsDto>();
        }

        /// <summary>
        /// ID
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// 报告单编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 接车单ID
        /// </summary>
        public Guid? PickUpOrderID { get; set; }

        /// <summary>
        /// 会员ID
        /// </summary>
        public Guid? MemberID { get; set; }

        /// <summary>
        /// 会员OpenID
        /// </summary>
        public string OpenID { get; set; }

        /// <summary>
        /// 车牌号
        /// </summary>
        public string PlateNumber { get; set; }

        /// <summary>
        /// 门店名称
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// 车型
        /// </summary>
        public string CarType { get; set; }

        /// <summary>
        /// 车辆注册时间
        /// </summary>
        public DateTime? RegistDate { get; set; }

        /// <summary>
        /// 当前公里数
        /// </summary>
        public decimal Mileage { get; set; }

        /// <summary>
        /// 车检人员ID
        /// </summary>
        public Guid? InspectorID { get; set; }

        /// <summary>
        /// 车检人员
        /// </summary>
        public string Inspector { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string CreatedUser { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// 车检详情
        /// </summary>
        public ICollection<CarInspectionDetailsDto> CarInspectionDetailsList { get; set; }
    }
}
