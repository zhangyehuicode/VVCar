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
    public class StockRecordService : DomainServiceBase<IRepository<StockRecord>, StockRecord, Guid>, IStockRecordService
    {
        public StockRecordService()
        {
        }

        public override StockRecord Add(StockRecord entity)
        {
            if (entity == null)
                return null;
            entity.ID = Util.NewID();
            entity.CreatedDate = DateTime.Now;
            entity.MerchantID = AppContext.CurrentSession.MerchantID;
            return base.Add(entity);
        }

        public IEnumerable<StockRecord> Search(StockRecordFilter filter, ref int totalCount)
        {
            var queryable = Repository.GetQueryable(false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID);
            if (!string.IsNullOrEmpty(filter.ProductCode))
                queryable = queryable.Where(t => t.Product.Code.Contains(filter.ProductCode));
            if (!string.IsNullOrEmpty(filter.ProductName))
                queryable = queryable.Where(t => t.Product.Name.Contains(filter.ProductName));
            if (!string.IsNullOrEmpty(filter.StaffName))
                queryable = queryable.Where(t => t.StaffName.Contains(filter.StaffName));
            if (!string.IsNullOrEmpty(filter.OrderCode))
                queryable = queryable.Where(t => t.OrderID.HasValue && t.Order.Code.Contains(filter.OrderCode));
            if (filter.StockRecordType.HasValue)
                queryable = queryable.Where(t => t.StockRecordType == filter.StockRecordType.Value);
            totalCount = queryable.Count();
            if (filter.Start.HasValue && filter.Limit.HasValue)
                queryable = queryable.OrderByDescending(t => t.CreatedDate).Skip(filter.Start.Value).Take(filter.Limit.Value);
            return queryable.ToList();
        }
    }
}
