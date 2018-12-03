using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.BaseData.Domain.Entities;
using VVCar.Shop.Domain.Entities;
using VVCar.Shop.Domain.Filters;
using VVCar.Shop.Domain.Services;
using YEF.Core;
using YEF.Core.Data;
using YEF.Core.Domain;

namespace VVCar.Shop.Services.DomainServices
{
    /// <summary>
    /// 日常支出维护报表
    /// </summary>
    public class DailyExpenseService : DomainServiceBase<IRepository<DailyExpense>, DailyExpense, Guid>, IDailyExpenseService
    {
        public DailyExpenseService()
        {
        }

        IRepository<User> UserRepo { get => UnitOfWork.GetRepository<IRepository<User>>(); }

        public override DailyExpense Add(DailyExpense entity)
        {
            if (entity == null)
                return null;
            var count = Repository.GetQueryable(false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID && t.ExpenseDate == entity.ExpenseDate).ToList().Count();
            if (count > 0)
                throw new DomainException(entity.ExpenseDate.ToDateString() + "数据已维护");
            entity.ID = Util.NewID();
            var userCount = UserRepo.GetQueryable(false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID).Count();
            entity.StaffCount = userCount;
            entity.CreatedDate = DateTime.Now;
            entity.CreatedUser = AppContext.CurrentSession.UserCode;
            entity.CreatedUserID = AppContext.CurrentSession.UserID;
            entity.MerchantID = AppContext.CurrentSession.MerchantID;
            return base.Add(entity);
        }

        public override bool Delete(Guid key)
        {
            var entity = Repository.GetByKey(key);
            if (entity == null)
                throw new DomainException("数据不存在");
            entity.IsDeleted = true;
            return Repository.Update(entity) > 0;
        }

        public override bool Update(DailyExpense entity)
        {
            if (entity == null)
                return false;
            var dailyExpense = Repository.GetByKey(entity.ID);
            dailyExpense.ExpenseDate = entity.ExpenseDate;
            dailyExpense.StaffCount = entity.StaffCount;
            dailyExpense.Money = entity.Money;
            dailyExpense.Remark = entity.Remark;
            dailyExpense.LastUpdateDate = DateTime.Now;
            dailyExpense.LastUpdateUser = AppContext.CurrentSession.UserCode;
            dailyExpense.LastUpdateUserID = AppContext.CurrentSession.UserID;
            return base.Update(entity);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool BatchDelete(Guid[] ids)
        {
            if (ids == null || ids.Length < 1)
                throw new DomainException("参数错误");
            var dailyExpenseList = Repository.GetQueryable(false).Where(t => ids.Contains(t.ID)).ToList();
            if (dailyExpenseList == null || dailyExpenseList.Count() < 1)
                throw new DomainException("数据不存在");
            dailyExpenseList.ForEach(t =>
            {
                t.IsDeleted = true;
            });
            return Repository.Update(dailyExpenseList) > 0;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public IEnumerable<DailyExpense> Search(DailyExpenseFilter filter, out int totalCount)
        {
            var queryable = Repository.GetQueryable(false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID);
            if (filter.ExpenseDate.HasValue)
                queryable = queryable.Where(t => t.ExpenseDate == filter.ExpenseDate);
            totalCount = queryable.Count();
            if (filter.Start.HasValue && filter.Limit.HasValue)
                queryable = queryable.OrderByDescending(t => t.CreatedDate).Skip(filter.Start.Value).Take(filter.Limit.Value);
            return queryable.OrderByDescending(t => t.ExpenseDate).ToArray();
        }
    }
}
