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
    public interface IOrderPaymentDetailsService : IDomainService<IRepository<OrderPaymentDetails>, OrderPaymentDetails, Guid>
    {

    }
}
