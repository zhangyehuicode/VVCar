using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.VIP.Domain.Dtos;
using VVCar.VIP.Domain.Entities;
using VVCar.VIP.Domain.Filters;
using YEF.Core.Data;
using YEF.Core.Domain;

namespace VVCar.VIP.Domain.Services
{
    public interface ICrowdOrderRecordService : IDomainService<IRepository<CrowdOrderRecord>, CrowdOrderRecord, Guid>
    {
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        IEnumerable<CrowdOrderRecordDto> Search(CrowdOrderRecordFilter filter, out int totalCount);

        /// <summary>
        /// 加入拼单
        /// </summary>
        /// <param name="crowdOrderRecordID"></param>
        /// <param name="carBitCoinMemberID"></param>
        /// <returns></returns>
        bool JoinCrowdOrderRecord(Guid crowdOrderRecordID, Guid carBitCoinMemberID);
    }
}
