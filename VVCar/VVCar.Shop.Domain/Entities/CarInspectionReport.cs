using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEF.Core.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace VVCar.Shop.Domain.Entities
{
    /// <summary>
    /// 车检报告单
    /// </summary>
    public class CarInspectionReport : EntityBase
    {
        public CarInspectionReport()
        {
            CarInspectionDetailsList = new List<CarInspectionDetails>();
        }

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
        /// 门店ID
        /// </summary>
        [Display(Name = "门店ID")]
        public Guid? DepartmentID { get; set; }

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
        public DateTime? RegistDate { get; set; }

        /// <summary>
        /// 当前公里数
        /// </summary>
        [Display(Name = "当前公里数")]
        public decimal Mileage { get; set; }

        /// <summary>
        /// 车检人员ID
        /// </summary>
        [Display(Name = "车检人员ID")]
        public Guid? InspectorID { get; set; }

        /// <summary>
        /// 车检人员
        /// </summary>
        [Display(Name = "车检人员")]
        public string Inspector { get; set; }

        /// <summary>
        /// 结论
        /// </summary>
        [Display(Name = "结论")]
        public string Conclusion { get; set; }

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

        /// <summary>
        /// 车检详情
        /// </summary>
        [Display(Name = "车检详情")]
        public ICollection<CarInspectionDetails> CarInspectionDetailsList { get; set; }
    }

    /// <summary>
    /// 车检详情
    /// </summary>
    public class CarInspectionDetails : EntityBase
    {
        public CarInspectionDetails()
        {
            ImgList = new List<CarInspectionDetailsImg>();
        }

        /// <summary>
        /// 车检ID
        /// </summary>
        [Display(Name = "车检ID")]
        public Guid CarInspectionReportID { get; set; }

        /// <summary>
        /// 车检
        /// </summary>
        public virtual CarInspectionReport CarInspectionReport { get; set; }

        /// <summary>
        /// 检查部位
        /// </summary>
        [Display(Name = "检查部位")]
        public ECarInspectionPart Part { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [Display(Name = "状态")]
        public ECarInspectionStatus Status { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        [Display(Name = "说明")]
        public string Explain { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        [Display(Name = "图片")]
        public ICollection<CarInspectionDetailsImg> ImgList { get; set; }
    }

    /// <summary>
    /// 车检详情图片
    /// </summary>
    public class CarInspectionDetailsImg : EntityBase
    {
        /// <summary>
        /// 车检详情ID
        /// </summary>
        [Display(Name = "车检详情ID")]
        public Guid CarInspectionDetailsID { get; set; }

        /// <summary>
        /// 车检详情
        /// </summary>
        public virtual CarInspectionDetails CarInspectionDetails { get; set; }

        /// <summary>
        /// 图片路径
        /// </summary>
        [Display(Name = "图片路径")]
        public string ImgUrl { get; set; }
    }

    /// <summary>
    /// 车检状态
    /// </summary>
    public enum ECarInspectionStatus
    {
        /// <summary>
        /// 正常
        /// </summary>
        [Description("正常")]
        Normal = 0,

        /// <summary>
        /// 异常
        /// </summary>
        [Description("异常")]
        Abnormal = 1,
    }

    /// <summary>
    /// 车检部位
    /// </summary>
    public enum ECarInspectionPart
    {
        /// <summary>
        /// 无
        /// </summary>
        [Description("无")]
        None = 0,

        /// <summary>
        /// 前保险杠漆面
        /// </summary>
        [Description("前保险杠漆面")]
        FrontGuardLacquer = 1,

        /// <summary>
        /// 前引擎盖漆面
        /// </summary>
        [Description("前引擎盖漆面")]
        FrontHoodLacquer = 2,

        /// <summary>
        /// 右前叶漆面
        /// </summary>
        [Description("右前叶漆面")]
        LeftFrontPageLacquer = 3,

        /// <summary>
        /// 左前门漆面
        /// </summary>
        [Description("左前门漆面")]
        LeftFrontDoorLacquer = 4,

        /// <summary>
        /// 左后叶漆面
        /// </summary>
        [Description("左后叶漆面")]
        LeftBackPageLacquer = 5,

        /// <summary>
        /// 左后门漆面
        /// </summary>
        [Description("左后门漆面")]
        LeftBackDoorLacquer = 6,

        /// <summary>
        /// 后保险杠漆面
        /// </summary>
        [Description("后保险杠漆面")]
        BackGuardLacquer = 7,

        /// <summary>
        /// 后备箱盖
        /// </summary>
        [Description("后备箱盖")]
        TrunkLidLacquer = 8,

        /// <summary>
        /// 右前叶漆面
        /// </summary>
        [Description("右前叶漆面")]
        RightFrontPageLacquer = 9,

        /// <summary>
        /// 右前门漆面
        /// </summary>
        [Description("右前门漆面")]
        RightFrontDoorLacquer = 10,

        /// <summary>
        /// 右后叶漆面
        /// </summary>
        [Description("右后叶漆面")]
        RightBackPageLacquer = 11,

        /// <summary>
        /// 右后门漆面
        /// </summary>
        [Description("右后门漆面")]
        RightBackDoorLacquerStatus = 12,

        /// <summary>
        /// 左前轮胎
        /// </summary>
        [Description("左前轮胎")]
        LeftFrontTire = 13,

        /// <summary>
        /// 右前轮胎
        /// </summary>
        [Description("右前轮胎")]
        RightFrontTire = 14,

        /// <summary>
        /// 左后轮胎
        /// </summary>
        [Description("左后轮胎")]
        LeftBackTire = 15,

        /// <summary>
        /// 右后轮胎
        /// </summary>
        [Description("右后轮胎")]
        RightBackTire = 16,

        /// <summary>
        /// 左前车毂
        /// </summary>
        [Description("左前车毂")]
        LeftFrontRim = 17,

        /// <summary>
        /// 右前车毂
        /// </summary>
        [Description("右前车毂")]
        RightFrontRimStatus = 18,

        /// <summary>
        /// 左后车毂
        /// </summary>
        [Description("左后车毂")]
        LeftBackRim = 19,

        /// <summary>
        /// 右后车毂
        /// </summary>
        [Description("右后车毂")]
        RightBackRim = 20,

        /// <summary>
        /// 发动机表面
        /// </summary>
        [Description("发动机表面")]
        EnergySurface = 21,

        /// <summary>
        /// 防冻液状态
        /// </summary>
        [Description("防冻液状态")]
        AntifreezingSolution = 22,

        /// <summary>
        /// 刹车油含水量
        /// </summary>
        [Description("刹车油含水量")]
        BrakeOilWater = 23,

        /// <summary>
        /// 刹车油液位
        /// </summary>
        [Description("刹车油液位")]
        BrakeOilLevelStatus = 24,

        /// <summary>
        /// 机油液位
        /// </summary>
        [Description("机油液位")]
        EngineOilLevel = 25,

        /// <summary>
        /// 机油形态
        /// </summary>
        [Description("机油形态")]
        EngineOilShape = 26,

        /// <summary>
        /// 保养到期
        /// </summary>
        [Description("保养到期")]
        MaintainExpired = 27,

        /// <summary>
        /// 前雨刮
        /// </summary>
        [Description("前雨刮")]
        FrontWiper = 28,

        /// <summary>
        /// 后雨刮
        /// </summary>
        [Description("后雨刮")]
        BackWiper = 29,

        /// <summary>
        /// 玻璃氧化检测
        /// </summary>
        [Description("玻璃氧化检测")]
        GlassOxidationDetection = 30,

        /// <summary>
        /// 玻璃破裂
        /// </summary>
        [Description("玻璃破裂")]
        GlassRupture = 31,

        /// <summary>
        /// 隔热膜
        /// </summary>
        [Description("隔热膜")]
        HeatInsulatingFilm = 32,

        /// <summary>
        /// 车内异味
        /// </summary>
        [Description("车内异味")]
        PeculiarSmellStatus = 33,

        /// <summary>
        /// 座椅
        /// </summary>
        [Description("座椅")]
        Seat = 34,

        /// <summary>
        /// 仪表盘
        /// </summary>
        [Description("仪表盘")]
        DashBoard = 35,

        /// <summary>
        /// 门内侧
        /// </summary>
        [Description("门内侧")]
        MedialPortal = 36,

        /// <summary>
        /// 橡塑件
        /// </summary>
        [Description("橡塑件")]
        PlasticPart = 37,

        /// <summary>
        /// 镀铬件
        /// </summary>
        [Description("镀铬件")]
        ChromiumPlatedPart = 38,

        /// <summary>
        /// 脚垫
        /// </summary>
        [Description("脚垫")]
        FootPad = 39,

        /// <summary>
        /// 座垫
        /// </summary>
        [Description("座垫")]
        SeatCushion = 40,
    }

    #region enum

    /// <summary>
    /// 发动机表面状态
    /// </summary>
    public enum EEnergySurfaceStatus
    {
        /// <summary>
        /// 正常
        /// </summary>
        [Description("正常")]
        Normal = 0,

        /// <summary>
        /// 不正常
        /// </summary>
        [Description("不正常")]
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
        [Description("正常")]
        Normal = 0,

        /// <summary>
        /// 不正常
        /// </summary>
        [Description("不正常")]
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
        [Description("百分三")]
        ThreePercentage = 0,

        /// <summary>
        /// 百分十
        /// </summary>
        [Description("百分十")]
        TenPercentage = 1,
    }

    /// <summary>
    /// 刹车油液位
    /// </summary>
    public enum EBrakeOilLevelStatus
    {
        /// <summary>
        /// 正常液位
        /// </summary>
        [Description("正常液位")]
        Normal = 0,

        /// <summary>
        /// 液位过低
        /// </summary>
        [Description("液位过低")]
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
        [Description("正常液位")]
        Normal = 0,

        /// <summary>
        /// 液位过低
        /// </summary>
        [Description("液位过低")]
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
        [Description("正常")]
        Normal = 0,

        /// <summary>
        /// 不正常
        /// </summary>
        [Description("不正常")]
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
        [Description("正常")]
        Normal = 0,

        /// <summary>
        /// 过期
        /// </summary>
        [Description("过期")]
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
        [Description("正常")]
        Normal = 0,

        /// <summary>
        /// 不正常
        /// </summary>
        [Description("不正常")]
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
        [Description("正常")]
        Normal = 0,

        /// <summary>
        /// 不正常
        /// </summary>
        [Description("不正常")]
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
        [Description("正常")]
        Normal = 0,

        /// <summary>
        /// 不正常
        /// </summary>
        [Description("不正常")]
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
        [Description("正常")]
        Normal = 0,

        /// <summary>
        /// 不正常
        /// </summary>
        [Description("不正常")]
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
        [Description("正常")]
        Normal = 0,

        /// <summary>
        /// 不正常
        /// </summary>
        [Description("不正常")]
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
        [Description("正常")]
        Normal = 0,

        /// <summary>
        /// 不正常
        /// </summary>
        [Description("不正常")]
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
        [Description("正常")]
        Normal = 0,

        /// <summary>
        /// 不正常
        /// </summary>
        [Description("不正常")]
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
        [Description("正常")]
        Normal = 0,

        /// <summary>
        /// 不正常
        /// </summary>
        [Description("不正常")]
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
        [Description("正常")]
        Normal = 0,

        /// <summary>
        /// 不正常
        /// </summary>
        [Description("不正常")]
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
        [Description("正常")]
        Normal = 0,

        /// <summary>
        /// 不正常
        /// </summary>
        [Description("不正常")]
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
        [Description("正常")]
        Normal = 0,

        /// <summary>
        /// 划痕
        /// </summary>
        [Description("划痕")]
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
        [Description("正常")]
        Normal = 0,

        /// <summary>
        /// 划痕
        /// </summary>
        [Description("划痕")]
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
        [Description("正常")]
        Normal = 0,

        /// <summary>
        /// 划痕
        /// </summary>
        [Description("划痕")]
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
        [Description("正常")]
        Normal = 0,

        /// <summary>
        /// 划痕
        /// </summary>
        [Description("划痕")]
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
        [Description("正常")]
        Normal = 0,

        /// <summary>
        /// 划痕
        /// </summary>
        [Description("划痕")]
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
        [Description("正常")]
        Normal = 0,

        /// <summary>
        /// 划痕
        /// </summary>
        [Description("划痕")]
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
        [Description("正常")]
        Normal = 0,

        /// <summary>
        /// 划痕
        /// </summary>
        [Description("划痕")]
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
        [Description("正常")]
        Normal = 0,

        /// <summary>
        /// 划痕
        /// </summary>
        [Description("划痕")]
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
        [Description("正常")]
        Normal = 0,

        /// <summary>
        /// 划痕
        /// </summary>
        [Description("划痕")]
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
        [Description("正常")]
        Normal = 0,

        /// <summary>
        /// 划痕
        /// </summary>
        [Description("划痕")]
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
        [Description("正常")]
        Normal = 0,

        /// <summary>
        /// 划痕
        /// </summary>
        [Description("划痕")]
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
        [Description("正常")]
        Normal = 0,

        /// <summary>
        /// 划痕
        /// </summary>
        [Description("划痕")]
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
        [Description("正常")]
        Normal = 0,

        /// <summary>
        /// 不正常
        /// </summary>
        [Description("不正常")]
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
        [Description("正常")]
        Normal = 0,

        /// <summary>
        /// 破裂
        /// </summary>
        [Description("破裂")]
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
        [Description("正常")]
        Normal = 0,

        /// <summary>
        /// 鼓包
        /// </summary>
        [Description("鼓包")]
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
        [Description("正常")]
        Normal = 0,

        /// <summary>
        /// 不正常
        /// </summary>
        [Description("不正常")]
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
        [Description("正常")]
        Normal = 0,

        /// <summary>
        /// 破损
        /// </summary>
        [Description("破损")]
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
        [Description("正常")]
        Normal = 0,

        /// <summary>
        /// 破损
        /// </summary>
        [Description("破损")]
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
        [Description("正常")]
        Normal = 0,

        /// <summary>
        /// 脏污
        /// </summary>
        [Description("脏污")]
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
        [Description("正常")]
        Normal = 0,

        /// <summary>
        /// 氧化发白
        /// </summary>
        [Description("氧化发白")]
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
        [Description("正常")]
        Normal = 0,

        /// <summary>
        /// 不正常
        /// </summary>
        [Description("不正常")]
        OffNormal = 1,
    }

    #endregion

}
