using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.BaseData.Domain.Dtos;
using VVCar.BaseData.Domain.Entities;
using VVCar.BaseData.Domain.Filters;
using YEF.Core.Data;
using YEF.Core.Domain;

namespace VVCar.BaseData.Domain.Services
{
    /// <summary>
    /// SystemSetting 领域接口
    /// </summary>
    public partial interface ISystemSettingService : IDomainService<IRepository<SystemSetting>, SystemSetting, Guid>
    {
        /// <summary>
        /// 更新设置值
        /// </summary>
        /// <param name="settingID">设置项ID</param>
        /// <param name="settingValue">设置值</param>
        bool UpdateSetting(Guid settingID, string settingValue);

        /// <summary>
        /// 获取设置值
        /// </summary>
        /// <param name="name">设置项名称</param>
        /// <param name="merchantId">商户Id</param>
        /// <returns></returns>
        string GetSettingValue(string name, Guid? merchantId = null);

        /// <summary>
        /// 查询系统参数
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        IEnumerable<SystemSettingDto> Search(SystemSettingFilter filter, out int totalCount);
    }
}
