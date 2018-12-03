using System;
using System.Linq;
using System.Web.Http;
using VVCar.Shop.Domain.Entities;
using VVCar.Shop.Domain.Filters;
using VVCar.Shop.Domain.Services;
using YEF.Core.Dtos;

namespace VVCar.Controllers.Shop
{
    /// <summary>
    /// 日常开支
    /// </summary>
    [RoutePrefix("api/DailyExpense")]
    public class DailyExpenseController : BaseApiController
    {
        public DailyExpenseController(IDailyExpenseService dailyExpenseService)
        {
            DailyExpenseService = dailyExpenseService;
        }

        IDailyExpenseService DailyExpenseService { get; set; }

        /// <summary>
        /// 新增
        /// </summary>
        [HttpPost]
        public JsonActionResult<DailyExpense> Add(DailyExpense entity)
        {
            return SafeExecute(() =>
            {
                return DailyExpenseService.Add(entity);
            });
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPut]
        public JsonActionResult<bool> Update(DailyExpense entity)
        {
            return SafeExecute(() =>
            {
                return DailyExpenseService.Update(entity);
            });
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        [HttpDelete, Route("BatchDelete")]
        public JsonActionResult<bool> BatchDelete(BatchOperationDto parameter)
        {
            return SafeExecute(() =>
            {
                return DailyExpenseService.BatchDelete(parameter.IdList.ToArray());
            });
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet, AllowAnonymous]
        public PagedActionResult<DailyExpense> Search([FromUri]DailyExpenseFilter filter)
        {
            return SafeGetPagedData<DailyExpense>((result) =>
            {
                var totalCount = 0;
                var data = DailyExpenseService.Search(filter, out totalCount);
                result.Data = data;
                result.TotalCount = totalCount;
            });
        }
    }
}
