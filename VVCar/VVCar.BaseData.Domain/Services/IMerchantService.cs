using System;
using System.Collections.Generic;
using YEF.Core.Data;
using YEF.Core.Domain;
using VVCar.BaseData.Domain.Entities;
using YEF.Core.Dtos;
using VVCar.BaseData.Domain.Filters;
using VVCar.BaseData.Domain.Dtos;

namespace VVCar.BaseData.Domain.Services
{
    /// <summary>
    /// 商户领域服务接口
    /// </summary>
    public interface IMerchantService : IDomainService<IRepository<Merchant>, Merchant, Guid>
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        IEnumerable<Merchant> Search(MerchantFilter filter, out int totalCount);
    }
}
