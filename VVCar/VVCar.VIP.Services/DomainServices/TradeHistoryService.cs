using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.VIP.Domain.Dtos;
using VVCar.VIP.Domain.Entities;
using VVCar.VIP.Domain.Services;
using YEF.Core;
using YEF.Core.Data;
using YEF.Core.Domain;

namespace VVCar.VIP.Services.DomainServices
{
    /// <summary>
    /// 消费记录 领域服务
    /// </summary>
    public partial class TradeHistoryService : DomainServiceBase<IRepository<TradeHistory>, TradeHistory, Guid>, ITradeHistoryService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TradeHistoryService"/> class.
        /// </summary>
        public TradeHistoryService()
        {
        }

        #region methods

        public override TradeHistory Add(TradeHistory entity)
        {
            if (entity == null)
                return null;
            entity.ID = Util.NewID();
            entity.TradeDepartmentID = AppContext.CurrentSession.DepartmentID;
            entity.CreatedUserID = AppContext.CurrentSession.UserID;
            if (string.IsNullOrEmpty(entity.CreatedUser))
                entity.CreatedUser = AppContext.CurrentSession.UserName;
            entity.CreatedDate = DateTime.Now;
            return base.Add(entity);
        }

        #endregion

        #region ITradeHistoryService 成员

        public IEnumerable<TradeHistoryDto> Search(Domain.Filters.HistoryFilter filter, out int totalCount)
        {
            IEnumerable<TradeHistoryDto> pagedData;
            var queryable = this.Repository.GetIncludes(false, "Member", "TradeDepartment");
            if (filter != null)
            {
                if (!string.IsNullOrEmpty(filter.WeChatOpenID))
                {
                    queryable = queryable.Where(t => t.Member.WeChatOpenID == filter.WeChatOpenID);
                }
                if (!string.IsNullOrEmpty(filter.CardNumber))
                    queryable = queryable.Where(t => t.CardNumber.Contains(filter.CardNumber));
                if (filter.TradeDepartmentID.HasValue)
                    queryable = queryable.Where(t => t.TradeDepartmentID == filter.TradeDepartmentID.Value);
                if (!string.IsNullOrEmpty(filter.MobilePhoneNo))
                    queryable = queryable.Where(t => t.Member.MobilePhoneNo.Contains(filter.MobilePhoneNo));
                if (filter.Status.HasValue)
                    queryable = queryable.Where(t => t.Card.Status == filter.Status.Value);
                if (filter.StartDate.HasValue)
                    queryable = queryable.Where(t => t.CreatedDate >= filter.StartDate);
                if (filter.FinishDate.HasValue)
                {
                    var finishDate = filter.FinishDate.Value.AddDays(1);
                    queryable = queryable.Where(t => t.CreatedDate < finishDate);
                }
                if (filter.BusinessType.HasValue)
                    queryable = queryable.Where(t => t.BusinessType == filter.BusinessType.Value);
                if (filter.ConsumeType.HasValue)
                    queryable = queryable.Where(t => t.ConsumeType == filter.ConsumeType.Value);
                if (!string.IsNullOrEmpty(filter.BatchCode))
                {
                    queryable = queryable.Where(t => t.Card.BatchCode.Contains(filter.BatchCode));
                }
                if (filter.CardTypeID.HasValue)
                    queryable = queryable.Where(t => t.Card.CardTypeID == filter.CardTypeID.Value);
            }
            queryable = queryable.OrderByDescending(t => t.CreatedDate);
            if (filter != null && filter.Start.HasValue && filter.Limit.HasValue)
            {
                totalCount = queryable.Count();
                pagedData = queryable.Skip(filter.Start.Value).Take(filter.Limit.Value)
                    .MapTo<TradeHistoryDto>().ToArray();
            }
            else
            {
                pagedData = queryable.MapTo<TradeHistoryDto>().ToArray();
                totalCount = pagedData.Count();
            }
            return pagedData;
        }

        #endregion
    }
}
