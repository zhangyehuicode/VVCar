﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.Shop.Domain.Dtos;
using VVCar.Shop.Domain.Entities;
using VVCar.Shop.Domain.Filters;
using YEF.Core.Data;
using YEF.Core.Domain;

namespace VVCar.Shop.Domain.Services
{
    /// <summary>
    /// 滞销产品设置领域服务接口
    /// </summary>
    public partial interface IUnsaleProductSettingService : IDomainService<IRepository<UnsaleProductSetting>, UnsaleProductSetting, Guid>
    {

        bool BatchDelete(Guid[] id);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        IEnumerable<UnsaleProductSettingDto> Search(UnsaleProductSettingFilter filter, out int totalCount);
    }
}