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
    /// 素材管理
    /// </summary>
    [RoutePrefix("api/Material")]
    public class MaterialController : BaseApiController
    {
        public MaterialController(IMaterialService materialService)
        {
            MaterialService = materialService;
        }

        IMaterialService MaterialService { get; set; }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonActionResult<Material> Add(Material entity)
        {
            return SafeExecute(() =>
            {
                return MaterialService.Add(entity);
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
                return MaterialService.BatchDelete(parameter.IdList.ToArray());
            });
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPut]
        public JsonActionResult<bool> Update(Material entity)
        {
            return SafeExecute(() =>
            {
                return MaterialService.Update(entity);
            });
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet, AllowAnonymous]
        public PagedActionResult<Material> Search([FromUri]MaterialFilter filter)
        {
            return SafeGetPagedData<Material>((result) =>
            {
                var totalCount = 0;
                var data = MaterialService.Search(filter, out totalCount);
                result.Data = data;
                result.TotalCount = totalCount;
            });
        }
    }
}
