using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VVCar.Shop.Domain.Entities;
using VVCar.Shop.Domain.Filters;
using VVCar.Shop.Domain.Services;
using YEF.Core.Dtos;

namespace VVCar.Controllers.Shop
{
    /// <summary>
    /// 购物车
    /// </summary>
    [RoutePrefix("api/ShoppingCart")]
    public class ShoppingCartController : BaseApiController
    {
        public ShoppingCartController(IShoppingCartService shoppingCartService)
        {
            ShoppingCartService = shoppingCartService;
        }

        IShoppingCartService ShoppingCartService { get; set; }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet, AllowAnonymous]
        public PagedActionResult<ShoppingCart> Search([FromUri]ShoppingCartFilter filter)
        {
            return SafeGetPagedData<ShoppingCart>((result) =>
            {
                var totalCount = 0;
                var data = ShoppingCartService.Search(filter, ref totalCount);
                result.Data = data;
                result.TotalCount = totalCount;
            });
        }

        /// <summary>
        /// 添加到购物车
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost, Route("AddToShoppingCart"), AllowAnonymous]
        public JsonActionResult<ShoppingCart> AddToShoppingCart(ShoppingCart entity)
        {
            return SafeExecute(() =>
            {
                return ShoppingCartService.AddToShoppingCart(entity);
            });
        }
    }
}
