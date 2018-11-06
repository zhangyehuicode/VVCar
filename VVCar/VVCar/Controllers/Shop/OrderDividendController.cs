using System.Web.Http;
using VVCar.Shop.Domain.Dtos;
using VVCar.Shop.Domain.Entities;
using VVCar.Shop.Domain.Filters;
using VVCar.Shop.Domain.Services;
using YEF.Core.Dtos;

namespace VVCar.Controllers.Shop
{
    /// <summary>
    /// 订单分红
    /// </summary>
    [RoutePrefix("api/OrderDividend")]
    public class OrderDividendController : BaseApiController
    {
        public OrderDividendController(IOrderDividendService orderDividendService)
        {
            OrderDividendService = orderDividendService;
        }

        #region properties

        IOrderDividendService OrderDividendService { get; set; }

        #endregion

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        [HttpGet, AllowAnonymous]
        public PagedActionResult<OrderDividendDto> Search([FromUri]OrderDividendFilter filter)
        {
            return SafeGetPagedData<OrderDividendDto>((result) =>
            {
                var totalCount = 0;
                var data = OrderDividendService.Search(filter, out totalCount);
                result.Data = data;
                result.TotalCount = totalCount;
            });
        }
    }
}
