using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.VIP.Domain.Entities;
using VVCar.VIP.Domain.Services;
using YEF.Core;
using YEF.Core.Data;
using YEF.Core.Domain;

namespace VVCar.VIP.Services.DomainServices
{
    /// <summary>
    /// 核销记录 领域服务
    /// </summary>
    public partial class VerificationRecordService : DomainServiceBase<IRepository<VerificationRecord>, VerificationRecord, Guid>, IVerificationRecordService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VerificationRecordService"/> class.
        /// </summary>
        public VerificationRecordService()
        {
        }

        public override VerificationRecord Add(VerificationRecord entity)
        {
            entity.ID = Util.NewID();
            entity.VerificationDate = DateTime.Now;
            return base.Add(entity);
        }
    }
}
