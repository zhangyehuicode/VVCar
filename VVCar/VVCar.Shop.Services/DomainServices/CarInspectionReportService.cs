using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.BaseData.Domain.Entities;
using VVCar.BaseData.Domain.Services;
using VVCar.Shop.Domain.Entities;
using VVCar.Shop.Domain.Filters;
using VVCar.Shop.Domain.Services;
using YEF.Core;
using YEF.Core.Data;
using YEF.Core.Domain;

namespace VVCar.Shop.Services.DomainServices
{
    /// <summary>
    /// 车检报告领域服务
    /// </summary>
    public class CarInspectionReportService : DomainServiceBase<IRepository<CarInspectionReport>, CarInspectionReport, Guid>, ICarInspectionReportService
    {
        public CarInspectionReportService()
        {
        }

        #region properties

        IRepository<MakeCodeRule> MakeCodeRuleRepo { get => UnitOfWork.GetRepository<IRepository<MakeCodeRule>>(); }

        #endregion

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override CarInspectionReport Add(CarInspectionReport entity)
        {
            if (entity == null)
                return null;
            entity.ID = Util.NewID();
            if (string.IsNullOrEmpty(entity.Code))
                entity.Code = GetCarInspectionReportCode();
            var existCode = Repository.Exists(t => t.Code == entity.Code);
            if (existCode)
                throw new DomainException($"创建车检报告失败，订单号{entity.Code}已存在");
            entity.CreatedDate = DateTime.Now;
            entity.CreatedUserID = AppContext.CurrentSession.UserID;
            entity.CreatedUser = AppContext.CurrentSession.UserName;
            return base.Add(entity);
        }

        /// <summary>
        /// 生成车检报告编号
        /// </summary>
        /// <returns></returns>
        public string GetCarInspectionReportCode()
        {
            var newCarInspectionReportCode = string.Empty;
            var existCode = false;
            var makeCodeRuleService = ServiceLocator.Instance.GetService<IMakeCodeRuleService>();
            var entity = Repository.GetQueryable(false).OrderByDescending(t => t.CreatedDate).FirstOrDefault();
            if(entity != null && entity.CreatedDate.Date != DateTime.Now.Date)
            {
                var rule = MakeCodeRuleRepo.GetQueryable().Where(t => t.Code == "CarInspectionReport" && t.IsAvailable).FirstOrDefault();
                if (rule != null)
                {
                    rule.CurrentValue = 0;
                    MakeCodeRuleRepo.Update(rule);
                }
            }
            do
            {
                newCarInspectionReportCode = makeCodeRuleService.GenerateCode("CarInspectionReport", DateTime.Now);
                existCode = Repository.Exists(t => t.Code == newCarInspectionReportCode);
            } while (existCode);
            return newCarInspectionReportCode;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override bool Update(CarInspectionReport entity)
        {
            if (entity == null)
                return false;
            var carInspectionReport = Repository.GetByKey(entity.ID);
            if (carInspectionReport == null)
                throw new DomainException("更新的报告不存在");
            carInspectionReport.PlateNumber = entity.PlateNumber;
            carInspectionReport.DepartmentName = entity.DepartmentName;
            carInspectionReport.Brand = entity.Brand;
            carInspectionReport.CarType = entity.CarType;
            carInspectionReport.RegistDate = entity.RegistDate;
            carInspectionReport.Mileage = entity.Mileage;
            carInspectionReport.Inspector = entity.Inspector;
            carInspectionReport.EnergySurfaceStatus = entity.EnergySurfaceStatus;
            carInspectionReport.AntifreezingSolutionStatus = entity.AntifreezingSolutionStatus;
            carInspectionReport.BrakeOilWaterStatus = entity.BrakeOilWaterStatus;
            carInspectionReport.BrakeOilLevelStatus = entity.BrakeOilLevelStatus;
            carInspectionReport.EngineOilLevelStatus = entity.EngineOilLevelStatus;
            carInspectionReport.EngineOilShapeStatus = entity.EngineOilShapeStatus;
            carInspectionReport.MaintainExpiredStatus = entity.MaintainExpiredStatus;
            carInspectionReport.LeftFrontTireStatus = carInspectionReport.LeftFrontTireStatus;
            carInspectionReport.RightFrontTireStatus = entity.RightFrontTireStatus;
            carInspectionReport.LeftBackTireStatus = entity.LeftBackTireStatus;
            carInspectionReport.RightBackTireStatus = entity.RightBackTireStatus;
            carInspectionReport.LeftFrontRimStatus = entity.LeftFrontRimStatus;
            carInspectionReport.RightFrontRimStatus = entity.RightFrontRimStatus;
            carInspectionReport.LeftBackRimStatus = entity.LeftBackRimStatus;
            carInspectionReport.RightBackRimStatus = entity.RightBackRimStatus;
            carInspectionReport.FrontWiperStatus = entity.FrontWiperStatus;
            carInspectionReport.BackWiperStatus = entity.BackWiperStatus;
            carInspectionReport.FrontGuardLacquerStatus = entity.FrontGuardLacquerStatus;
            carInspectionReport.BackGuardLacquerStatus = entity.BackGuardLacquerStatus;
            carInspectionReport.FrontHoodLacquerStatus = entity.FrontHoodLacquerStatus;
            carInspectionReport.TrunkLidLacquerStatus = entity.TrunkLidLacquerStatus;
            carInspectionReport.LeftFrontPageLacquerStatus = entity.LeftFrontPageLacquerStatus;
            carInspectionReport.RightFrontPageLacquerStatus = entity.RightFrontPageLacquerStatus;
            carInspectionReport.LeftBackPageLacquerStatus = entity.LeftBackPageLacquerStatus;
            carInspectionReport.RightBackPageLacquerStatus = entity.RightBackPageLacquerStatus;
            carInspectionReport.LeftFrontDoorLacquerStatus = entity.LeftFrontDoorLacquerStatus;
            carInspectionReport.RightFrontDoorLacquerStatus = entity.RightFrontDoorLacquerStatus;
            carInspectionReport.LeftBackDoorLacquerStatus = entity.LeftBackDoorLacquerStatus;
            carInspectionReport.RightBackDoorLacquerStatus = entity.RightBackDoorLacquerStatus;
            carInspectionReport.GlassOxidationDetectionStatus = entity.GlassOxidationDetectionStatus;
            carInspectionReport.GlassRuptureStatus = entity.GlassRuptureStatus;
            carInspectionReport.HeatInsulatingFilmStatus = entity.HeatInsulatingFilmStatus;
            carInspectionReport.PeculiarSmellStatus = entity.PeculiarSmellStatus;
            carInspectionReport.SeatStatus = entity.SeatStatus;
            carInspectionReport.DashBoardStatus = entity.DashBoardStatus;
            carInspectionReport.MedialPortalStatus = entity.MedialPortalStatus;
            carInspectionReport.PlasticPartStatus = entity.PlasticPartStatus;
            carInspectionReport.ChromiumPlatedPartStatus = entity.ChromiumPlatedPartStatus;
            carInspectionReport.LastUpdateDate = DateTime.Now;
            carInspectionReport.LastUpdateUser = AppContext.CurrentSession.UserName;
            carInspectionReport.LastUpdateUserID = AppContext.CurrentSession.UserID;
            return base.Update(entity);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public IEnumerable<CarInspectionReport> Search(CarInspectionReportFilter filter, out int totalCount)
        {
            var queryable = Repository.GetQueryable(false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID);
            if (!string.IsNullOrEmpty(filter.PlateNumber))
                queryable = queryable.Where(t => t.PlateNumber.Contains(filter.PlateNumber));
            if (filter.PickUpOrderID.HasValue)
                queryable = queryable.Where(t => t.PickUpOrderID == filter.PickUpOrderID);
            if (filter.MemberID.HasValue)
                queryable = queryable.Where(t => t.MemberID == filter.MemberID);
            totalCount = queryable.Count();
            if (filter.Start.HasValue && filter.Limit.HasValue)
                queryable = queryable.OrderByDescending(t => t.CreatedDate).Skip(filter.Start.Value).Take(filter.Limit.Value);
            return queryable.ToArray();
        }
    }
}
