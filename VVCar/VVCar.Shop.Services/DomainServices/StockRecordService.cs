﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.BaseData.Services;
using VVCar.Shop.Domain.Dtos;
using VVCar.Shop.Domain.Entities;
using VVCar.Shop.Domain.Enums;
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
            entity.StockRecordType = entity.Quantity < 0 ? EStockRecordType.Out : EStockRecordType.In;
            entity.MerchantID = AppContext.CurrentSession.MerchantID;
            if (entity.Source == EStockRecordSource.Background)
            {
                entity.StaffID = AppContext.CurrentSession.UserID;
                entity.StaffName = AppContext.CurrentSession.UserName;
            }
            return base.Add(entity);
        }

        public IEnumerable<StockRecordDto> Search(StockRecordFilter filter, ref int totalCount)
        {
            var queryable = Repository.GetIncludes(false, "Product", "Product.ProductCategory").Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID);
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
            if (filter.ProductID.HasValue)
                queryable = queryable.Where(t => t.ProductID == filter.ProductID.Value);
            if (filter.CreatedDate.HasValue)
            {
                var nextday = filter.CreatedDate.Value.Date.AddDays(1);
                queryable = queryable.Where(t => t.CreatedDate >= filter.CreatedDate.Value && t.CreatedDate < nextday);
            }
            if (!string.IsNullOrEmpty(filter.NameCodeStaff))
                queryable = queryable.Where(t => t.Product.Name.Contains(filter.NameCodeStaff) || t.Product.Code.Contains(filter.NameCodeStaff) || t.StaffName.Contains(filter.NameCodeStaff));
            totalCount = queryable.Count();
            if (filter.Start.HasValue && filter.Limit.HasValue)
                queryable = queryable.OrderByDescending(t => t.CreatedDate).Skip(filter.Start.Value).Take(filter.Limit.Value);
            return queryable.OrderByDescending(t => t.CreatedDate).ToList().MapTo<List<StockRecordDto>>();
        }
    }
}