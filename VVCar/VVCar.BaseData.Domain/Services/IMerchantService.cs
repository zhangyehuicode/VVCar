using System;
using System.Collections.Generic;
using VVCar.BaseData.Domain.Filters;
using YEF.Core.Data;
using YEF.Core.Domain;

namespace VVCar.BaseData.Domain.Services
{
    /// <summary>
    /// 商户领域服务接口
    /// </summary>
    public interface IMerchantService : IDomainService<IRepository<Merchant>, Merchant, Guid>
    {
        /// <summary>
        /// 激活商户
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        bool ActivateMerchant(Guid[] ids);

        /// <summary>
        /// 冻结商户
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        bool FreezeMerchant(Guid[] ids);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        IEnumerable<Merchant> Search(MerchantFilter filter, out int totalCount);
    }
}
