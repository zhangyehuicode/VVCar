using System;
using System.Collections.Generic;
using VVCar.Shop.Domain.Dtos;
using VVCar.Shop.Domain.Entities;
using VVCar.Shop.Domain.Filters;
using YEF.Core.Data;
using YEF.Core.Domain;

namespace VVCar.Shop.Domain.Services
{
    /// <summary>
    /// 服务周期配置领域接口
    /// </summary>
    public interface IServicePeriodService : IDomainService<IRepository<ServicePeriodSetting>, ServicePeriodSetting, Guid>
    {
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        bool DeleteServicePeriods(Guid[] ids);

        /// <summary>
        /// 启用服务
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        bool EnableServicePeriod(Guid[] ids);

        /// <summary>
        /// 禁用服务
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        bool DisableServicePeriod(Guid[] ids);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        IEnumerable<ServicePeriodSettingDto> Search(ServicePeriodFilter filter, out int totalCount);

        /// <summary>
        /// 服务周期提醒
        /// </summary>
        /// <returns></returns>
        bool ServicePeriodReminder();
    }
}
