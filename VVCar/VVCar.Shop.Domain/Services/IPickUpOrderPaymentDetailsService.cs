using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.Shop.Domain.Entities;
using YEF.Core.Data;
using YEF.Core.Domain;

namespace VVCar.Shop.Domain.Services
{
    public interface IPickUpOrderPaymentDetailsService : IDomainService<IRepository<PickUpOrderPaymentDetails>, PickUpOrderPaymentDetails, Guid>
    {
        /// <summary>
        /// 获取接车单支付明细
        /// </summary>
        /// <param name="pickUpOrderCode"></param>
        /// <returns></returns>
        IEnumerable<PickUpOrderPaymentDetails> GetPickUpOrderPaymentDetails(string pickUpOrderCode);
    }
}
