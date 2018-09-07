using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.VIP.Domain.Dtos;
using VVCar.VIP.Domain.Entities;
using VVCar.VIP.Domain.Filters;
using VVCar.VIP.Domain.Services;
using YEF.Core;
using YEF.Core.Data;
using YEF.Core.Domain;

namespace VVCar.VIP.Services.DomainServices
{
    public class StockholderDividendService : DomainServiceBase<IRepository<StockholderDividend>, StockholderDividend, Guid>, IStockholderDividendService
    {
        public StockholderDividendService()
        {
        }

        public override StockholderDividend Add(StockholderDividend entity)
        {
            if (entity == null)
                return null;
            entity.ID = Util.NewID();
            entity.CreatedDate = DateTime.Now;
            entity.MerchantID = AppContext.CurrentSession.MerchantID;
            return base.Add(entity);
        }

        public IEnumerable<StockholderDividendDto> Search(StockholderDividendFilter filter, out int totalCount)
        {
            var queryable = Repository.GetIncludes(false, "Member");
            if (filter.MemberID.HasValue)
                queryable = queryable.Where(t => t.MemberID == filter.MemberID.Value);
            if (filter.Source.HasValue)
                queryable = queryable.Where(t => t.Source == filter.Source.Value);
            if (filter.OrderType.HasValue)
                queryable = queryable.Where(t => t.OrderType == filter.OrderType.Value);
            if (!string.IsNullOrEmpty(filter.TradeNo))
                queryable = queryable.Where(t => t.TradeNo.Contains(filter.TradeNo));
            totalCount = queryable.Count();
            if (filter.Start.HasValue && filter.Limit.HasValue)
                queryable = queryable.OrderByDescending(t => t.CreatedDate).Skip(filter.Start.Value).Take(filter.Limit.Value);
            return queryable.OrderByDescending(t => t.CreatedDate).MapTo<StockholderDividendDto>().ToArray();
        }
    }
}
