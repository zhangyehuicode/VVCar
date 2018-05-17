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
using YEF.Utility;

namespace VVCar.Shop.Services.DomainServices
{
    public class ShoppingCartService : DomainServiceBase<IRepository<ShoppingCart>, ShoppingCart, Guid>, IShoppingCartService
    {
        public ShoppingCartService()
        {
        }

        #region properties

        IShoppingCartItemService ShoppingCartItemService { get => ServiceLocator.Instance.GetService<IShoppingCartItemService>(); }

        #endregion

        public override ShoppingCart Add(ShoppingCart entity)
        {
            if (entity == null || entity.ShoppingCartItemList == null || entity.ShoppingCartItemList.Count < 1)
                return null;
            if (string.IsNullOrEmpty(entity.OpenID))
                throw new DomainException("缺少用户信息");
            entity.ID = Util.NewID();
            entity.CreatedDate = DateTime.Now;
            entity.ShoppingCartItemList.ForEach(t =>
            {
                t.ID = Util.NewID();
                t.ShoppingCartID = entity.ID;
                t.CreatedDate = DateTime.Now;
            });
            RecountShoppingCart(entity);
            return base.Add(entity);
        }

        public ShoppingCart UpdateAddItems(ShoppingCart entity)
        {
            if (entity == null)
                return null;
            var cart = Repository.GetInclude(t => t.ShoppingCartItemList).Where(t => t.ID == entity.ID).FirstOrDefault();
            if (cart == null)
                return null;

            cart.OpenID = entity.OpenID;
            RecountShoppingCart(entity);
            cart.Money = entity.Money;
            cart.LastUpdatedDate = DateTime.Now;

            List<ShoppingCartItem> newItems = null;
            var items = entity.ShoppingCartItemList;
            if (items != null && items.Count() > 0)
            {
                items.ForEach(t =>
                {
                    t.ShoppingCartID = cart.ID;
                });
                newItems = ShoppingCartItemService.Add(items.ToList());
            }

            if (base.Update(cart))
                return cart;
            return null;
        }

        public void RecountShoppingCart(ShoppingCart entity, bool saveChange = false)
        {
            if (entity == null)
                return;
            decimal totalMoney = 0;
            if (saveChange)
            {
                var cart = Repository.GetInclude(t => t.ShoppingCartItemList).Where(t => t.ID == entity.ID).FirstOrDefault();
                if (cart != null)
                {
                    if (cart.ShoppingCartItemList != null && cart.ShoppingCartItemList.Count > 0)
                    {
                        cart.ShoppingCartItemList.ForEach(t =>
                        {
                            totalMoney += t.Quantity * t.PriceSale;
                        });
                        cart.Money = totalMoney;
                    }
                    else
                        cart.Money = 0;
                    Repository.Update(cart);
                }
            }
            else
            {
                if (entity.ShoppingCartItemList == null || entity.ShoppingCartItemList.Count < 1)
                    entity.Money = 0;
                else
                {
                    entity.ShoppingCartItemList.ForEach(t =>
                    {
                        totalMoney += t.Quantity * t.PriceSale;
                    });
                    entity.Money = totalMoney;
                }
            }
        }

        public ShoppingCart AddToShoppingCart(ShoppingCart entity)
        {
            UnitOfWork.BeginTransaction();
            try
            {
                ShoppingCart result = null;
                if (entity == null)
                    return null;
                if (string.IsNullOrEmpty(entity.OpenID))
                    return null;
                var cart = Repository.GetQueryable(false).Where(t => t.OpenID == entity.OpenID).FirstOrDefault();
                if (cart == null)
                    result = Add(entity);
                else
                {
                    entity.ID = cart.ID;
                    result = UpdateAddItems(entity);
                }
                UnitOfWork.CommitTransaction();
                if (cart != null)
                    RecountShoppingCart(result, true);
                return result;
            }
            catch (Exception e)
            {
                UnitOfWork.RollbackTransaction();
                AppContext.Logger.Error(e.Message);
                throw e;
            }
        }

        public IEnumerable<ShoppingCart> Search(ShoppingCartFilter filter, ref int totalCount)
        {
            var queryable = Repository.GetInclude(t => t.ShoppingCartItemList, false);
            if (!string.IsNullOrEmpty(filter.OpenID))
                queryable = queryable.Where(t => t.OpenID == filter.OpenID);
            if (filter.MemberID.HasValue)
                queryable = queryable.Where(t => t.MemberID == filter.MemberID.Value);
            totalCount = queryable.Count();
            if (filter.Start.HasValue && filter.Limit.HasValue)
                queryable = queryable.OrderByDescending(t => t.CreatedDate).Skip(filter.Start.Value).Take(filter.Limit.Value);
            return queryable.ToArray();
        }
    }
}
