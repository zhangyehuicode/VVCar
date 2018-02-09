using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VVCar.VIP.Domain.Dtos;
using VVCar.VIP.Domain.Entities;
using VVCar.VIP.Domain.Enums;
using VVCar.VIP.Domain.Filters;
using VVCar.VIP.Domain.Services;
using YEF.Core;
using YEF.Core.Data;
using YEF.Core.Domain;

namespace VVCar.VIP.Services.DomainServices
{
    /// <summary>
    /// 储值记录领域服务
    /// </summary>
    public partial class RechargeHistoryService : DomainServiceBase<IRepository<RechargeHistory>, RechargeHistory, Guid>, IRechargeHistoryService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RechargeHistoryService"/> class.
        /// </summary>
        public RechargeHistoryService()
        {
        }

        #region methods

        public override RechargeHistory Add(RechargeHistory entity)
        {
            if (entity == null)
                return null;
            entity.ID = Util.NewID();
            if (entity.TradeDepartmentID == null)
                entity.TradeDepartmentID = AppContext.CurrentSession.DepartmentID;
            entity.CreatedUserID = AppContext.CurrentSession.UserID;
            if (string.IsNullOrEmpty(entity.CreatedUser))
                entity.CreatedUser = AppContext.CurrentSession.UserName;
            entity.CreatedDate = DateTime.Now;
            return base.Add(entity);
        }

        #endregion

        #region IRechargeHistoryService 成员

        public IEnumerable<TradeHistoryDto> Search(Domain.Filters.HistoryFilter filter, out int totalCount)
        {
            IEnumerable<TradeHistoryDto> pagedData = null;
            var queryable = this.Repository.GetIncludes(false, "Member", "TradeDepartment");
            queryable.GroupBy(t => t.TradeDepartmentID)
                .Select(t => new
                {
                    TotalAmount = t.Sum(g => g.TradeAmount),
                });
            if (filter != null)
            {
                if (!string.IsNullOrEmpty(filter.WeChatOpenID))
                {
                    queryable = queryable.Where(t => t.Member.WeChatOpenID == filter.WeChatOpenID);
                }
                if (!string.IsNullOrEmpty(filter.DepartmentCode))
                    queryable = queryable.Where(t => t.TradeDepartment.Code == filter.DepartmentCode);
                if (filter.TradeDepartmentID.HasValue)
                    queryable = queryable.Where(t => t.TradeDepartmentID == filter.TradeDepartmentID.Value);
                if (!string.IsNullOrEmpty(filter.CardNumber))
                    queryable = queryable.Where(t => t.CardNumber.Contains(filter.CardNumber));
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

        public decimal LastRecharge(Expression<Func<RechargeHistory, bool>> predicate)
        {
            var lastRecharge = Repository.GetQueryable(false)
                .Where(predicate)
                .OrderByDescending(p => p.CreatedDate)
                .Select(t => t.TradeAmount)
                .FirstOrDefault();
            return lastRecharge;
        }

        /// <summary>
        /// 获取储值统计信息
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public RechargeTotalDataDto GetTotalData(RechargeHistoryFilter filter)
        {
            var queryable = this.Repository.GetQueryable(false);
            if (filter != null)
            {
                if (!string.IsNullOrEmpty(filter.DepartmentCode))
                    queryable = queryable.Where(t => t.TradeDepartment.Code == filter.DepartmentCode);
                if (filter.StartDate.HasValue)
                    queryable = queryable.Where(t => t.CreatedDate >= filter.StartDate);
                if (filter.FinishDate.HasValue)
                {
                    var finishDate = filter.FinishDate.Value.AddDays(1);
                    queryable = queryable.Where(t => t.CreatedDate < finishDate);
                }
                if (filter.BusinessType.HasValue)
                    queryable = queryable.Where(t => t.BusinessType == filter.BusinessType.Value);
            }
            var groupData = queryable.GroupBy(t => t.TradeDepartmentID)
                .Select(group => new
                {
                    Cash = group.Sum(g => (decimal?)(g.PaymentType == EPaymentType.Cash ? g.TradeAmount : 0)),
                    BankCard = group.Sum(g => (decimal?)(g.PaymentType == EPaymentType.BankCard ? g.TradeAmount : 0)),
                    WeChat = group.Sum(g => (decimal?)(g.PaymentType == EPaymentType.WeChat ? g.TradeAmount : 0)),
                    Alipay = group.Sum(g => (decimal?)(g.PaymentType == EPaymentType.Alipay ? g.TradeAmount : 0)),
                }).FirstOrDefault();
            var totalData = new RechargeTotalDataDto();
            totalData.DepartmentCode = filter.DepartmentCode;
            if (groupData != null)
            {
                totalData.Cash = groupData.Cash.GetValueOrDefault();
                totalData.BankCard = groupData.BankCard.GetValueOrDefault();
                totalData.WeChat = groupData.WeChat.GetValueOrDefault();
                totalData.Alipay = groupData.Alipay.GetValueOrDefault();
            }
            return totalData;
        }

        //public int Count(Expression<Func<RechargeHistory, bool>> predicate)
        //{
        //    return Repository.Count(predicate);
        //}

        /// <summary>
        /// 开发票
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool DrawReceipt(RechargeHistory entity)
        {
            var originalEntity = Get(entity.ID);
            originalEntity.HasDrawReceipt = entity.HasDrawReceipt;
            originalEntity.DrawReceiptDepartment = originalEntity.HasDrawReceipt ? AppContext.CurrentSession.DepartmentName : "";
            originalEntity.DrawReceiptUser = originalEntity.HasDrawReceipt ? AppContext.CurrentSession.UserName : "";
            originalEntity.DrawReceiptMoney = entity.DrawReceiptMoney;
            if (originalEntity.HasDrawReceipt)
            {
                if (originalEntity.DrawReceiptMoney <= 0)
                    throw new DomainException("开票金额不能小于或等于零！");
                if (originalEntity.TradeAmount < originalEntity.DrawReceiptMoney)
                    throw new DomainException("开票金额不能大于交易金额！");
                originalEntity.DrawReceiptUserId = AppContext.CurrentSession.UserID;
            }
            else
            {
                originalEntity.DrawReceiptMoney = 0;
                originalEntity.DrawReceiptUserId = null;
            }
            return base.Update(originalEntity);
        }

        #endregion
    }
}
