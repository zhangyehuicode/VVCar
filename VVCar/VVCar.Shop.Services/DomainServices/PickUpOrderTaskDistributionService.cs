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
    public class PickUpOrderTaskDistributionService : DomainServiceBase<IRepository<PickUpOrderTaskDistribution>, PickUpOrderTaskDistribution, Guid>, IPickUpOrderTaskDistributionService
    {
        public PickUpOrderTaskDistributionService()
        {
        }

        public override PickUpOrderTaskDistribution Add(PickUpOrderTaskDistribution entity)
        {
            if (entity == null)
                return null;
            entity.ID = Util.NewID();
            entity.CreatedUserID = AppContext.CurrentSession.UserID;
            entity.CreatedUser = AppContext.CurrentSession.UserName;
            entity.CreatedDate = DateTime.Now;
            entity.MerchantID = AppContext.CurrentSession.MerchantID;
            return base.Add(entity);
        }

        public override bool Delete(Guid key)
        {
            var entity = Repository.GetByKey(key);
            if (entity == null)
                throw new DomainException("数据不存在");
            entity.IsDeleted = true;
            entity.LastUpdateUserID = AppContext.CurrentSession.UserID;
            entity.LastUpdateUser = AppContext.CurrentSession.UserName;
            entity.LastUpdateDate = DateTime.Now;
            return Repository.Update(entity) > 0;
        }
    }
}
