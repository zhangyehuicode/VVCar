using System;
using System.Linq;
using System.Web.Http;
using VVCar.VIP.Domain.Entities;
using VVCar.VIP.Domain.Filters;
using VVCar.VIP.Domain.Services;
using YEF.Core.Dtos;

namespace VVCar.Controllers.VIP
{
    /// <summary>
    /// 超能课堂
    /// </summary>
    [RoutePrefix("api/SuperClass")]
    public class SuperClassController : BaseApiController
    {
        /// <summary>
        /// 超能课堂
        /// </summary>
        /// <param name="superClassService"></param>
        public SuperClassController(ISuperClassService superClassService)
        {
            SuperClassService = superClassService;
        }

        ISuperClassService SuperClassService { get; set; }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonActionResult<SuperClass> Add(SuperClass entity)
        {
            return SafeExecute(() =>
            {
                return SuperClassService.Add(entity);
            });
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPut]
        public JsonActionResult<bool> Update(SuperClass entity)
        {
            return SafeExecute(() =>
            {
                return SuperClassService.Update(entity);
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
                return SuperClassService.BatchDelete(parameter.IdList.ToArray());
            });
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet, AllowAnonymous]
        public PagedActionResult<SuperClass> Search([FromUri]SuperClassFilter filter)
        {
            return SafeGetPagedData<SuperClass>((result) =>
            {
                var totalCount = 0;
                var data = SuperClassService.Search(filter, out totalCount);
                result.Data = data;
                result.TotalCount = totalCount;
            });
        }
    }
}
