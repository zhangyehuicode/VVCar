﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.VIP.Domain.Dtos;
using VVCar.VIP.Domain.Entities;
using VVCar.VIP.Domain.Filters;
using YEF.Core.Data;
using YEF.Core.Domain;

namespace VVCar.VIP.Domain.Services
{
    /// <summary>
    /// 门店砍价领域服务接口
    /// </summary>
    public interface IMerchantBargainOrderService : IDomainService<IRepository<MerchantBargainOrder>, MerchantBargainOrder, Guid>
    {
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        bool BatchDelete(Guid[] ids);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        IEnumerable<MerchantBargainOrderDto> Search(MerchantBargainOrderFilter filter, out int totalCount);

        /// <summary>
        /// 获取砍价数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IEnumerable<MerchantBargainOrderDto> GetMerchantBargainOrderListByProductID(Guid id);
    }
}