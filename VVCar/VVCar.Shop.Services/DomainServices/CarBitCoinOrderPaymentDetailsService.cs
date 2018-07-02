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
    /// <summary>
    /// 车比特订单支付明细 领域服务
    /// </summary>
    public class CarBitCoinOrderPaymentDetailsService : DomainServiceBase<IRepository<CarBitCoinOrderPaymentDetails>, CarBitCoinOrderPaymentDetails, Guid>, ICarBitCoinOrderPaymentDetailsService
    {
        public CarBitCoinOrderPaymentDetailsService()
        {
        }

        #region properties

        ICarBitCoinOrderService OrderService { get => ServiceLocator.Instance.GetService<ICarBitCoinOrderService>(); }

        IRepository<CarBitCoinOrder> OrderRepo { get => UnitOfWork.GetRepository<IRepository<CarBitCoinOrder>>(); }

        #endregion

        protected override bool DoValidate(CarBitCoinOrderPaymentDetails entity)
        {
            if (entity == null)
                return false;
            if (string.IsNullOrEmpty(entity.CarBitCoinOrderCode))
                throw new DomainException("订单号不能为空");
            if (entity.PayMoney < 0)
                throw new DomainException("支付金额需大于等于零");
            return true;
        }

        public override CarBitCoinOrderPaymentDetails Add(CarBitCoinOrderPaymentDetails entity)
        {
            UnitOfWork.BeginTransaction();
            try
            {
                if (entity == null)
                    return null;
                if (string.IsNullOrEmpty(entity.CarBitCoinOrderCode))
                    throw new DomainException("订单号不能为空");
                if (entity.CarBitCoinOrderID == null || entity.CarBitCoinOrderID == Guid.Parse("00000000-0000-0000-0000-000000000000"))
                {
                    var order = OrderRepo.GetQueryable(false).FirstOrDefault(t => t.Code == entity.CarBitCoinOrderCode);
                    if (order == null)
                        throw new DomainException($"订单{entity.CarBitCoinOrderCode}不存在");
                    entity.CarBitCoinOrderID = order.ID;
                }
                entity.ID = Util.NewID();
                entity.CreatedDate = DateTime.Now;
                entity.MerchantID = AppContext.CurrentSession.MerchantID;
                var result = base.Add(entity);

                OrderService.RecountMoneySave(entity.CarBitCoinOrderCode, true);

                UnitOfWork.CommitTransaction();

                return result;
            }
            catch (Exception e)
            {
                UnitOfWork.RollbackTransaction();
                throw new DomainException(e.Message);
            }
        }

        public IEnumerable<CarBitCoinOrderPaymentDetails> GetOrderPaymentDetails(string orderCode)
        {
            var result = new List<CarBitCoinOrderPaymentDetails>();
            if (string.IsNullOrEmpty(orderCode))
                return result;
            result = Repository.GetQueryable(false).Where(t => t.CarBitCoinOrderCode == orderCode).ToList();
            return result;
        }
    }
}
