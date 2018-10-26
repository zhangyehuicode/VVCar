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
    public class PickUpOrderPaymentDetailsService : DomainServiceBase<IRepository<PickUpOrderPaymentDetails>, PickUpOrderPaymentDetails, Guid>, IPickUpOrderPaymentDetailsService
    {
        public PickUpOrderPaymentDetailsService()
        {
        }

        #region properties

        IPickUpOrderService PickUpOrderService { get => ServiceLocator.Instance.GetService<IPickUpOrderService>(); }

        IRepository<PickUpOrder> PickUpOrderRepo { get => UnitOfWork.GetRepository<IRepository<PickUpOrder>>(); }

        #endregion

        protected override bool DoValidate(PickUpOrderPaymentDetails entity)
        {
            if (entity == null)
                return false;
            if (string.IsNullOrEmpty(entity.PickUpOrderCode))
                throw new DomainException("订单号不能为空");
            if (entity.PayMoney < 0)
                throw new DomainException("支付金额需大于等于零");
            return true;
        }

        public override PickUpOrderPaymentDetails Add(PickUpOrderPaymentDetails entity)
        {
            UnitOfWork.BeginTransaction();
            try
            {
                if (entity == null)
                    return null;
                if (string.IsNullOrEmpty(entity.PickUpOrderCode))
                    throw new DomainException("订单号不能为空");
                if (entity.PickUpOrderID == null || entity.PickUpOrderID == Guid.Parse("00000000-0000-0000-0000-000000000000"))
                {
                    var pickuporder = PickUpOrderRepo.GetQueryable(false).FirstOrDefault(t => t.Code == entity.PickUpOrderCode);
                    if (pickuporder == null)
                        throw new DomainException($"订单{entity.PickUpOrderCode}不存在");
                    entity.PickUpOrderID = pickuporder.ID;
                }
                entity.ID = Util.NewID();
                entity.CreatedDate = DateTime.Now;
                entity.MerchantID = AppContext.CurrentSession.MerchantID;
                var result = base.Add(entity);

                PickUpOrderService.RecountMoneySave(entity.PickUpOrderCode);

                UnitOfWork.CommitTransaction();

                return result;
            }
            catch (Exception e)
            {
                UnitOfWork.RollbackTransaction();
                throw new DomainException(e.Message);
            }
        }

        public IEnumerable<PickUpOrderPaymentDetails> GetPickUpOrderPaymentDetails(string pickUpOrderCode)
        {
            var result = new List<PickUpOrderPaymentDetails>();
            if (string.IsNullOrEmpty(pickUpOrderCode))
                return result;
            result = Repository.GetQueryable(false).Where(t => t.PickUpOrderCode == pickUpOrderCode).ToList();
            return result;
        }

        /// <summary>
        /// 出现
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public IEnumerable<PickUpOrderPaymentDetails> Search(PickUpOrderPaymentDetailsFilter filter, out int totalCount)
        {
            var queryable = Repository.GetQueryable(false).Where(t => t.PickUpOrderID == filter.PickUpOrderID && t.MerchantID == AppContext.CurrentSession.MerchantID);
            totalCount = queryable.Count();
            if(filter.Start.HasValue && filter.Limit.HasValue)
                queryable = queryable.OrderByDescending(t => t.CreatedDate).Skip(filter.Start.Value).Take(filter.Limit.Value);
            return queryable.ToList();
        }
    }
}
