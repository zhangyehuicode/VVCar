using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using VVCar.Shop.Domain.Entities;
using VVCar.Shop.Domain.Filters;
using VVCar.Shop.Domain.Services;
using YEF.Core;
using YEF.Core.Dtos;

namespace VVCar.Controllers.Shop
{
    /// <summary>
    /// 套餐子项
    /// </summary>
    [RoutePrefix("api/ComboItem")]
    public class ComboItemController : BaseApiController
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="comboItemService"></param>
        public ComboItemController(IComboItemService comboItemService)
        {
            ComboItemService = comboItemService;
        }

        IComboItemService ComboItemService { get; set; }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonActionResult<ComboItem> Add(ComboItem entity)
        {
            return SafeExecute(() =>
            {
                return ComboItemService.Add(entity);
            });
        }

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="comboItems"></param>
        /// <returns></returns>
        [HttpPost, Route("BatchAdd")]
        public JsonActionResult<bool> BatchAdd(IEnumerable<ComboItem> comboItems)
        {
            return SafeExecute(() =>
            {
                if (comboItems == null)
                    throw new DomainException("参数错误");
                return this.ComboItemService.BatchAdd(comboItems);
            });
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        [HttpDelete, Route("BatchDelete")]
        public JsonActionResult<bool> BatchDelete(BatchOperationDto parameter)
        {
            return SafeExecute(() =>
            {
                return this.ComboItemService.BatchDelete(parameter.IdList.ToArray());
            });
        }

        /// <summary>
        /// 更新库存数量
        /// </summary>
        /// <param name="comboItem"></param>
        /// <returns></returns>
        [HttpPut]
        public JsonActionResult<bool> UpdateStock(ComboItem comboItem)
        {
            return SafeExecute(() =>
            {
                return ComboItemService.Update(comboItem);
            });
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        public PagedActionResult<ComboItem> Search([FromUri]ComboItemFilter filter)
        {
            return SafeGetPagedData<ComboItem>((result) =>
            {
                var totalCount = 0;
                var data = this.ComboItemService.Search(filter, out totalCount);
                result.Data = data;
                result.TotalCount = totalCount;
            });
        }
    }
}
