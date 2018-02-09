using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.VIP.Domain.Entities;
using YEF.Core.Data;
using YEF.Core.Domain;

namespace VVCar.VIP.Domain.Services
{
    /// <summary>
    /// 访问记录统计 领域服务接口
    /// </summary>
    public partial interface IVisitRecordService : IDomainService<IRepository<VisitRecord>, VisitRecord, Guid>
    {
        /// <summary>
        /// 增加访问记录
        /// </summary>
        /// <param name="identifyID"></param>
        void AddVisitRecord(Guid identifyID);
    }
}
