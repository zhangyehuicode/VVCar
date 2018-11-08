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
            CarInspectionDetailsList = new List<CarInspectionDetails>();
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
        /// 发动机表面状态
        /// </summary>
        public string EnergySurfaceStatus { get; set; }

        /// <summary>
        /// 防冻液状态
        /// </summary>
        public string AntifreezingSolutionStatus { get; set; }

        /// <summary>
        /// 刹车油含水量
        /// </summary>
        public string BrakeOilWaterStatus { get; set; }

        /// <summary>
        /// 刹车油液位
        /// </summary>
        public string BrakeOilLevelStatus { get; set; }

        /// <summary>
        /// 机油液位
        /// </summary>
        public string EngineOilLevelStatus { get; set; }

        /// <summary>
        /// 机油形态
        /// </summary>
        public string EngineOilShapeStatus { get; set; }

        /// <summary>
        /// 保养到期时间
        /// </summary>
        public string MaintainExpiredStatus { get; set; }

        /// <summary>
        /// 左前轮胎
        /// </summary>
        public string LeftFrontTireStatus { get; set; }

        /// <summary>
        /// 右前轮胎
        /// </summary>
        public string RightFrontTireStatus { get; set; }

        /// <summary>
        /// 左后轮胎
        /// </summary>
        public string LeftBackTireStatus { get; set; }

        /// <summary>
        /// 右后轮胎
        /// </summary>
        public string RightBackTireStatus { get; set; }

        /// <summary>
        /// 左前车毂
        /// </summary>
        public string LeftFrontRimStatus { get; set; }

        /// <summary>
        /// 右前车毂
        /// </summary>
        public string RightFrontRimStatus { get; set; }

        /// <summary>
        /// 左后车毂
        /// </summary>
        public string LeftBackRimStatus { get; set; }

        /// <summary>
        /// 右后车毂
        /// </summary>
        public string RightBackRimStatus { get; set; }

        /// <summary>
        /// 前雨刮
        /// </summary>
        public string FrontWiperStatus { get; set; }

        /// <summary>
        /// 后雨刮
        /// </summary>
        public string BackWiperStatus { get; set; }

        /// <summary>
        /// 前保险杆漆面
        /// </summary>
        public string FrontGuardLacquerStatus { get; set; }

        /// <summary>
        /// 后保险杆漆面
        /// </summary>
        public string BackGuardLacquerStatus { get; set; }

        /// <summary>
        /// 前引擎盖漆面
        /// </summary>
        public string FrontHoodLacquerStatus { get; set; }

        /// <summary>
        /// 后备箱盖
        /// </summary>
        public string TrunkLidLacquerStatus { get; set; }

        /// <summary>
        /// 左前叶漆面
        /// </summary>
        public string LeftFrontPageLacquerStatus { get; set; }

        /// <summary>
        /// 右前叶漆面
        /// </summary>
        public string RightFrontPageLacquerStatus { get; set; }

        /// <summary>
        /// 左后叶漆面
        /// </summary>
        public string LeftBackPageLacquerStatus { get; set; }

        /// <summary>
        /// 右后叶漆面
        /// </summary>
        public string RightBackPageLacquerStatus { get; set; }


        /// <summary>
        /// 左前门漆面
        /// </summary>
        public string LeftFrontDoorLacquerStatus { get; set; }

        /// <summary>
        /// 右前叶漆面
        /// </summary>
        public string RightFrontDoorLacquerStatus { get; set; }

        /// <summary>
        /// 左后叶漆面
        /// </summary>
        public string LeftBackDoorLacquerStatus { get; set; }

        /// <summary>
        /// 右后叶漆面
        /// </summary>
        public string RightBackDoorLacquerStatus { get; set; }

        /// <summary>
        /// 玻璃氧化检测
        /// </summary>
        public string GlassOxidationDetectionStatus { get; set; }

        /// <summary>
        /// 玻璃破裂
        /// </summary>
        public string GlassRuptureStatus { get; set; }

        /// <summary>
        /// 隔热膜
        /// </summary>
        public string HeatInsulatingFilmStatus { get; set; }

        /// <summary>
        /// 车内异味
        /// </summary>
        public string PeculiarSmellStatus { get; set; }

        /// <summary>
        /// 座椅
        /// </summary>
        public string SeatStatus { get; set; }

        /// <summary>
        /// 仪表盘
        /// </summary>
        public string DashBoardStatus { get; set; }

        /// <summary>
        /// 门内侧
        /// </summary>
        public string MedialPortalStatus { get; set; }

        /// <summary>
        /// 橡塑件
        /// </summary>
        public string PlasticPartStatus { get; set; }

        /// <summary>
        /// 镀铬件
        /// </summary>
        public string ChromiumPlatedPartStatus { get; set; }

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
        public ICollection<CarInspectionDetails> CarInspectionDetailsList { get; set; }
    }
}
