using System.Linq;
using System.Web.Http;
using VVCar.Shop.Domain.Dtos;
using VVCar.Shop.Domain.Entities;
using VVCar.Shop.Domain.Filters;
using VVCar.Shop.Domain.Services;
using YEF.Core;
using YEF.Core.Dtos;

namespace VVCar.Controllers.VIP
{
    /// <summary>
    /// 服务周期配置
    /// </summary>
    [RoutePrefix("api/ServicePeriod")]
    public class ServicePeriodController : BaseApiController
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="servicePeriodService"></param>
        public ServicePeriodController(IServicePeriodService servicePeriodService)
        {
            ServicePeriodService = servicePeriodService;
        }

        IServicePeriodService ServicePeriodService { get; set; }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonActionResult<ServicePeriodSetting> Add(ServicePeriodSetting entity)
        {
            return SafeExecute(() =>
            {
                return this.ServicePeriodService.Add(entity);
            });
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        [HttpDelete, Route("BatchDelete")]
        public JsonActionResult<bool> DeleteServicePeriods(BatchOperationDto parameter)
        {
            return SafeExecute(() =>
            {
                return ServicePeriodService.DeleteServicePeriods(parameter.IdList.ToArray());
            });
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public PagedActionResult<ServicePeriodSettingDto> Search([FromUri]ServicePeriodFilter filter)
        {
            return SafeGetPagedData<ServicePeriodSettingDto>((result) =>
            {
                if (!ModelState.IsValid)
                {
                    throw new DomainException("查询参数错误");
                }
                int totalCount = 0;
                var pageData = this.ServicePeriodService.Search(filter, out totalCount);
                result.Data = pageData;
                result.TotalCount = totalCount;
            });
        }
    }
}