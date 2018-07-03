using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.Shop.Domain.Entities;
using VVCar.Shop.Domain.Filters;
using VVCar.Shop.Domain.Services;
using YEF.Core;
using YEF.Core.Data;
using YEF.Core.Domain;

namespace VVCar.Shop.Services.DomainServices
{
    /// <summary>
    /// 车比特会员引擎 领域服务
    /// </summary>
    public class CarBitCoinMemberEngineService : DomainServiceBase<IRepository<CarBitCoinMemberEngine>, CarBitCoinMemberEngine, Guid>, ICarBitCoinMemberEngineService
    {
        public CarBitCoinMemberEngineService()
        {
        }

        public override CarBitCoinMemberEngine Add(CarBitCoinMemberEngine entity)
        {
            if (entity == null)
                return null;
            entity.ID = Util.NewID();
            entity.CreatedDate = DateTime.Now;
            return base.Add(entity);
        }

        public IEnumerable<CarBitCoinMemberEngine> Search(CarBitCoinMemberEngineFilter filter, out int totalCount)
        {
            var queryable = Repository.GetQueryable(false);
            if (filter.CarBitCoinMemberID.HasValue)
                queryable = queryable.Where(t => t.CarBitCoinMemberID == filter.CarBitCoinMemberID.Value);
            totalCount = queryable.Count();
            if (filter.Start.HasValue && filter.Limit.HasValue)
                queryable = queryable.OrderByDescending(t => t.CreatedDate).Skip(filter.Start.Value).Take(filter.Limit.Value);
            return queryable.OrderByDescending(t => t.CreatedDate).ToArray();
        }
    }
}
