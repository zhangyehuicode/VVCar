using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.BaseData.Domain.Entities;
using VVCar.BaseData.Domain.Services;
using VVCar.BaseData.Services;
using VVCar.Shop.Domain.Dtos;
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

        IRepository<CarInspectionDetailsImg> CarInspectionDetailsImgRepo { get => UnitOfWork.GetRepository<IRepository<CarInspectionDetailsImg>>(); }

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
            if (string.IsNullOrEmpty(entity.Code))
                entity.Code = GetCarInspectionReportCode();
            var existCode = Repository.Exists(t => t.Code == entity.Code);
            if (existCode)
                throw new DomainException($"创建车检报告失败，车检号{entity.Code}已存在");
            entity.ID = Util.NewID();
            entity.CreatedDate = DateTime.Now;
            entity.CreatedUserID = AppContext.CurrentSession.UserID;
            entity.CreatedUser = AppContext.CurrentSession.UserName;
            entity.MerchantID = AppContext.CurrentSession.MerchantID;
            if (!entity.DepartmentID.HasValue)
            {
                entity.DepartmentID = AppContext.CurrentSession.DepartmentID;
                entity.DepartmentName = AppContext.CurrentSession.DepartmentName;
            }
            if (!entity.InspectorID.HasValue)
            {
                entity.InspectorID = AppContext.CurrentSession.UserID;
                entity.Inspector = AppContext.CurrentSession.UserName;
            }
            entity.CarInspectionDetailsList.ForEach(t =>
            {
                t.ID = Util.NewID();
                t.CarInspectionReportID = entity.ID;
                t.ImgList.ForEach(item =>
                {
                    item.ID = Util.NewID();
                    item.CarInspectionReportID = entity.ID;
                    item.CarInspectionDetailsID = t.ID;
                });
            });
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
            if (entity != null && entity.CreatedDate.Date != DateTime.Now.Date)
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
                throw new DomainException("数据不存在");
            carInspectionReport.PlateNumber = entity.PlateNumber;
            carInspectionReport.DepartmentName = entity.DepartmentName;
            carInspectionReport.Brand = entity.Brand;
            carInspectionReport.CarType = entity.CarType;
            carInspectionReport.RegistDate = entity.RegistDate;
            carInspectionReport.Mileage = entity.Mileage;
            carInspectionReport.Inspector = entity.Inspector;
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
        public IEnumerable<CarInspectionReportDto> Search(CarInspectionReportFilter filter, out int totalCount)
        {
            var queryable = Repository.GetInclude(t => t.CarInspectionDetailsList, false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID);
            if (!string.IsNullOrEmpty(filter.PlateNumber))
                queryable = queryable.Where(t => t.PlateNumber.Contains(filter.PlateNumber));
            if (filter.PickUpOrderID.HasValue)
                queryable = queryable.Where(t => t.PickUpOrderID == filter.PickUpOrderID);
            if (filter.MemberID.HasValue)
                queryable = queryable.Where(t => t.MemberID == filter.MemberID.Value);
            if (!string.IsNullOrEmpty(filter.Code))
                queryable = queryable.Where(t => t.Code.Contains(filter.Code));
            totalCount = queryable.Count();
            if (filter.Start.HasValue && filter.Limit.HasValue)
                queryable = queryable.OrderByDescending(t => t.CreatedDate).Skip(filter.Start.Value).Take(filter.Limit.Value);
            var result = queryable.ToList().MapTo<List<CarInspectionReportDto>>();
            result.ForEach(t =>
            {
                IEnumerable<ECarInspectionPart> parts = t.CarInspectionDetailsList.Select(m => m.Part).Distinct();
                foreach (ECarInspectionPart part in Enum.GetValues(typeof(ECarInspectionPart)))
                {
                    if (!parts.Contains(part) && part != ECarInspectionPart.None)
                    {
                        t.CarInspectionDetailsList.Add(new CarInspectionDetailsDto
                        {
                            Part = part,
                            Status = ECarInspectionStatus.Normal,
                            Explain = string.Empty,
                            ImgList = new List<CarInspectionDetailsImg>(),
                        });
                    }
                }
                var carInspectionDetailsImgList = CarInspectionDetailsImgRepo.GetQueryable(false).Where(m => m.CarInspectionReportID == t.ID).ToList();
                t.CarInspectionDetailsList.ForEach(d =>
                {
                    var imglist = carInspectionDetailsImgList.Where(m => m.CarInspectionDetailsID == d.ID).ToList();
                    d.ImgList = imglist;
                });
            });
            return result.OrderByDescending(t => t.CreatedDate).ToList();
            //var result = new List<CarInspectionReportDto>();
            //queryList.ForEach(t =>
            //{
            //    var carInspectionReportDto = new CarInspectionReportDto();
            //    carInspectionReportDto = t.MapTo<CarInspectionReportDto>();
            //    carInspectionReportDto.EnergySurfaceStatus = t.EnergySurfaceStatus.GetDescription();
            //    carInspectionReportDto.AntifreezingSolutionStatus = t.AntifreezingSolutionStatus.GetDescription();
            //    carInspectionReportDto.BrakeOilWaterStatus = t.BrakeOilWaterStatus.GetDescription();
            //    carInspectionReportDto.BrakeOilLevelStatus = t.BrakeOilLevelStatus.GetDescription();
            //    carInspectionReportDto.EngineOilLevelStatus = t.EngineOilLevelStatus.GetDescription();
            //    carInspectionReportDto.EngineOilShapeStatus = t.EngineOilShapeStatus.GetDescription();
            //    carInspectionReportDto.MaintainExpiredStatus = t.MaintainExpiredStatus.GetDescription();
            //    carInspectionReportDto.LeftFrontTireStatus = t.LeftFrontTireStatus.GetDescription();
            //    carInspectionReportDto.RightFrontTireStatus = t.RightFrontTireStatus.GetDescription();
            //    carInspectionReportDto.LeftBackTireStatus = t.LeftBackTireStatus.GetDescription();
            //    carInspectionReportDto.RightBackTireStatus = t.RightBackTireStatus.GetDescription();
            //    carInspectionReportDto.LeftFrontRimStatus = t.LeftFrontRimStatus.GetDescription();
            //    carInspectionReportDto.RightFrontRimStatus = t.RightFrontRimStatus.GetDescription();
            //    carInspectionReportDto.LeftBackRimStatus = t.LeftBackRimStatus.GetDescription();
            //    carInspectionReportDto.RightBackRimStatus = t.RightBackRimStatus.GetDescription();
            //    carInspectionReportDto.FrontWiperStatus = t.FrontWiperStatus.GetDescription();
            //    carInspectionReportDto.BackWiperStatus = t.BackWiperStatus.GetDescription();
            //    carInspectionReportDto.FrontGuardLacquerStatus = t.FrontGuardLacquerStatus.GetDescription();
            //    carInspectionReportDto.BackGuardLacquerStatus = t.BackGuardLacquerStatus.GetDescription();
            //    carInspectionReportDto.FrontHoodLacquerStatus = t.FrontHoodLacquerStatus.GetDescription();
            //    carInspectionReportDto.TrunkLidLacquerStatus = t.TrunkLidLacquerStatus.GetDescription();
            //    carInspectionReportDto.LeftFrontPageLacquerStatus = t.LeftFrontPageLacquerStatus.GetDescription();
            //    carInspectionReportDto.RightFrontPageLacquerStatus = t.RightFrontPageLacquerStatus.GetDescription();
            //    carInspectionReportDto.LeftBackPageLacquerStatus = t.LeftBackPageLacquerStatus.GetDescription();
            //    carInspectionReportDto.RightBackPageLacquerStatus = t.RightBackPageLacquerStatus.GetDescription();
            //    carInspectionReportDto.LeftFrontDoorLacquerStatus = t.LeftFrontDoorLacquerStatus.GetDescription();
            //    carInspectionReportDto.RightFrontDoorLacquerStatus = t.RightFrontDoorLacquerStatus.GetDescription();
            //    carInspectionReportDto.LeftBackDoorLacquerStatus = t.LeftBackDoorLacquerStatus.GetDescription();
            //    carInspectionReportDto.RightBackDoorLacquerStatus = t.RightBackDoorLacquerStatus.GetDescription();
            //    carInspectionReportDto.GlassOxidationDetectionStatus = t.GlassOxidationDetectionStatus.GetDescription();
            //    carInspectionReportDto.GlassRuptureStatus = t.GlassRuptureStatus.GetDescription();
            //    carInspectionReportDto.HeatInsulatingFilmStatus = t.HeatInsulatingFilmStatus.GetDescription();
            //    carInspectionReportDto.PeculiarSmellStatus = t.PeculiarSmellStatus.GetDescription();
            //    carInspectionReportDto.SeatStatus = t.SeatStatus.GetDescription();
            //    carInspectionReportDto.DashBoardStatus = t.DashBoardStatus.GetDescription();
            //    carInspectionReportDto.MedialPortalStatus = t.MedialPortalStatus.GetDescription();
            //    carInspectionReportDto.PlasticPartStatus = t.PlasticPartStatus.GetDescription();
            //    carInspectionReportDto.ChromiumPlatedPartStatus = t.ChromiumPlatedPartStatus.GetDescription();
            //    result.Add(carInspectionReportDto);
            //});
            //return result;
        }

        /// <summary>
        /// 获取车检部位
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CarInspectionPartInfo> GetCarInspectionPart()
        {
            var result = new List<CarInspectionPartInfo>();
            var parts = Enum.GetValues(typeof(ECarInspectionPart));
            foreach (ECarInspectionPart part in parts)
            {
                if (part != ECarInspectionPart.None)
                    result.Add(new CarInspectionPartInfo
                    {
                        Name = part.GetDescription(),
                        Part = part,
                    });
            }
            return result.OrderBy(t => t.Part);
        }
    }
}
