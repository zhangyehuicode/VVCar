using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEF.Core;
using VVCar.VIP.Domain.Entities;
using VVCar.VIP.Domain.Services;
using YEF.Core.Data;
using YEF.Core.Domain;

namespace VVCar.VIP.Services.DomainServices
{
    /// <summary>
    /// 访问记录统计 领域服务
    /// </summary>
    public class VisitRecordService : DomainServiceBase<IRepository<VisitRecord>, VisitRecord, Guid>, IVisitRecordService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VisitRecordService"/> class.
        /// </summary>
        public VisitRecordService()
        {
        }

        #region methods

        public override VisitRecord Add(VisitRecord entity)
        {
            entity.ID = Util.NewID();
            return base.Add(entity);
        }

        /// <summary>
        /// 增加访问记录
        /// </summary>
        /// <param name="identifyID"></param>
        public void AddVisitRecord(Guid identifyID)
        {
            var visitDate = DateTime.Today;
            var visitStatistics = Repository.Get(t => t.IdentifyID == identifyID && t.VisitDate == visitDate);
            if (visitStatistics == null)
            {
                visitStatistics = new VisitRecord()
                {
                    IdentifyID = identifyID,
                    VisitDate = visitDate,
                    PV = 1
                };
                Add(visitStatistics);
            }
            else
            {
                visitStatistics.PV++;
                Repository.Update(visitStatistics);
            }
        }

        #endregion
    }
}
