using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.Shop.Domain.Entities;
using VVCar.Shop.Domain.Filters;
using YEF.Core.Data;
using YEF.Core.Domain;

namespace VVCar.Shop.Domain.Services
{
    public interface ICarBitCoinDistributionService : IDomainService<IRepository<CarBitCoinDistribution>, CarBitCoinDistribution, Guid>
    {
        /// <summary>
        /// 转换车比特到会员
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool CarBitCoinTransform(Guid id);

        /// <summary>
        /// 分配车比特
        /// </summary>
        /// <returns></returns>
        bool DistributionCarBitCoin(Guid? cbcmemberId);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        IEnumerable<CarBitCoinDistribution> Search(CarBitCoinDistributionFilter filter, out int totalCount);
    }
}
