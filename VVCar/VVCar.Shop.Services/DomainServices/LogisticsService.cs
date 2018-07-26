using System;
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
    public class LogisticsService : DomainServiceBase<IRepository<Logistics>, Logistics, Guid>, ILogisticsService
    {
        public LogisticsService()
        {
        }

        #region properties

        IRepository<Order> OrderRepo { get => UnitOfWork.GetRepository<IRepository<Order>>(); }

        #endregion

        public override Logistics Add(Logistics entity)
        {
            if (entity == null)
                return null;
            entity.ID = Util.NewID();
            entity.CreatedDate = DateTime.Now;
            entity.MerchantID = AppContext.CurrentSession.MerchantID;
            return base.Add(entity);
        }

        public IEnumerable<LogisticsDto> Search(LogisticsFilter filter, out int totalCount)
        {
            var queryable = Repository.GetInclude(t => t.Order, false);
            if (!string.IsNullOrEmpty(filter.OrderCode))
                queryable = queryable.Where(t => t.Order.Code.Contains(filter.OrderCode));
            if (filter.RevisitStatus.HasValue)
                queryable = queryable.Where(t => t.RevisitStatus == filter.RevisitStatus);
            if (filter.UserID.HasValue)
                queryable = queryable.Where(t => t.UserID == filter.UserID.Value);
            totalCount = queryable.Count();
            if (filter.Start.HasValue && filter.Limit.HasValue)
                queryable = queryable.OrderByDescending(t => t.CreatedDate).Skip(filter.Start.Value).Take(filter.Limit.Value);
            return queryable.OrderByDescending(t => t.CreatedDate).ToList().MapTo<List<LogisticsDto>>();
        }

        public bool Delivery(Guid id)
        {
            var logistics = Repository.GetByKey(id, false);
            if (logistics == null)
                throw new DomainException("数据不存在");
            var order = OrderRepo.GetByKey(logistics.OrderID);
            if (order == null)
                throw new DomainException("订单不存在");
            if (order.Status != EOrderStatus.PayUnshipped)
            {
                if (order.Status == EOrderStatus.Delivered)
                    throw new DomainException("订单已发货");
                if (order.Status == EOrderStatus.Finish)
                    throw new DomainException("订单已完成");
                if (order.Status == EOrderStatus.UnEnough)
                    throw new DomainException("订单付款不足");
                if (order.Status == EOrderStatus.UnPay)
                    throw new DomainException("订单未付款");
            }
            order.Status = EOrderStatus.Delivered;
            order.LastUpdatedDate = DateTime.Now;
            order.LastUpdatedUser = AppContext.CurrentSession.UserName;
            order.LastUpdatedUserID = AppContext.CurrentSession.UserID;
            UnitOfWork.BeginTransaction();
            try
            {
                OrderRepo.Update(order);
                SendNotifyToSalesman();
                UnitOfWork.CommitTransaction();
                return true;
            }
            catch (Exception e)
            {
                UnitOfWork.RollbackTransaction();
                throw e;
            }
        }

        public bool SendNotifyToSalesman()
        {
            return true;
        }
    }
}
