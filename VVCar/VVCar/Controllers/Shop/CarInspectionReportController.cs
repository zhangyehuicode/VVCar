using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using VVCar.Shop.Domain.Entities;
using VVCar.Shop.Domain.Filters;
using VVCar.Shop.Domain.Services;
using YEF.Core.Dtos;

namespace VVCar.Controllers.Shop
{
    /// <summary>
    /// 车检报告
    /// </summary>
    [RoutePrefix("api/CarInspectionReport")]
    public class CarInspectionReportController : BaseApiController
    {
        public CarInspectionReportController(ICarInspectionReportService carInspectionReportService)
        {
            CarInspectionReportService = carInspectionReportService;
        }

        ICarInspectionReportService CarInspectionReportService { get; set; }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost, AllowAnonymous]
        public JsonActionResult<CarInspectionReport> Add(CarInspectionReport entity)
        {
            return SafeExecute(() =>
            {
                return CarInspectionReportService.Add(entity);
            });
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPut, AllowAnonymous]
        public JsonActionResult<bool> Update(CarInspectionReport entity)
        {
            return SafeExecute(() =>
            {
                return CarInspectionReportService.Update(entity);
            });
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet, AllowAnonymous]
        public PagedActionResult<CarInspectionReport> Search(CarInspectionReportFilter filter)
        {
            return SafeGetPagedData<CarInspectionReport>((result) =>
            {
                var totalCount = 0;
                var data = CarInspectionReportService.Search(filter, out totalCount);
                result.Data = data;
                result.TotalCount = totalCount;
            });
        }
    }
}