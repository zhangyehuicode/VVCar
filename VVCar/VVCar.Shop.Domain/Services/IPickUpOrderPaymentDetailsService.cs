using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.Shop.Domain.Entities;
using VVCar.Shop.Domain.Filters;
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

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        IEnumerable<PickUpOrderPaymentDetails> Search(PickUpOrderPaymentDetailsFilter filter, out int totalCount);
    }
}
