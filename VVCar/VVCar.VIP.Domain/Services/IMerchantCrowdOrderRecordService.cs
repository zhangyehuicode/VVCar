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
    /// <summary>
    /// 拼单记录领域服务接口
    /// </summary>
    public interface IMerchantCrowdOrderRecordService : IDomainService<IRepository<MerchantCrowdOrderRecord>, MerchantCrowdOrderRecord, Guid>
    {
        /// <summary>
        /// 新增拼单子项
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        MerchantCrowdOrderRecordDto AddMerchantCrowdOrderRecordItem(MerchantCrowdOrderRecordItem entity);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        IEnumerable<MerchantCrowdOrderRecordDto> Search(MerchantCrowdOrderRecordFilter filter, out int totalCount);

        /// <summary>
        /// 加入拼单
        /// </summary>
        /// <param name="merchantCrowdOrderRecordID"></param>
        /// <param name="memberID"></param>
        /// <returns></returns>
        bool JoinMerchantCrowdOrderRecord(Guid merchantCrowdOrderRecordID, Guid memberID);
    }
}
