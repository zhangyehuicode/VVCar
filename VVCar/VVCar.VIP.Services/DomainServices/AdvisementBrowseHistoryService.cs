using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.VIP.Domain.Dtos;
using VVCar.VIP.Domain.Entities;
using VVCar.VIP.Domain.Filters;
using VVCar.VIP.Domain.Services;
using YEF.Core;
using YEF.Core.Data;
using YEF.Core.Domain;

namespace VVCar.VIP.Services.DomainServices
{
    /// <summary>
    /// 寻客侠广告浏览记录领域服务
    /// </summary>
    public class AdvisementBrowseHistoryService : DomainServiceBase<IRepository<AdvisementBrowseHistory>, AdvisementBrowseHistory, Guid>, IAdvisementBrowseHistoryService
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override AdvisementBrowseHistory Add(AdvisementBrowseHistory entity)
        {
            if (entity == null)
                throw new DomainException("参数错误");
            entity.ID = Util.NewID();
            entity.CreatedDate = DateTime.Now;
            entity.MerchantID = AppContext.CurrentSession.MerchantID;
            return base.Add(entity);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public IEnumerable<AdvisementBrowseHistoryDto> Search(AdvisementBrowseHistoryFilter filter, out int totalCount)
        {
            var queryable = Repository.GetInclude(t => t.AdvisementSetting, false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID);

            totalCount = queryable.Count();
            if (filter.Start.HasValue && filter.Limit.HasValue)
                queryable = queryable.OrderByDescending(t => t.CreatedDate).Skip(filter.Start.Value).Take(filter.Limit.Value);
            return queryable.MapTo<AdvisementBrowseHistoryDto>().ToArray();
        }
    }
}
