﻿using System;
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
    public class PickUpOrderService : DomainServiceBase<IRepository<PickUpOrder>, PickUpOrder, Guid>, IPickUpOrderService
    {
        public PickUpOrderService()
        {
        }

        public override PickUpOrder Add(PickUpOrder entity)
        {
            if (entity == null || entity.PickUpOrderItemList == null || entity.PickUpOrderItemList.Count < 1)
                return null;
            entity.ID = Util.NewID();
            entity.CreatedDate = DateTime.Now;
            entity.MerchantID = AppContext.CurrentSession.MerchantID;
            entity.PickUpOrderItemList.ForEach(t =>
            {
                t.ID = Util.NewID();
                t.PickUpOrderID = entity.ID;
                t.MerchantID = entity.MerchantID;
            });
            RecountMoney(entity);
            return base.Add(entity);
        }

        public void RecountMoney(PickUpOrder entity)
        {
            if (entity == null)
                return;
            if (entity.PickUpOrderItemList == null || entity.PickUpOrderItemList.Count < 1)
                entity.Money = 0;
            decimal totalMoney = 0;
            entity.PickUpOrderItemList.ForEach(t =>
            {
                t.Money = t.Quantity * t.PriceSale;
                totalMoney += t.Money;
            });
            entity.Money = totalMoney;
        }

        public IEnumerable<PickUpOrder> Search(PickUpOrderFilter filter, ref int totalCount)
        {
            var queryable = Repository.GetInclude(t => t.PickUpOrderItemList, false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID);
            if (!string.IsNullOrEmpty(filter.PlateNumber))
                queryable = queryable.Where(t => t.PlateNumber == filter.PlateNumber);
            if (filter.CreatedDate.HasValue)
            {
                var date = filter.CreatedDate.Value.Date;
                var nextdate = filter.CreatedDate.Value.Date.AddDays(1);
                queryable = queryable.Where(t => t.CreatedDate >= date && t.CreatedDate < nextdate);
            }
            totalCount = queryable.Count();
            if (filter.Start.HasValue && filter.Limit.HasValue)
                queryable = queryable.OrderByDescending(t => t.CreatedDate).Skip(filter.Start.Value).Take(filter.Limit.Value);
            return queryable.OrderByDescending(t => t.CreatedDate).ToArray();
        }
    }
}
