using System.Linq;
using System.Web.Http;
using VVCar.VIP.Domain.Entities;
using VVCar.VIP.Domain.Filters;
using VVCar.VIP.Domain.Services;
using YEF.Core.Dtos;

namespace VVCar.Controllers.VIP
{
    /// <summary>
    /// 公告
    /// </summary>
    [RoutePrefix("api/Announcement")]
    public class AnnouncementController : BaseApiController
    {
        /// <summary>
        /// ctor
        /// </summary>
        public AnnouncementController(IAnnouncementService announcementService)
        {
            AnnouncementService = announcementService;
        }

        IAnnouncementService AnnouncementService { get; set; }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonActionResult<Announcement> Add(Announcement entity)
        {
            return SafeExecute(() =>
            {
                return AnnouncementService.Add(entity);
            });
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPut]
        public JsonActionResult<bool> Update(Announcement entity)
        {
            return SafeExecute(() =>
            {
                return AnnouncementService.Update(entity);
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
                return AnnouncementService.BatchDelete(parameter.IdList.ToArray());
            });
        }

        /// <summary>
        /// 手动批量推送
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        [HttpPost, Route("BatchHandPush")]
        public JsonActionResult<bool> BatchHandPush(BatchOperationDto parameter)
        {
            return SafeExecute(() =>
            {
                return AnnouncementService.BatchHandPush(parameter.IdList.ToArray());
            });
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        public PagedActionResult<Announcement> Search([FromUri]AnnouncementFilter filter)
        {
            return SafeGetPagedData<Announcement>((result) =>
            {
                var totalCount = 0;
                var data = AnnouncementService.Search(filter, out totalCount);
                result.Data = data;
                result.TotalCount = totalCount;
            });
        }
    }
}
