using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VVCar.Shop.Domain.Entities;
using VVCar.Shop.Domain.Services;
using YEF.Core.Dtos;

namespace VVCar.Controllers.Shop
{
    /// <summary>
    /// 购物车子项
    /// </summary>
    [RoutePrefix("api/ShoppingCartItem")]
    public class ShoppingCartItemController : BaseApiController
    {
        public ShoppingCartItemController(IShoppingCartItemService shoppingCartItemService)
        {
            ShoppingCartItemService = shoppingCartItemService;
        }

        IShoppingCartItemService ShoppingCartItemService { get; set; }

        /// <summary>
        /// 更新数量
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost, Route("UpdateShoppingCartItem"), AllowAnonymous]
        public JsonActionResult<bool> UpdateShoppingCartItem(ShoppingCartItem entity)
        {
            return SafeExecute(() =>
            {
                return ShoppingCartItemService.UpdateShoppingCartItem(entity);
            });
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet, Route("DeleteShoppingCartItem"), AllowAnonymous]
        public JsonActionResult<bool> DeleteShoppingCartItem(Guid id)
        {
            return SafeExecute(() =>
            {
                return ShoppingCartItemService.Delete(id);
            });
        }
    }
}
