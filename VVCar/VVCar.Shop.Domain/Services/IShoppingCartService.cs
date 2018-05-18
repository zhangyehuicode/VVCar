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
    public interface IShoppingCartService : IDomainService<IRepository<ShoppingCart>, ShoppingCart, Guid>
    {
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        IEnumerable<ShoppingCart> Search(ShoppingCartFilter filter, ref int totalCount);

        /// <summary>
        /// 添加到购物车
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        ShoppingCart AddToShoppingCart(ShoppingCart entity);

        /// <summary>
        /// 重新计算订单总额
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="saveChange"></param>
        void RecountShoppingCart(ShoppingCart entity, bool saveChange = false);

        /// <summary>
        /// 清除购物车
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        bool ClearShoppingCart(string openid);
    }
}
