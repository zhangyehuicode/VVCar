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
    public interface ICarBitCoinMemberEngineService : IDomainService<IRepository<CarBitCoinMemberEngine>, CarBitCoinMemberEngine, Guid>
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        IEnumerable<CarBitCoinMemberEngine> Search(CarBitCoinMemberEngineFilter filter, out int totalCount);
    }
}
