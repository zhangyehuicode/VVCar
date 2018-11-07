using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.BaseData.Services;
using VVCar.Shop.Domain.Dtos;
using VVCar.Shop.Domain.Entities;
using VVCar.Shop.Domain.Filters;
using VVCar.Shop.Domain.Services;
using YEF.Core;
using YEF.Core.Data;
using YEF.Core.Domain;

namespace VVCar.Shop.Services.DomainServices
{
    /// <summary>
    /// 订单分红领域服务
    /// </summary>
    public class OrderDividendService : DomainServiceBase<IRepository<OrderDividend>, OrderDividend, Guid>, IOrderDividendService
    {
        public OrderDividendService()
        {
        }

        /// <summary>
        /// 结算
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool Balance(Guid[] ids)
        {
            if (ids == null || ids.Length < 1)
                throw new DomainException("参数不正确");
            var orderDividendList = this.Repository.GetQueryable(true).Where(t => ids.Contains(t.ID)).ToList();
            if (orderDividendList == null || orderDividendList.Count() < 1)
                throw new DomainException("数据不存在");
            var balancedData = orderDividendList.Exists(t => t.IsBalance == true);
            if (balancedData)
                throw new DomainException("存在已经结算过的数据");
            UnitOfWork.BeginTransaction();
            try
            {
                orderDividendList.ForEach(t =>
                {
                    t.IsBalance = true;
                    t.BalanceDate = DateTime.Now;
                    t.BalanceUserID = AppContext.CurrentSession.UserID;
                    t.BalanceUserName = AppContext.CurrentSession.UserName;
                });
                this.Repository.UpdateRange(orderDividendList);
                UnitOfWork.CommitTransaction();
                return true;
            }
            catch (Exception e)
            {
                UnitOfWork.RollbackTransaction();
                throw e;
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public IEnumerable<OrderDividendDto> Search(OrderDividendFilter filter, out int totalCount)
        {
            var queryable = Repository.GetQueryable(false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID);
            if (!string.IsNullOrEmpty(filter.Keyword))
                queryable = queryable.Where(t => t.UserCode.Contains(filter.Keyword) || t.UserName.Contains(filter.Keyword));
            if (filter.OrderType.HasValue)
                queryable = queryable.Where(t => t.OrderType == filter.OrderType);
            if (filter.IsBalance.HasValue)
                queryable = queryable.Where(t => t.IsBalance == filter.IsBalance);
            if (!string.IsNullOrEmpty(filter.TradeNo))
                queryable = queryable.Where(t => t.TradeNo.Contains(filter.TradeNo));
            if (filter.UserID.HasValue)
                queryable = queryable.Where(t => t.UserID == filter.UserID);
            if (filter.StartDate.HasValue)
                queryable = queryable.Where(t => t.CreatedDate >= filter.StartDate);
            if(filter.EndDate.HasValue)
                queryable = queryable.Where(t => t.CreatedDate < filter.EndDate);
            totalCount = queryable.Count();
            if (filter.Start.HasValue && filter.Limit.HasValue)
                queryable = queryable.OrderByDescending(t => t.CreatedDate).Skip(filter.Start.Value).Take(filter.Limit.Value);
            return queryable.MapTo<OrderDividendDto>().ToList();
        }
    }
}
