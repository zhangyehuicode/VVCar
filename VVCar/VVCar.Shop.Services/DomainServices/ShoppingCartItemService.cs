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
    public class ShoppingCartItemService : DomainServiceBase<IRepository<ShoppingCartItem>, ShoppingCartItem, Guid>, IShoppingCartItemService
    {
        public ShoppingCartItemService()
        {
        }

        #region properties

        IShoppingCartService ShoppingCartService { get => ServiceLocator.Instance.GetService<IShoppingCartService>(); }

        #endregion

        protected override bool DoValidate(ShoppingCartItem entity)
        {
            if (entity.Quantity < 0)
                throw new DomainException("数量需大于零");
            return true;
        }

        public override ShoppingCartItem Add(ShoppingCartItem entity)
        {
            if (entity == null || entity.ShoppingCartID == null)
                return null;
            entity.ID = Util.NewID();
            entity.CreatedDate = DateTime.Now;
            RecountMoney(entity);
            return base.Add(entity);
        }

        public List<ShoppingCartItem> Add(List<ShoppingCartItem> entities)
        {
            var result = new List<ShoppingCartItem>();
            if (entities == null || entities.Count < 1)
                return null;
            entities.ForEach(t =>
            {
                t.ID = Util.NewID();
                t.CreatedDate = DateTime.Now;
                RecountMoney(t);
            });
            result = Repository.AddRange(entities).ToList();
            return result;
        }

        public bool UpdateShoppingCartItem(ShoppingCartItem entity)
        {
            if (entity == null)
                return false;
            var item = Repository.GetByKey(entity.ID);
            if (item == null)
                return false;
            if (item.Quantity == entity.Quantity)
                return true;
            item.Quantity = entity.Quantity;
            if (item.Quantity < 0)
                throw new DomainException("数量需大于零");
            RecountMoney(item);
            UnitOfWork.BeginTransaction();
            try
            {
                var result = base.Update(item);
                ShoppingCartService.RecountShoppingCart(new ShoppingCart { ID = item.ShoppingCartID }, true);
                UnitOfWork.CommitTransaction();
                return result;
            }
            catch (Exception e)
            {
                UnitOfWork.RollbackTransaction();
                throw e;
            }
        }

        public override bool Delete(Guid key)
        {
            var entity = Repository.GetByKey(key);
            if (entity == null)
                throw new DomainException("数据不存在");
            UnitOfWork.BeginTransaction();
            try
            {
                var result = base.Delete(key);
                ShoppingCartService.RecountShoppingCart(new ShoppingCart { ID = entity.ShoppingCartID }, true);
                UnitOfWork.CommitTransaction();
                return result;
            }
            catch (Exception e)
            {
                UnitOfWork.RollbackTransaction();
                throw e;
            }
        }

        public void RecountMoney(ShoppingCartItem entity)
        {
            if (entity == null)
                return;
            entity.Money = entity.Quantity * entity.PriceSale;
            if (entity.Money < 0)
                entity.Money = 0;
        }
    }
}
