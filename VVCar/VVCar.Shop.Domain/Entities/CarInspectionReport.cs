using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEF.Core.Data;
using System.ComponentModel.DataAnnotations;

namespace VVCar.Shop.Domain.Entities
{
    /// <summary>
    /// 车检报告单
    /// </summary>
    public class CarInspectionReport : EntityBase
    {
        /// <summary>
        /// 报告单编码
        /// </summary>
        [Display(Name = "报告单编码")]
        public string Code { get; set; }

        /// <summary>
        /// 接车单ID
        /// </summary>
        [Display(Name = "接车单ID")]
        public Guid? PickUpOrderID { get; set; }

        /// <summary>
        /// 会员ID
        /// </summary>
        [Display(Name = "会员ID")]
        public Guid? MemberID { get; set; }

        /// <summary>
        /// 会员OpenID
        /// </summary>
        [Display(Name = "会员OpenID")]
        public string OpenID { get; set; }

        /// <summary>
        /// 车牌号
        /// </summary>
        [Display(Name = "车牌号")]
        public string PlateNumber { get; set; }

        /// <summary>
        /// 门店名称
        /// </summary>
        [Display(Name = "门店名称")]
        public string DepartmentName { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
        [Display(Name = "品牌")]
        public string Brand { get; set; }

        /// <summary>
        /// 车型
        /// </summary>
        [Display(Name = "车型")]
        public string CarType { get; set; }

        /// <summary>
        /// 车辆注册时间
        /// </summary>
        [Display(Name = "车辆注册时间")]
        public DateTime RegistDate { get; set; }

        /// <summary>
        /// 当前公里数
        /// </summary>
        [Display(Name = "当前公里数")]
        public decimal Mileage { get; set; }

        /// <summary>
        /// 车检人员
        /// </summary>
        [Display(Name = "车检人员")]
        public string Inspector { get; set; }

        /// <summary>
        /// 发动机表面状态
        /// </summary>
        [Display(Name = "发动机表面状态")]
        public EEnergySurfaceStatus EnergySurfaceStatus { get; set; }

        /// <summary>
        /// 防冻液状态
        /// </summary>
        [Display(Name = "防冻液状态")]
        public EAntifreezingSolutionStatus AntifreezingSolutionStatus { get; set; }

        /// <summary>
        /// 刹车油含水量
        /// </summary>
        [Display(Name = "刹车油含水量")]
        public EBrakeOilWaterStatus BrakeOilWaterStatus { get; set; }

        /// <summary>
        /// 刹车油液位
        /// </summary>
        [Display(Name = "刹车油液位")]
        public EBrakeOilLevelStatus BrakeOilLevelStatus { get; set; }

        /// <summary>
        /// 机油液位
        /// </summary>
        [Display(Name = "机油液位")]
        public EEngineOilLevelStatus EngineOilLevelStatus { get; set; }

        /// <summary>
        /// 机油形态
        /// </summary>
        [Display(Name = "机油形态")]
        public EEngineOilShapeStatus EngineOilShapeStatus { get; set; }

        /// <summary>
        /// 保养到期时间
        /// </summary>
        [Display(Name = "保养到期时间")]
        public EMaintainExpiredStatus MaintainExpiredStatus { get; set; }

        /// <summary>
        /// 左前轮胎
        /// </summary>
        [Display(Name = "左前轮胎")]
        public ELeftFrontTireStatus LeftFrontTireStatus { get; set; }

        /// <summary>
        /// 右前轮胎
        /// </summary>
        [Display(Name = "右前轮胎")]
        public ERightFrontTireStatus RightFrontTireStatus { get; set; }

        /// <summary>
        /// 左后轮胎
        /// </summary>
        [Display(Name = "左后轮胎")]
        public ELeftBackTireStatus LeftBackTireStatus { get; set; }

        /// <summary>
        /// 右后轮胎
        /// </summary>
        [Display(Name = "右后轮胎")]
        public ERightBackTireStatus RightBackTireStatus { get; set; }

        /// <summary>
        /// 左前车毂
        /// </summary>
        [Display(Name = "左前车毂")]
        public ELeftFrontRimStatus LeftFrontRimStatus { get; set; }

        /// <summary>
        /// 右前车毂
        /// </summary>
        [Display(Name = "右前车毂")]
        public ERightFrontRimStatus RightFrontRimStatus { get; set; }

        /// <summary>
        /// 左后车毂
        /// </summary>
        [Display(Name = "左后车毂")]
        public ELeftBackRimStatus LeftBackRimStatus { get; set; }

        /// <summary>
        /// 右后车毂
        /// </summary>
        [Display(Name = "右后车毂")]
        public ERightBackRimStatus RightBackRimStatus { get; set; }

        /// <summary>
        /// 前雨刮
        /// </summary>
        [Display(Name = "前雨刮")]
        public EFrontWiperStatus FrontWiperStatus { get; set; }

        /// <summary>
        /// 后雨刮
        /// </summary>
        [Display(Name = "后雨刮")]
        public EBackWiperStatus BackWiperStatus { get; set; }

        /// <summary>
        /// 前保险杆漆面
        /// </summary>
        [Display(Name = "前保险杆漆面")]
        public EFrontGuardLacquerStatus FrontGuardLacquerStatus { get; set; }

        /// <summary>
        /// 后保险杆漆面
        /// </summary>
        [Display(Name = "后保险杆漆面")]
        public EBackGuardLacquerStatus BackGuardLacquerStatus { get; set; }

        /// <summary>
        /// 前引擎盖漆面
        /// </summary>
        [Display(Name = "前引擎盖漆面")]
        public EFrontHoodLacquerStatus FrontHoodLacquerStatus { get; set; }

        /// <summary>
        /// 后备箱盖
        /// </summary>
        [Display(Name = "后备箱盖")]
        public ETrunkLidLacquerStatus TrunkLidLacquerStatus { get; set; }

        /// <summary>
        /// 左前叶漆面
        /// </summary>
        [Display(Name = "左前叶漆面")]
        public ELeftFrontPageLacquerStatus LeftFrontPageLacquerStatus { get; set; }

        /// <summary>
        /// 右前叶漆面
        /// </summary>
        [Display(Name = "右前叶漆面")]
        public ERightFrontPageLacquerStatus RightFrontPageLacquerStatus { get; set; }

        /// <summary>
        /// 左后叶漆面
        /// </summary>
        [Display(Name = "左后叶漆面")]
        public ELeftBackPageLacquerStatus LeftBackPageLacquerStatus { get; set; }

        /// <summary>
        /// 右后叶漆面
        /// </summary>
        [Display(Name = "右后叶漆面")]
        public ERightBackPageLacquerStatus RightBackPageLacquerStatus { get; set; }


        /// <summary>
        /// 左前门漆面
        /// </summary>
        [Display(Name = "左前门漆面")]
        public ELeftFrontDoorLacquerStatus LeftFrontDoorLacquerStatus { get; set; }

        /// <summary>
        /// 右前叶漆面
        /// </summary>
        [Display(Name = "右前叶漆面")]
        public ERightFrontDoorLacquerStatus RightFrontDoorLacquerStatus { get; set; }

        /// <summary>
        /// 左后叶漆面
        /// </summary>
        [Display(Name = "左后叶漆面")]
        public ELeftBackDoorLacquerStatus LeftBackDoorLacquerStatus { get; set; }

        /// <summary>
        /// 右后叶漆面
        /// </summary>
        [Display(Name = "右后叶漆面")]
        public ERightBackDoorLacquerStatus RightBackDoorLacquerStatus { get; set; }

        /// <summary>
        /// 玻璃氧化检测
        /// </summary>
        [Display(Name = "玻璃氧化检测")]
        public EGlassOxidationDetectionStatus GlassOxidationDetectionStatus { get; set; }

        /// <summary>
        /// 玻璃破裂
        /// </summary>
        [Display(Name = "玻璃破裂")]
        public EGlassRuptureStatus GlassRuptureStatus { get; set; }

        /// <summary>
        /// 隔热膜
        /// </summary>
        [Display(Name = "隔热膜")]
        public EHeatInsulatingFilmStatus HeatInsulatingFilmStatus { get; set; }

        /// <summary>
        /// 车内异味
        /// </summary>
        [Display(Name = "车内异味")]
        public EPeculiarSmellStatus PeculiarSmellStatus { get; set; }

        /// <summary>
        /// 座椅
        /// </summary>
        [Display(Name = "座椅")]
        public ESeatStatus SeatStatus { get; set; }

        /// <summary>
        /// 仪表盘
        /// </summary>
        [Display(Name = "仪表盘")]
        public EDashBoardStatus DashBoardStatus { get; set; }

        /// <summary>
        /// 门内侧
        /// </summary>
        [Display(Name = "门内侧")]
        public EMedialPortalStatus MedialPortalStatus { get; set; }

        /// <summary>
        /// 橡塑件
        /// </summary>
        [Display(Name = "塑料件")]
        public EPlasticPartStatus PlasticPartStatus { get; set; }

        /// <summary>
        /// 镀铬件
        /// </summary>
        [Display(Name = "镀铬件")]
        public EChromiumPlatedPartStatus ChromiumPlatedPartStatus { get; set; }

        /// <summary>
        /// 创建人ID
        /// </summary>
        [Display(Name = "创建人ID")]
        public Guid CreatedUserID { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [Display(Name = "创建人")]
        public string CreatedUser { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Display(Name = "创建时间")]
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// 最后修改人ID
        /// </summary>
        [Display(Name = "最后修改人ID")]
        public Guid? LastUpdateUserID { get; set; }

        /// <summary>
        /// 最后修改人
        /// </summary>
        [Display(Name = "最后修改人")]
        public string LastUpdateUser { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        [Display(Name = "最后修改时间")]
        public DateTime? LastUpdateDate { get; set; }
    }

    /// <summary>
    /// 发动机表面状态
    /// </summary>
    public enum EEnergySurfaceStatus
    {
        /// <summary>
        /// 正常
        /// </summary>
        [Display(Name = "正常")]
        Normal = 0,

        /// <summary>
        /// 不正常
        /// </summary>
        [Display(Name = "不正常")]
        OffNormal = 1,
    }

    /// <summary>
    /// 防冻液状态
    /// </summary>
    public enum EAntifreezingSolutionStatus
    {
        /// <summary>
        /// 正常
        /// </summary>
        [Display(Name = "正常")]
        Normal = 0,

        /// <summary>
        /// 不正常
        /// </summary>
        [Display(Name = "不正常")]
        OffNormal = 1,
    }

    /// <summary>
    /// 刹车油含水量
    /// </summary>
    public enum EBrakeOilWaterStatus
    {
        /// <summary>
        /// 百分三
        /// </summary>
        [Display(Name = "百分三")]
        ThreePercentage = 1,

        /// <summary>
        /// 百分十
        /// </summary>
        [Display(Name = "百分十")]
        TenPercentage = 2,
    }

    /// <summary>
    /// 刹车油液位
    /// </summary>
    public enum EBrakeOilLevelStatus
    {
        /// <summary>
        /// 正常液位
        /// </summary>
        [Display(Name = "正常液位")]
        Normal = 0,

        /// <summary>
        /// 液位过低
        /// </summary>
        [Display(Name = "液位过低")]
        Lower = 1,
    }

    /// <summary>
    /// 机油液位
    /// </summary>
    public enum EEngineOilLevelStatus
    {
        /// <summary>
        /// 正常液位
        /// </summary>
        [Display(Name = "正常液位")]
        Normal = 0,

        /// <summary>
        /// 液位过低
        /// </summary>
        [Display(Name = "液位过低")]
        Lower = 1,
    }

    /// <summary>
    /// 机油形态
    /// </summary>
    public enum EEngineOilShapeStatus
    {
        /// <summary>
        /// 正常
        /// </summary>
        [Display(Name = "正常")]
        Normal = 0,

        /// <summary>
        /// 不正常
        /// </summary>
        [Display(Name = "不正常")]
        OffNormal = 1,
    }

    /// <summary>
    /// 保养到期时间
    /// </summary>
    public enum EMaintainExpiredStatus
    {
        /// <summary>
        /// 正常
        /// </summary>
        [Display(Name = "正常")]
        Normal = 0,

        /// <summary>
        /// 过期
        /// </summary>
        [Display(Name = "过期")]
        Expired = 1,
    }

    /// <summary>
    /// 左前轮胎
    /// </summary>
    public enum ELeftFrontTireStatus
    {
        /// <summary>
        /// 正常
        /// </summary>
        [Display(Name = "正常")]
        Normal = 0,

        /// <summary>
        /// 不正常
        /// </summary>
        [Display(Name = "不正常")]
        OffNormal = 1,
    }

    /// <summary>
    /// 右前轮胎
    /// </summary>
    public enum ERightFrontTireStatus
    {
        /// <summary>
        /// 正常
        /// </summary>
        [Display(Name = "正常")]
        Normal = 0,

        /// <summary>
        /// 不正常
        /// </summary>
        [Display(Name = "不正常")]
        OffNormal = 1,
    }

    /// <summary>
    /// 左后轮胎
    /// </summary>
    public enum ELeftBackTireStatus
    {
        /// <summary>
        /// 正常
        /// </summary>
        [Display(Name = "正常")]
        Normal = 0,

        /// <summary>
        /// 不正常
        /// </summary>
        [Display(Name = "不正常")]
        OffNormal = 1,
    }

    /// <summary>
    /// 右后轮胎
    /// </summary>
    public enum ERightBackTireStatus
    {
        /// <summary>
        /// 正常
        /// </summary>
        [Display(Name = "正常")]
        Normal = 0,

        /// <summary>
        /// 不正常
        /// </summary>
        [Display(Name = "不正常")]
        OffNormal = 1,
    }

    /// <summary>
    /// 左前车毂
    /// </summary>
    public enum ELeftFrontRimStatus
    {
        /// <summary>
        /// 正常
        /// </summary>
        [Display(Name = "正常")]
        Normal = 0,

        /// <summary>
        /// 不正常
        /// </summary>
        [Display(Name = "不正常")]
        OffNormal = 1,
    }

    /// <summary>
    /// 右前车毂
    /// </summary>
    public enum ERightFrontRimStatus
    {
        /// <summary>
        /// 正常
        /// </summary>
        [Display(Name = "正常")]
        Normal = 0,

        /// <summary>
        /// 不正常
        /// </summary>
        [Display(Name = "不正常")]
        OffNormal = 1,
    }

    /// <summary>
    /// 左后车毂
    /// </summary>
    public enum ELeftBackRimStatus
    {
        /// <summary>
        /// 正常
        /// </summary>
        [Display(Name = "正常")]
        Normal = 0,

        /// <summary>
        /// 不正常
        /// </summary>
        [Display(Name = "不正常")]
        OffNormal = 1,
    }

    /// <summary>
    /// 右后车毂
    /// </summary>
    public enum ERightBackRimStatus
    {
        /// <summary>
        /// 正常
        /// </summary>
        [Display(Name = "正常")]
        Normal = 0,

        /// <summary>
        /// 不正常
        /// </summary>
        [Display(Name = "不正常")]
        OffNormal = 1,
    }

    /// <summary>
    /// 前雨刷
    /// </summary>
    public enum EFrontWiperStatus
    {
        /// <summary>
        /// 正常
        /// </summary>
        [Display(Name = "正常")]
        Normal = 0,

        /// <summary>
        /// 不正常
        /// </summary>
        [Display(Name = "不正常")]
        OffNormal = 1,
    }

    /// <summary>
    /// 后雨刷
    /// </summary>
    public enum EBackWiperStatus
    {
        /// <summary>
        /// 正常
        /// </summary>
        [Display(Name = "正常")]
        Normal = 0,

        /// <summary>
        /// 不正常
        /// </summary>
        [Display(Name = "不正常")]
        OffNormal = 1,
    }

    /// <summary>
    /// 前保险杆漆面状态
    /// </summary>
    public enum EFrontGuardLacquerStatus
    {
        /// <summary>
        /// 正常
        /// </summary>
        [Display(Name = "正常")]
        Normal = 0,

        /// <summary>
        /// 划痕
        /// </summary>
        [Display(Name = "划痕")]
        Scratch = 1,
    }

    /// <summary>
    /// 后保险杆漆面状态
    /// </summary>
    public enum EBackGuardLacquerStatus
    {
        /// <summary>
        /// 正常
        /// </summary>
        [Display(Name = "正常")]
        Normal = 0,

        /// <summary>
        /// 划痕
        /// </summary>
        [Display(Name = "划痕")]
        Scratch = 1,
    }

    /// <summary>
    /// 前引擎盖漆面
    /// </summary>
    public enum EFrontHoodLacquerStatus
    {
        /// <summary>
        /// 正常
        /// </summary>
        [Display(Name = "正常")]
        Normal = 0,

        /// <summary>
        /// 划痕
        /// </summary>
        [Display(Name = "划痕")]
        Scratch = 1,
    }

    /// <summary>
    /// 后备箱盖状态
    /// </summary>
    public enum ETrunkLidLacquerStatus
    {
        /// <summary>
        /// 正常
        /// </summary>
        [Display(Name = "正常")]
        Normal = 0,

        /// <summary>
        /// 划痕
        /// </summary>
        [Display(Name = "划痕")]
        Scratch = 1,
    }

    /// <summary>
    /// 左前叶漆面
    /// </summary>
    public enum ELeftFrontPageLacquerStatus
    {
        /// <summary>
        /// 正常
        /// </summary>
        [Display(Name = "正常")]
        Normal = 0,

        /// <summary>
        /// 划痕
        /// </summary>
        [Display(Name = "划痕")]
        Scratch = 1,
    }

    /// <summary>
    /// 右前叶漆面
    /// </summary>
    public enum ERightFrontPageLacquerStatus
    {
        /// <summary>
        /// 正常
        /// </summary>
        [Display(Name = "正常")]
        Normal = 0,

        /// <summary>
        /// 划痕
        /// </summary>
        [Display(Name = "划痕")]
        Scratch = 1,
    }

    /// <summary>
    /// 左后叶漆面
    /// </summary>
    public enum ELeftBackPageLacquerStatus
    {
        /// <summary>
        /// 正常
        /// </summary>
        [Display(Name = "正常")]
        Normal = 0,

        /// <summary>
        /// 划痕
        /// </summary>
        [Display(Name = "划痕")]
        Scratch = 1,
    }


    /// <summary>
    /// 右后叶漆面
    /// </summary>
    public enum ERightBackPageLacquerStatus
    {
        /// <summary>
        /// 正常
        /// </summary>
        [Display(Name = "正常")]
        Normal = 0,

        /// <summary>
        /// 划痕
        /// </summary>
        [Display(Name = "划痕")]
        Scratch = 1,
    }

    /// <summary>
    /// 左前门漆面
    /// </summary>
    public enum ELeftFrontDoorLacquerStatus
    {
        /// <summary>
        /// 正常
        /// </summary>
        [Display(Name = "正常")]
        Normal = 0,

        /// <summary>
        /// 划痕
        /// </summary>
        [Display(Name = "划痕")]
        Scratch = 1,
    }

    /// <summary>
    /// 右前门漆面
    /// </summary>
    public enum ERightFrontDoorLacquerStatus
    {
        /// <summary>
        /// 正常
        /// </summary>
        [Display(Name = "正常")]
        Normal = 0,

        /// <summary>
        /// 划痕
        /// </summary>
        [Display(Name = "划痕")]
        Scratch = 1,
    }

    /// <summary>
    /// 左后门漆面
    /// </summary>
    public enum ELeftBackDoorLacquerStatus
    {
        /// <summary>
        /// 正常
        /// </summary>
        [Display(Name = "正常")]
        Normal = 0,

        /// <summary>
        /// 划痕
        /// </summary>
        [Display(Name = "划痕")]
        Scratch = 1,
    }

    /// <summary>
    /// 右后门漆面
    /// </summary>
    public enum ERightBackDoorLacquerStatus
    {
        /// <summary>
        /// 正常
        /// </summary>
        [Display(Name = "正常")]
        Normal = 0,

        /// <summary>
        /// 划痕
        /// </summary>
        [Display(Name = "划痕")]
        Scratch = 1,
    }

    /// <summary>
    /// 玻璃氧化检测
    /// </summary>
    public enum EGlassOxidationDetectionStatus
    {
        /// <summary>
        /// 正常
        /// </summary>
        [Display(Name = "正常")]
        Normal = 0,

        /// <summary>
        /// 不正常
        /// </summary>
        [Display(Name = "不正常")]
        OffNormal = 1,
    }

    /// <summary>
    /// 玻璃破裂
    /// </summary>
    public enum EGlassRuptureStatus
    {
        /// <summary>
        /// 正常
        /// </summary>
        [Display(Name = "正常")]
        Normal = 0,

        /// <summary>
        /// 破裂
        /// </summary>
        [Display(Name = "破裂")]
        Rupture = 1,
    }

    /// <summary>
    /// 隔热膜
    /// </summary>
    public enum EHeatInsulatingFilmStatus
    {
        /// <summary>
        /// 正常
        /// </summary>
        [Display(Name = "正常")]
        Normal = 0,

        /// <summary>
        /// 鼓包
        /// </summary>
        [Display(Name = "鼓包")]
        Swell = 1,
    }

    /// <summary>
    /// 异味
    /// </summary>
    public enum EPeculiarSmellStatus
    {
        /// <summary>
        /// 正常
        /// </summary>
        [Display(Name = "正常")]
        Normal = 0,

        /// <summary>
        /// 不正常
        /// </summary>
        [Display(Name = "不正常")]
        OffNormal = 1,
    }

    /// <summary>
    /// 座椅
    /// </summary>
    public enum ESeatStatus
    {
        /// <summary>
        /// 正常
        /// </summary>
        [Display(Name = "正常")]
        Normal = 0,

        /// <summary>
        /// 破损
        /// </summary>
        [Display(Name = "破损")]
        Damage = 1,
    }

    /// <summary>
    /// 仪表盘
    /// </summary>
    public enum EDashBoardStatus
    {
        /// <summary>
        /// 正常
        /// </summary>
        [Display(Name = "正常")]
        Normal = 0,

        /// <summary>
        /// 破损
        /// </summary>
        [Display(Name = "破损")]
        Damage = 1,
    }

    /// <summary>
    /// 门内侧
    /// </summary>
    public enum EMedialPortalStatus
    {
        /// <summary>
        /// 正常
        /// </summary>
        [Display(Name = "正常")]
        Normal = 0,

        /// <summary>
        /// 脏污
        /// </summary>
        [Display(Name = "脏污")]
        Dirty = 1,
    }

    /// <summary>
    /// 橡塑件
    /// </summary>
    public enum EPlasticPartStatus
    {
        /// <summary>
        /// 正常
        /// </summary>
        [Display(Name = "正常")]
        Normal = 0,

        /// <summary>
        /// 氧化发白
        /// </summary>
        [Display(Name = "氧化发白")]
        Oxide = 1,
    }

    /// <summary>
    /// 镀铬件
    /// </summary>
    public enum EChromiumPlatedPartStatus
    {
        /// <summary>
        /// 正常
        /// </summary>
        [Display(Name = "正常")]
        Normal = 0,

        /// <summary>
        /// 不正常
        /// </summary>
        [Display(Name = "不正常")]
        OffNormal = 1,
    }
}
