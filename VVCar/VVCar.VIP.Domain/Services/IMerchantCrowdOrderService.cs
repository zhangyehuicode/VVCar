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
    /// 门店拼单领域服务接口
    /// </summary>
    public interface IMerchantCrowdOrderService : IDomainService<IRepository<MerchantCrowdOrder>, MerchantCrowdOrder, Guid>
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
        IEnumerable<MerchantCrowdOrderDto> Search(MerchantCrowdOrderFilter filter, out int totalCount);

        /// <summary>
        /// 获取拼单数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IEnumerable<MerchantCrowdOrderDto> GetMerchantCrowdOrderListByProductID(Guid id);
    }
}
