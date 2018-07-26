using System;
using System.Linq;
using System.Web.Http;
using VVCar.BaseData.Domain.Dtos;
using VVCar.BaseData.Domain.Entities;
using VVCar.BaseData.Domain.Filters;
using VVCar.BaseData.Domain.Services;
using YEF.Core.Dtos;

namespace VVCar.Controllers.Api
{
    /// <summary>
    /// 门店标签(客户标签)
    /// </summary>
    [RoutePrefix("api/Tag")]
    public class TagController : BaseApiController
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="tagService"></param>
        public TagController(ITagService tagService)
        {
            TagService = tagService;
        }

        ITagService TagService { get; set; }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonActionResult<Tag> Add(Tag entity)
        {
            return SafeExecute(() =>
            {
                return TagService.Add(entity);
            });
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPut]
        public JsonActionResult<bool> Update(Tag entity)
        {
            return SafeExecute(() =>
            {
                return TagService.Update(entity);
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
                return TagService.BatchDelete(parameter.IdList.ToArray());
            });
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        public PagedActionResult<TagDto> Search([FromUri]TagFilter filter)
        {
            return SafeGetPagedData<TagDto>((result) =>
            {
                var totalCount = 0;
                var data = TagService.Search(filter, out totalCount);
                result.Data = data;
                result.TotalCount = totalCount;
            });
        }
    }
}
