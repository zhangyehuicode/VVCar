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
    public interface IShoppingCartItemService : IDomainService<IRepository<ShoppingCartItem>, ShoppingCartItem, Guid>
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        List<ShoppingCartItem> Add(List<ShoppingCartItem> entities);

        /// <summary>
        /// 更新数量
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool UpdateShoppingCartItem(ShoppingCartItem entity);
    }
}
