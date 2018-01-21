using System;
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
    /// 卡片类型 领域接口
    /// </summary>
    public interface IMemberCardTypeService : IDomainService<IRepository<MemberCardType>, MemberCardType, Guid>
    {
        /// <summary>
        /// 查询卡片类型
        /// </summary>
        /// <param name="filter">过滤条件</param>
        /// <param name="totalCount">总记录数</param>
        /// <returns></returns>
        IEnumerable<MemberCardType> Search(MemberCardTypeFilter filter, out int totalCount);

        /// <summary>
        /// 获取可用的卡片类型
        /// </summary>
        /// <returns></returns>
        IEnumerable<MemberCardTypeDto> GetUsableTypes();
    }
}
