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
    public class PickUpOrderPaymentDetailsService : DomainServiceBase<IRepository<PickUpOrderPaymentDetails>, PickUpOrderPaymentDetails, Guid>, IPickUpOrderPaymentDetailsService
    {
        public PickUpOrderPaymentDetailsService()
        {
        }

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
            if (entity == null)
                return null;
            entity.ID = Util.NewID();
            entity.CreatedDate = DateTime.Now;
            return base.Add(entity);
        }

        public IEnumerable<PickUpOrderPaymentDetails> GetPickUpOrderPaymentDetails(string pickUpOrderCode)
        {
            var result = new List<PickUpOrderPaymentDetails>();
            if (string.IsNullOrEmpty(pickUpOrderCode))
                return result;
            result = Repository.GetQueryable(false).Where(t => t.PickUpOrderCode == pickUpOrderCode).ToList();
            return result;
        }
    }
}
