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
    /// 寻客侠广告设置
    /// </summary>
    [RoutePrefix("api/AdvisementSetting")]
    public class AdvisementSettingController : BaseApiController
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="advisementSettingService"></param>
        public AdvisementSettingController(IAdvisementSettingService advisementSettingService)
        {
            AdvisementSettingService = advisementSettingService;
        }

        IAdvisementSettingService AdvisementSettingService { get; set; }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonActionResult<AdvisementSetting> Add(AdvisementSetting entity)
        {
            return SafeExecute(() =>
            {
                return AdvisementSettingService.Add(entity);
            });
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPut]
        public JsonActionResult<bool> Update(AdvisementSetting entity)
        {
            return SafeExecute(() =>
            {
                return AdvisementSettingService.Update(entity);
            });
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public JsonActionResult<bool> Delete(Guid id)
        {
            return SafeExecute(() =>
            {
                return AdvisementSettingService.Delete(id);
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
                return AdvisementSettingService.BatchDelete(parameter.IdList.ToArray());
            });
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet, AllowAnonymous]
        public PagedActionResult<AdvisementSetting> Search([FromUri]AdvisementSettingFilter filter)
        {
            return SafeGetPagedData<AdvisementSetting>((result) =>
            {
                var totalCount = 0;
                var data = AdvisementSettingService.Search(filter, out totalCount);
                result.Data = data;
                result.TotalCount = totalCount;
            });
        }
    }
}
