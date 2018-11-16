using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.Shop.Domain.Dtos;
using VVCar.Shop.Domain.Entities;
using VVCar.Shop.Domain.Filters;
using YEF.Core.Data;
using YEF.Core.Domain;

namespace VVCar.Shop.Domain.Services
{
    /// <summary>
    /// 车检报告领域服务接口
    /// </summary>
    public interface ICarInspectionReportService : IDomainService<IRepository<CarInspectionReport>, CarInspectionReport, Guid>
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        IEnumerable<CarInspectionReportDto> Search(CarInspectionReportFilter filter, out int totalCount);

        /// <summary>
        /// 获取车检部位
        /// </summary>
        /// <returns></returns>
        IEnumerable<CarInspectionPartInfo> GetCarInspectionPart();
    }
}
