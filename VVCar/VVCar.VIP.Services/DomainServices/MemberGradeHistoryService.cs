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
    /// 会员等级变更记录 领域服务实现
    /// </summary>
    public class MemberGradeHistoryService : DomainServiceBase<IRepository<MemberGradeHistory>, MemberGradeHistory, Guid>, IMemberGradeHistoryService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MemberGradeHistoryService"/> class.
        /// </summary>
        public MemberGradeHistoryService()
        {
        }

        #region methods

        public override MemberGradeHistory Add(MemberGradeHistory entity)
        {
            if (entity == null)
                return null;
            entity.ID = Util.NewID();
            entity.CreatedDate = DateTime.Now;
            return base.Add(entity);
        }

        #endregion
    }
}
