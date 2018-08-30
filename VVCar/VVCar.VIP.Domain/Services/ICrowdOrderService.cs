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
    /// 拼单领域服务接口
    /// </summary>
    public partial interface ICrowdOrderService : IDomainService<IRepository<CrowdOrder>, CrowdOrder, Guid>
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
        IEnumerable<CrowdOrderDto> Search(CrowdOrderFilter filter, out int totalCount);

        /// <summary>
        /// 获取拼单数据
        /// </summary>
        /// <returns></returns>
        IEnumerable<CrowdOrderDto> GetCrowdOrders();
    }
}
