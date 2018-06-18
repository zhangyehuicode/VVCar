using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.Shop.Domain.Entities;
using VVCar.Shop.Domain.Services;
using YEF.Core;
using YEF.Core.Data;
using YEF.Core.Domain;

namespace VVCar.Shop.Services.DomainServices
{
    public class OrderPaymentDetailsService : DomainServiceBase<IRepository<OrderPaymentDetails>, OrderPaymentDetails, Guid>, IOrderPaymentDetailsService
    {
        public OrderPaymentDetailsService()
        {
        }

        #region properties

        IOrderService OrderService { get => ServiceLocator.Instance.GetService<IOrderService>(); }

        IRepository<Order> OrderRepo { get => UnitOfWork.GetRepository<IRepository<Order>>(); }

        #endregion

        protected override bool DoValidate(OrderPaymentDetails entity)
        {
            if (entity == null)
                return false;
            if (string.IsNullOrEmpty(entity.OrderCode))
                throw new DomainException("订单号不能为空");
            if (entity.PayMoney < 0)
                throw new DomainException("支付金额需大于等于零");
            return true;
        }

        public override OrderPaymentDetails Add(OrderPaymentDetails entity)
        {
            UnitOfWork.BeginTransaction();
            try
            {
                if (entity == null)
                    return null;
                if (string.IsNullOrEmpty(entity.OrderCode))
                    throw new DomainException("订单号不能为空");
                if (entity.OrderID == null || entity.OrderID == Guid.Parse("00000000-0000-0000-0000-000000000000"))
                {
                    var order = OrderRepo.GetQueryable(false).FirstOrDefault(t => t.Code == entity.OrderCode);
                    if (order == null)
                        throw new DomainException($"订单{entity.OrderCode}不存在");
                    entity.OrderID = order.ID;
                }
                entity.ID = Util.NewID();
                entity.CreatedDate = DateTime.Now;
                entity.MerchantID = AppContext.CurrentSession.MerchantID;
                var result = base.Add(entity);

                OrderService.RecountMoneySave(entity.OrderCode, true);

                UnitOfWork.CommitTransaction();

                return result;
            }
            catch (Exception e)
            {
                UnitOfWork.RollbackTransaction();
                throw new DomainException(e.Message);
            }
        }

        public IEnumerable<OrderPaymentDetails> GetOrderPaymentDetails(string orderCode)
        {
            var result = new List<OrderPaymentDetails>();
            if (string.IsNullOrEmpty(orderCode))
                return result;
            result = Repository.GetQueryable(false).Where(t => t.OrderCode == orderCode).ToList();
            return result;
        }
    }
}
