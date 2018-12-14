using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVCar.Shop.Domain.Dtos;
using VVCar.Shop.Domain.Services;
using YEF.Core.Data;
using VVCar.Shop.Domain.Entities;
using YEF.Core;
using VVCar.BaseData.Domain.Entities;
using VVCar.Shop.Domain.Filters;
using VVCar.VIP.Domain.Entities;
using VVCar.Shop.Domain.Enums;
using VVCar.VIP.Domain.Dtos;
using VVCar.BaseData.Domain.Services;
using VVCar.BaseData.Domain;
using VVCar.VIP.Domain.Services;
using VVCar.BaseData.Services;
using VVCar.VIP.Domain.Enums;

namespace VVCar.Shop.Services.DomainServices
{
    /// <summary>
    /// 报表服务
    /// </summary>
    public class ReportingService : IReportingService
    {
        /// <summary>
        /// constructor
        /// </summary>
        public ReportingService()
        {
        }

        #region properties

        IRepository<Reimbursement> ReimbursementRepo { get => ServiceLocator.Instance.GetService<IRepository<Reimbursement>>(); }

        IRepository<PickUpOrder> PickUpOrderRepo { get => ServiceLocator.Instance.GetService<IRepository<PickUpOrder>>(); }

        IRepository<PickUpOrderItem> PickUpOrderItemRepo { get => ServiceLocator.Instance.GetService<IRepository<PickUpOrderItem>>(); }

        IRepository<Order> OrderRepo { get => ServiceLocator.Instance.GetService<IRepository<Order>>(); }

        IRepository<OrderItem> OrderItemRepo { get => ServiceLocator.Instance.GetService<IRepository<OrderItem>>(); }

        IRepository<DailyExpense> DailyExpenseRepo { get => ServiceLocator.Instance.GetService<IRepository<DailyExpense>>(); }

        IRepository<Product> ProductRepo { get => ServiceLocator.Instance.GetService<IRepository<Product>>(); }

        IRepository<User> UserRepo { get => ServiceLocator.Instance.GetService<IRepository<User>>(); }

        IRepository<UserRole> UserRoleRepo { get => ServiceLocator.Instance.GetService<IRepository<UserRole>>(); }

        IRepository<Member> MemberRepo { get => ServiceLocator.Instance.GetService<IRepository<Member>>(); }

        IRepository<AgentDepartment> AgentDepartmentRepo { get => ServiceLocator.Instance.GetService<IRepository<AgentDepartment>>(); }

        IRepository<ConsumeHistory> ConsumeHistoryRepo { get => ServiceLocator.Instance.GetService<IRepository<ConsumeHistory>>(); }

        IRepository<PickUpOrderTaskDistribution> PickUpOrderTaskDistributionRepo { get => ServiceLocator.Instance.GetService<IRepository<PickUpOrderTaskDistribution>>(); }

        IRepository<UserMember> UserMemberRepo { get => ServiceLocator.Instance.GetService<IRepository<UserMember>>(); }

        IRepository<UnsaleProductSettingItem> UnsaleProductSettingItemRepo { get => ServiceLocator.Instance.GetService<IRepository<UnsaleProductSettingItem>>(); }

        IRepository<Merchant> MerchantRepo { get => ServiceLocator.Instance.GetService<IRepository<Merchant>>(); }

        IRepository<MemberPlate> MemberPlateRepo { get => ServiceLocator.Instance.GetService<IRepository<MemberPlate>>(); }

        IRepository<OrderDividend> OrderDividendRepo { get => ServiceLocator.Instance.GetService<IRepository<OrderDividend>>(); }

        IRepository<CouponTemplate> CouponTemplateRepo { get => ServiceLocator.Instance.GetService<IRepository<CouponTemplate>>(); }

        IRepository<UnsaleProductHistory> UnsaleProductHistoryRepo { get => ServiceLocator.Instance.GetService<IRepository<UnsaleProductHistory>>(); }

        IWeChatService WeChatService { get => ServiceLocator.Instance.GetService<IWeChatService>(); }

        ISystemSettingService SystemSettingService { get => ServiceLocator.Instance.GetService<ISystemSettingService>(); }

        #endregion

        /// <summary>
        /// 营业额报表
        /// </summary>
        /// <returns></returns>
        public TurnoverReportingDto GetTurnoverReporting()
        {
            var result = new TurnoverReportingDto();
            result.PickUpOrderTurnover = PickUpOrderTurnover();
            result.ShopTurnover = ShopTurnover();
            result.MemberCount = MemberCount();

            var totalturnover = result.PickUpOrderTurnover;
            //totalturnover.ForEach(t =>
            //{
            //    var sameday = result.ShopTurnover.Where(s => s.Unit == t.Unit).FirstOrDefault();
            //    if (sameday != null)
            //        t.Turnover += sameday.Turnover;
            //});

            totalturnover.AddRange(result.ShopTurnover);
            totalturnover = totalturnover.GroupBy(t => t.Unit).Select(t => new TurnoverDto
            {
                Unit = t.Key,
                Turnover = t.Sum(s => s.Turnover),
            }).ToList();

            result.TotalTurnover = totalturnover;

            var today = DateTime.Now.Date;
            var nextDay = today.AddDays(1);

            var pickUpOrderTotalTurnover = PickUpOrderRepo.GetQueryable(false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID).GroupBy(g => 1).Select(t => t.Sum(s => s.Money)).FirstOrDefault();
            var orderTotalTurnover = OrderRepo.GetQueryable(false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID).GroupBy(g => 1).Select(t => t.Sum(s => s.Money)).FirstOrDefault();
            var todayPickUpOrderTotalTurnover = PickUpOrderRepo.GetQueryable(false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID && t.CreatedDate >= today && t.CreatedDate < nextDay).GroupBy(g => 1).Select(t => t.Sum(s => s.Money)).FirstOrDefault();
            var todayOrderTotalTurnover = OrderRepo.GetQueryable(false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID && t.CreatedDate >= today && t.CreatedDate < nextDay).GroupBy(g => 1).Select(t => t.Sum(s => s.Money)).FirstOrDefault();

            var memberTotalCount = MemberRepo.GetQueryable(false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID).Count();
            var todayMemberCount = MemberRepo.GetQueryable(false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID && t.CreatedDate >= today && t.CreatedDate < nextDay).Count();

            result.TotalMarketTurnover = pickUpOrderTotalTurnover + orderTotalTurnover;
            result.TodayTurnover = todayPickUpOrderTotalTurnover + todayOrderTotalTurnover;
            result.TotalMember = memberTotalCount;
            result.TodayMember = todayMemberCount;

            return result;
        }

        /// <summary>
        /// 接车单营业额
        /// </summary>
        /// <returns></returns>
        public List<TurnoverDto> PickUpOrderTurnover()
        {
            var result = new List<TurnoverDto>();

            var starttime = DateTime.Now.AddDays(-6).Date;
            var now = DateTime.Now.Date;
            var pickuporderlist = PickUpOrderRepo.GetQueryable(false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID && t.CreatedDate > starttime).ToList();

            while (starttime <= now)
            {
                var pickuporders = pickuporderlist.Where(t => t.CreatedDate.Date == starttime.Date).ToList();
                result.Add(new TurnoverDto
                {
                    Unit = starttime.Date.Day,
                    Turnover = pickuporders.GroupBy(g => 1).Select(t => t.Sum(s => s.ReceivedMoney)).FirstOrDefault(),
                });
                starttime = starttime.AddDays(1);
            }

            return result;
        }

        /// <summary>
        /// 商城营业额
        /// </summary>
        /// <returns></returns>
        public List<TurnoverDto> ShopTurnover()
        {
            var result = new List<TurnoverDto>();

            var starttime = DateTime.Now.AddDays(-6).Date;
            var now = DateTime.Now.Date;
            var orderlist = OrderRepo.GetQueryable(false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID && t.CreatedDate > starttime).ToList();

            while (starttime <= now)
            {
                var orders = orderlist.Where(t => t.CreatedDate.Date == starttime.Date).ToList();
                result.Add(new TurnoverDto
                {
                    Unit = starttime.Date.Day,
                    Turnover = orders.GroupBy(g => 1).Select(t => t.Sum(s => s.ReceivedMoney)).FirstOrDefault(),
                });
                starttime = starttime.AddDays(1);
            }

            return result;
        }

        public List<TurnoverDto> MemberCount()
        {
            var result = new List<TurnoverDto>();

            var starttime = DateTime.Now.AddDays(-6).Date;
            var now = DateTime.Now.Date;
            var memberlist = MemberRepo.GetQueryable(false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID && t.CreatedDate > starttime).ToList();

            while (starttime <= now)
            {
                result.Add(new TurnoverDto
                {
                    Unit = starttime.Date.Day,
                    Count = memberlist.Where(t => t.CreatedDate.Date == starttime.Date).Count(),
                });
                starttime = starttime.AddDays(1);
            }

            return result;
        }

        /// <summary>
        /// 月营业额
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public MonthTurnoverDto GetMonthTurnover(DateTime date)
        {
            if (date == null)
                throw new DomainException("参数错误");
            var result = new MonthTurnoverDto();

            var starttime = new DateTime(date.Year, date.Month, 1);
            var endtime = starttime.AddMonths(1);
            var pickuporderlist = PickUpOrderRepo.GetQueryable(false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID && t.CreatedDate >= starttime && t.CreatedDate < endtime).ToList();
            var orderlist = OrderRepo.GetQueryable(false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID && t.CreatedDate >= starttime && t.CreatedDate < endtime).ToList();

            result.PickUpOrderTurnover = pickuporderlist.GroupBy(g => 1).Select(t => t.Sum(s => s.ReceivedMoney)).FirstOrDefault();
            result.ShopTurnover = orderlist.GroupBy(g => 1).Select(t => t.Sum(s => s.ReceivedMoney)).FirstOrDefault();
            result.TotalTurnover = result.PickUpOrderTurnover + result.ShopTurnover;

            return result;
        }

        /// <summary>
        /// 获取员工绩效
        /// </summary>
        /// <returns></returns>
        public StaffPerformance GetStaffPerformance(Guid userId, DateTime? date)
        {
            var staffPerformance = new StaffPerformance();

            var user = UserRepo.GetByKey(userId, false);
            if (user == null)
                throw new DomainException("员工信息不存在");

            var starttime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var endtime = starttime.AddMonths(1);
            if (date.HasValue)
            {
                starttime = new DateTime(date.Value.Year, date.Value.Month, 1);
                endtime = starttime.AddMonths(1);
            }
            //var pickuporderqueryable = PickUpOrderRepo.GetIncludes(false, "PickUpOrderItemList", "PickUpOrderItemList.Product").Where(t => t.StaffID == user.ID && t.MerchantID == AppContext.CurrentSession.MerchantID).ToList();
            //var monthpickuporderlist = pickuporderqueryable.Where(t => t.CreatedDate >= starttime && t.CreatedDate < endtime).ToList();

            //staffPerformance.MonthPerformance = monthpickuporderlist.GroupBy(g => 1).Select(t => t.Sum(s => s.Money)).FirstOrDefault();
            //staffPerformance.TotalPerformance = pickuporderqueryable.GroupBy(g => 1).Select(t => t.Sum(s => s.Money)).FirstOrDefault();
            //staffPerformance.CustomerServiceCount = pickuporderqueryable.Count();
            //staffPerformance.MonthCustomerServiceCount = monthpickuporderlist.Count();

            //pickuporderqueryable.Select(t => t.PickUpOrderItemList).ForEach(t =>
            //{
            //    staffPerformance.TotalCommission += t.GroupBy(g => 1).Select(item => item.Sum(s => s.Product.PriceSale * s.Product.CommissionRate / 100)).FirstOrDefault();
            //});
            //monthpickuporderlist.Select(t => t.PickUpOrderItemList).ForEach(t =>
            //{
            //    staffPerformance.MonthCommission += t.GroupBy(g => 1).Select(item => item.Sum(s => s.Product.PriceSale * s.Product.CommissionRate / 100)).FirstOrDefault();
            //});
            //staffPerformance.BasicSalary = user.BasicSalary;

            //staffPerformance.TotalOpenAccountCount = AgentDepartmentRepo.GetQueryable(false).Where(t => t.UserID == user.ID).Count();
            //staffPerformance.MonthOpenAccountCount = AgentDepartmentRepo.GetQueryable(false).Where(t => t.UserID == user.ID && t.CreatedDate >= starttime && t.CreatedDate < endtime).Count();

            var orderDividendList = OrderDividendRepo.GetQueryable(false).Where(t => t.UserID == user.ID && t.MerchantID == AppContext.CurrentSession.MerchantID).ToList();
            var monthOrderDividendList = orderDividendList.Where(t => t.CreatedDate >= starttime && t.CreatedDate < endtime).ToList();

            var pickUpOrderTotalCount = orderDividendList.Where(t => t.OrderType == EShopTradeOrderType.PickupOrder).Select(t => t.TradeNo).Distinct().Count();
            var pickUpOrderMonthCount = orderDividendList.Where(t => t.OrderType == EShopTradeOrderType.PickupOrder && t.CreatedDate >= starttime && t.CreatedDate < endtime).Select(t => t.TradeNo).Distinct().Count();

            staffPerformance.MonthPerformance = monthOrderDividendList.GroupBy(g => 1).Select(t => t.Sum(s => s.Money)).FirstOrDefault();
            staffPerformance.TotalPerformance = orderDividendList.GroupBy(g => 1).Select(t => t.Sum(s => s.Money)).FirstOrDefault();
            staffPerformance.CustomerServiceCount = pickUpOrderTotalCount;
            staffPerformance.MonthCustomerServiceCount = pickUpOrderMonthCount;

            staffPerformance.TotalCommission = orderDividendList.GroupBy(g => 1).Select(t => t.Sum(s => s.Commission)).FirstOrDefault();
            staffPerformance.MonthCommission = monthOrderDividendList.GroupBy(g => 1).Select(t => t.Sum(s => s.Commission)).FirstOrDefault();

            staffPerformance.BasicSalary = user.BasicSalary;

            staffPerformance.TotalOpenAccountCount = AgentDepartmentRepo.GetQueryable(false).Where(t => t.UserID == user.ID).Count();
            staffPerformance.MonthOpenAccountCount = AgentDepartmentRepo.GetQueryable(false).Where(t => t.UserID == user.ID && t.CreatedDate >= starttime && t.CreatedDate < endtime).Count();

            return staffPerformance;
        }

        /// <summary>
        /// 员工业绩统计
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public IEnumerable<StaffPerformance> StaffPerformanceStatistics(StaffPerformanceFilter filter, ref int totalCount)
        {
            //var result = new List<StaffPerformance>();
            //var userQueryable = UserRepo.GetQueryable(false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID);
            //if (filter != null && !string.IsNullOrEmpty(filter.StaffName))
            //    userQueryable = userQueryable.Where(t => t.Name.Contains(filter.StaffName));
            //var users = userQueryable.ToList();
            //if (users != null && users.Count > 0)
            //{
            //    var now = DateTime.Now;
            //    var monthStartTime = new DateTime(now.Year, now.Month, 1);
            //    var nextMonthStartTime = monthStartTime.AddMonths(1);
            //    var pickuporderQueryable = PickUpOrderRepo.GetIncludes(false, "PickUpOrderItemList", "PickUpOrderItemList.Product").Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID);
            //    var currentpickuporderlist = pickuporderQueryable;
            //    var monthpickuporderlist = pickuporderQueryable.Where(t => t.CreatedDate >= monthStartTime && t.CreatedDate < nextMonthStartTime);
            //    if (filter != null)
            //    {
            //        if (filter.StartDate.HasValue)
            //            currentpickuporderlist = currentpickuporderlist.Where(t => t.CreatedDate >= filter.StartDate.Value);
            //        if (filter.EndDate.HasValue)
            //            currentpickuporderlist = currentpickuporderlist.Where(t => t.CreatedDate < filter.EndDate.Value);
            //    }
            //    users.ForEach(user =>
            //    {
            //        var userpickuporder = pickuporderQueryable.Where(t => t.StaffID == user.ID).ToList();
            //        var monthuserpickuporder = monthpickuporderlist.Where(t => t.StaffID == user.ID).ToList();
            //        var currentuserpickuporder = currentpickuporderlist.Where(t => t.StaffID == user.ID).ToList();

            //        var staffPerformance = new StaffPerformance();

            //        staffPerformance.TotalPerformance = userpickuporder.GroupBy(g => 1).Select(t => t.Sum(s => s.Money)).FirstOrDefault();
            //        staffPerformance.MonthPerformance = monthuserpickuporder.GroupBy(g => 1).Select(t => t.Sum(s => s.Money)).FirstOrDefault(); ;
            //        staffPerformance.CurrentPerformance = currentuserpickuporder.GroupBy(g => 1).Select(t => t.Sum(s => s.Money)).FirstOrDefault();

            //        staffPerformance.CustomerServiceCount = userpickuporder.Count();
            //        staffPerformance.MonthCustomerServiceCount = monthuserpickuporder.Count();
            //        staffPerformance.CurrentCustomerServiceCount = currentuserpickuporder.Count();

            //        userpickuporder.Select(t => t.PickUpOrderItemList).ForEach(t =>
            //        {
            //            staffPerformance.TotalCommission += t.GroupBy(g => 1).Select(item => item.Sum(s => s.Product.PriceSale * s.Product.CommissionRate / 100)).FirstOrDefault();
            //        });
            //        monthuserpickuporder.Select(t => t.PickUpOrderItemList).ForEach(t =>
            //        {
            //            staffPerformance.MonthCommission += t.GroupBy(g => 1).Select(item => item.Sum(s => s.Product.PriceSale * s.Product.CommissionRate / 100)).FirstOrDefault();
            //        });
            //        currentuserpickuporder.Select(t => t.PickUpOrderItemList).ForEach(t =>
            //        {
            //            staffPerformance.CurrentCommission += t.GroupBy(g => 1).Select(item => item.Sum(s => s.Product.PriceSale * s.Product.CommissionRate / 100)).FirstOrDefault();
            //        });

            //        staffPerformance.BasicSalary = user.BasicSalary;
            //        staffPerformance.Subsidy = 0;

            //        staffPerformance.StaffID = user.ID;
            //        staffPerformance.StaffName = user.Name;
            //        staffPerformance.StaffCode = user.Code;

            //        staffPerformance.TotalOpenAccountCount = AgentDepartmentRepo.GetQueryable(false).Where(t => t.UserID == user.ID).Count();
            //        staffPerformance.MonthOpenAccountCount = AgentDepartmentRepo.GetQueryable(false).Where(t => t.UserID == user.ID && t.CreatedDate >= monthStartTime && t.CreatedDate < nextMonthStartTime).Count();

            //        result.Add(staffPerformance);
            //    });
            //}
            //totalCount = result.Count();
            //if (filter != null && filter.Start.HasValue && filter.Limit.HasValue)
            //    result = result.OrderByDescending(t => t.CurrentPerformance).Skip(filter.Start.Value).Take(filter.Limit.Value).ToList();
            //return result.OrderByDescending(t => t.CurrentPerformance).ToList();
            var result = new List<StaffPerformance>();
            var userQueryable = UserRepo.GetQueryable(false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID);
            if (!string.IsNullOrEmpty(filter.StaffName))
                userQueryable = userQueryable.Where(t => t.Name.Contains(filter.StaffName) || t.Code.Contains(filter.StaffName));
            var orderDividendQueryable = OrderDividendRepo.GetQueryable(false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID);
            var now = DateTime.Now;
            var monthStartTime = new DateTime(now.Year, now.Month, 1);
            var nextMonthStartTime = monthStartTime.AddMonths(1);
            var currentOrderDividendQueryable = orderDividendQueryable;
            var monthOrderDividendQueryable = orderDividendQueryable.Where(t => t.CreatedDate >= monthStartTime && t.CreatedDate < nextMonthStartTime);
            if (filter.StartDate.HasValue)
                currentOrderDividendQueryable = currentOrderDividendQueryable.Where(t => t.CreatedDate >= filter.StartDate.Value);
            if (filter.EndDate.HasValue)
                currentOrderDividendQueryable = currentOrderDividendQueryable.Where(t => t.CreatedDate < filter.EndDate.Value);
            var users = userQueryable.ToList();
            users.ForEach(t =>
            {
                var userOrderDividendList = orderDividendQueryable.Where(dividend => dividend.UserID == t.ID).ToList();
                var userMonthOrderDividendList = monthOrderDividendQueryable.Where(dividend => dividend.UserID == t.ID).ToList();
                var userCurrentOrderDividendList = currentOrderDividendQueryable.Where(dividend => dividend.UserID == t.ID).ToList();

                var staffPerformance = new StaffPerformance();
                staffPerformance.TotalPerformance = userOrderDividendList.GroupBy(g => 1).Select(sl => sl.Sum(s => s.Money)).FirstOrDefault();
                staffPerformance.MonthPerformance = userMonthOrderDividendList.GroupBy(g => 1).Select(sl => sl.Sum(s => s.Money)).FirstOrDefault();
                staffPerformance.CurrentPerformance = userCurrentOrderDividendList.GroupBy(g => 1).Select(sl => sl.Sum(s => s.Money)).FirstOrDefault();

                staffPerformance.CustomerServiceCount = userOrderDividendList.Where(m => m.OrderType == EShopTradeOrderType.PickupOrder).Count();
                staffPerformance.MonthCustomerServiceCount = userMonthOrderDividendList.Where(m => m.OrderType == EShopTradeOrderType.PickupOrder).Count();
                staffPerformance.CurrentCustomerServiceCount = userCurrentOrderDividendList.Where(m => m.OrderType == EShopTradeOrderType.PickupOrder).Count();

                staffPerformance.TotalCommission = userOrderDividendList.GroupBy(g => 1).Select(sl => sl.Sum(s => s.Commission)).FirstOrDefault();
                staffPerformance.MonthCommission = userMonthOrderDividendList.GroupBy(g => 1).Select(sl => sl.Sum(s => s.Commission)).FirstOrDefault();
                staffPerformance.CurrentCommission = userCurrentOrderDividendList.GroupBy(g => 1).Select(sl => sl.Sum(s => s.Commission)).FirstOrDefault();

                staffPerformance.BasicSalary = t.BasicSalary;
                staffPerformance.Subsidy = 0;

                staffPerformance.StaffID = t.ID;
                staffPerformance.StaffName = t.Name;
                staffPerformance.StaffCode = t.Code;

                staffPerformance.TotalOpenAccountCount = AgentDepartmentRepo.GetQueryable(false).Where(m => m.UserID == t.ID).Count();
                staffPerformance.MonthOpenAccountCount = AgentDepartmentRepo.GetQueryable(false).Where(m => m.UserID == t.ID).Count();

                result.Add(staffPerformance);
            });
            totalCount = result.Count();
            if (filter.Start.HasValue && filter.Limit.HasValue)
                result = result.OrderByDescending(t => t.CurrentPerformance).Skip(filter.Start.Value).Take(filter.Limit.Value).ToList();
            return result.OrderByDescending(t => t.CurrentPerformance).ToList();
        }

        /// <summary>
        /// 员工个人产值
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public IEnumerable<StaffOutputValue> StaffOutputValueStatistics(StaffOutputValueFilter filter, out int totalCount)
        {
            var result = new List<StaffOutputValue>();
            var userQueryable = UserRepo.GetQueryable(false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID);
            var dailyExpenseQueryable = DailyExpenseRepo.GetQueryable(false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID);
            if (!string.IsNullOrEmpty(filter.StaffName))
                userQueryable = userQueryable.Where(t => t.Name.Contains(filter.StaffName) || t.Code.Contains(filter.StaffName));
            var currentOrderDividendQueryable = OrderDividendRepo.GetQueryable(false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID);
            if (filter.StartDate.HasValue)
            {
                dailyExpenseQueryable = dailyExpenseQueryable.Where(t => t.ExpenseDate >= filter.StartDate.Value);
                currentOrderDividendQueryable = currentOrderDividendQueryable.Where(t => t.CreatedDate >= filter.StartDate.Value);
            }
            if (filter.EndDate.HasValue)
            {
                dailyExpenseQueryable = dailyExpenseQueryable.Where(t => t.ExpenseDate < filter.EndDate.Value);
                currentOrderDividendQueryable = currentOrderDividendQueryable.Where(t => t.CreatedDate < filter.EndDate.Value);
            }
            var dailyExpenseList = dailyExpenseQueryable.ToList();
            var dailyExpense = dailyExpenseList.GroupBy(g => 1).Select(sl => sl.Sum(s => s.Money)).FirstOrDefault();
            var users = userQueryable.ToList();
            users.ForEach(t =>
            {
                var averageDailyExpense = dailyExpenseList.GroupBy(g => 1).Select(sl => sl.Sum(s => Math.Ceiling((s.Money / s.StaffCount) * 100)) / 100).FirstOrDefault();
                if (t.DutyTime.HasValue)
                {
                    averageDailyExpense = dailyExpenseList.Where(d => d.ExpenseDate >= t.DutyTime.Value).ToList().GroupBy(g => 1).Select(sl => sl.Sum(s => Math.Ceiling((s.Money / s.StaffCount) * 100)) / 100).FirstOrDefault();
                }
                var userCurrentOrderDividendList = currentOrderDividendQueryable.Where(dividend => dividend.UserID == t.ID).ToList();

                var staffOutputValue = new StaffOutputValue();
                staffOutputValue.TotalPerformance = userCurrentOrderDividendList.GroupBy(g => 1).Select(sl => sl.Sum(s => s.Money)).FirstOrDefault();
                staffOutputValue.TotalCostMoney = userCurrentOrderDividendList.GroupBy(g => 1).Select(sl => sl.Sum(s => s.CostMoney)).FirstOrDefault();
                staffOutputValue.TotalProfit = staffOutputValue.TotalPerformance - staffOutputValue.TotalCostMoney;
                staffOutputValue.DailyExpense = dailyExpense;
                staffOutputValue.AverageDailyExpense = averageDailyExpense;
                staffOutputValue.TotalRetaainedProfit = staffOutputValue.TotalProfit - staffOutputValue.AverageDailyExpense;

                staffOutputValue.StaffID = t.ID;
                staffOutputValue.StaffName = t.Name;
                staffOutputValue.StaffCode = t.Code;

                result.Add(staffOutputValue);
            });
            totalCount = result.Count();
            if (filter.Start.HasValue && filter.Limit.HasValue)
                result = result.OrderByDescending(t => t.TotalRetaainedProfit).Skip(filter.Start.Value).Take(filter.Limit.Value).ToList();
            return result.OrderByDescending(t => t.TotalRetaainedProfit).ToList();
        }

        /// <summary>
        /// 门店开发业绩统计
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public IEnumerable<DepartmentPerformance> DepartmentPerformanceStatistics(DepartmentPerformanceFilter filter, ref int totalCount)
        {
            var result = new List<DepartmentPerformance>();
            var salesmanager = Guid.Parse("00000000-0000-0000-0000-000000000005");
            var generalmanager = Guid.Parse("00000000-0000-0000-0000-000000000006");
            var saleUserIDs = UserRoleRepo.GetQueryable(false).Where(t => (t.RoleID == salesmanager || t.RoleID == generalmanager)).Select(t => t.UserID).Distinct();
            var agentDepartmentQueryable = AgentDepartmentRepo.GetInclude(t => t.User, false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID);
            var userQueryable = UserRepo.GetQueryable(false).Where(t => (saleUserIDs.Contains(t.ID) && t.MerchantID == AppContext.CurrentSession.MerchantID));
            if (filter != null && !string.IsNullOrEmpty(filter.StaffName))
                userQueryable = userQueryable.Where(t => t.Name.Contains(filter.StaffName));
            var userList = userQueryable.ToList();
            if (userList != null && userList.Count > 0)
            {
                var now = DateTime.Now;
                var monthStartTime = new DateTime(now.Year, now.Month, 1);
                var nextMonthStartTime = monthStartTime.AddMonths(1);
                var currentAgentDepartmentQueryable = agentDepartmentQueryable;
                var monthAgentDepartmentQueryable = agentDepartmentQueryable.Where(t => t.CreatedDate >= monthStartTime && t.CreatedDate < nextMonthStartTime);
                if (filter != null)
                {
                    if (filter.StartDate.HasValue)
                        currentAgentDepartmentQueryable = currentAgentDepartmentQueryable.Where(t => t.CreatedDate >= filter.StartDate.Value);
                    if (filter.EndDate.HasValue)
                        currentAgentDepartmentQueryable = currentAgentDepartmentQueryable.Where(t => t.CreatedDate < filter.EndDate.Value);
                }
                userList.ForEach(user =>
                {
                    var userAgentDepartmentList = agentDepartmentQueryable.Where(t => t.UserID == user.ID).ToList();
                    var monthUserAgentDepartmentList = monthAgentDepartmentQueryable.Where(t => t.UserID == user.ID).ToList();
                    var currentUserAgentDepartmentList = currentAgentDepartmentQueryable.Where(t => t.UserID == user.ID).ToList();

                    var departmentPerformance = new DepartmentPerformance();
                    departmentPerformance.TotalDepartmentNumber = userAgentDepartmentList.Count;
                    departmentPerformance.CurrentDepartmentNumber = currentUserAgentDepartmentList.Count;
                    departmentPerformance.MonthDepartmentNumber = monthUserAgentDepartmentList.Count;


                    departmentPerformance.StaffID = user.ID;
                    departmentPerformance.StaffName = user.Name;
                    departmentPerformance.StaffCode = user.Code;

                    result.Add(departmentPerformance);
                });
            }
            totalCount = result.Count();
            if (filter != null && filter.StartDate.HasValue && filter.EndDate.HasValue)
                result = result.OrderByDescending(t => t.CurrentDepartmentNumber).Skip(filter.Start.Value).Take(filter.Limit.Value).ToList();
            return result.OrderByDescending(t => t.CurrentDepartmentNumber);
        }

        /// <summary>
        /// 零售产品汇总统计
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public IEnumerable<ProductRetailStatisticsDto> ProductRetailStatistics(ProductRetailStatisticsFilter filter, ref int totalCount)
        {
            //var result = new List<ProductRetailStatisticsDto>();

            //var orderItemQueryable = OrderItemRepo.GetQueryable(false);
            //var pickUpOrderItemQueryable = PickUpOrderItemRepo.GetInclude(t => t.Product, false);

            //if (filter != null)
            //{
            //    if(!string.IsNullOrEmpty(filter.PlateNumber))
            //    {
            //        var memberPlate = MemberPlateRepo.GetQueryable(false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID && t.PlateNumber == filter.PlateNumber).FirstOrDefault();
            //        if(memberPlate != null)
            //            orderItemQueryable = orderItemQueryable.Where(t => t.Order.MemberID == memberPlate.MemberID);
            //        else
            //            orderItemQueryable = orderItemQueryable.Where(t => 1==0);
            //        pickUpOrderItemQueryable = pickUpOrderItemQueryable.Where(t => t.PickUpOrder.PlateNumber == filter.PlateNumber);
            //    }

            //    if (!string.IsNullOrEmpty(filter.ProductCodeName))
            //    {
            //        var productIDs = ProductRepo.GetQueryable(false).Where(t => t.Code.Contains(filter.ProductCodeName) || t.Name.Contains(filter.ProductCodeName)).Select(t=>t.ID).Distinct();
            //        orderItemQueryable = orderItemQueryable.Where(t => productIDs.Contains(t.GoodsID));
            //        pickUpOrderItemQueryable = pickUpOrderItemQueryable.Where(t => productIDs.Contains(t.ProductID));
            //    }

            //    if (filter.StartDate.HasValue)
            //    {
            //        orderItemQueryable = orderItemQueryable.Where(t => t.Order.CreatedDate >= filter.StartDate.Value);
            //        pickUpOrderItemQueryable = pickUpOrderItemQueryable.Where(t => t.PickUpOrder.CreatedDate >= filter.StartDate.Value);
            //    }
            //    if (filter.EndDate.HasValue)
            //    {
            //        orderItemQueryable = orderItemQueryable.Where(t => t.Order.CreatedDate < filter.EndDate.Value);
            //        pickUpOrderItemQueryable = pickUpOrderItemQueryable.Where(t => t.PickUpOrder.CreatedDate < filter.EndDate.Value);
            //    }
            //    if (filter.ProductType.HasValue)
            //    {
            //        orderItemQueryable = orderItemQueryable.Where(t => t.ProductType == filter.ProductType.Value);
            //        pickUpOrderItemQueryable = pickUpOrderItemQueryable.Where(t => t.Product.ProductType == filter.ProductType.Value);
            //    }
            //}

            //var orderItemList = orderItemQueryable.Where(t => t.Order.MerchantID == AppContext.CurrentSession.MerchantID && t.ProductType != Domain.Enums.EProductType.MemberCard).ToList();
            //var orderItemGroup = orderItemList.GroupBy(g => g.GoodsID).Select(t => new
            //{
            //    ProductID = t.Key,
            //    Quantity = t.Sum(s => s.Quantity),
            //    ProductName = t.FirstOrDefault() != null ? t.FirstOrDefault().ProductName : "",
            //    ProductCode = "",
            //    ProductType = t.FirstOrDefault() != null ? t.FirstOrDefault().ProductType : Domain.Enums.EProductType.Goods,
            //    Money = t.Sum(s => s.Money),
            //}).ToList();

            //var pickUpOrderList = pickUpOrderItemQueryable.Where(t => t.PickUpOrder.MerchantID == AppContext.CurrentSession.MerchantID).ToList();
            //var pickUpOrderGroup = pickUpOrderList.GroupBy(g => g.ProductID).Select(t => new
            //{
            //    ProductID = t.Key,
            //    Quantity = t.Sum(s => s.Quantity),
            //    ProductName = t.FirstOrDefault() != null ? t.FirstOrDefault().ProductName : "",
            //    ProductCode = t.FirstOrDefault() != null ? t.FirstOrDefault().ProductCode : "",
            //    ProductType = t.FirstOrDefault() != null ? t.FirstOrDefault().Product.ProductType : Domain.Enums.EProductType.Goods,
            //    Money = t.Sum(s => s.Money),
            //}).ToList();

            //orderItemGroup.AddRange(pickUpOrderGroup);

            //var productQueryable = ProductRepo.GetInclude(p => p.ProductCategory, false);
            //orderItemGroup.ForEach(t =>
            //{
            //    var item = new ProductRetailStatisticsDto();
            //    item.ProductID = t.ProductID;
            //    item.Quantity = t.Quantity;
            //    item.ProductName = t.ProductName;
            //    item.ProductType = t.ProductType.GetDescription();
            //    item.Money = t.Money;
            //    var product = productQueryable.Where(p => p.ID == t.ProductID).FirstOrDefault();
            //    if (product != null)
            //    {
            //        item.ProductName = product.Name;
            //        item.ProductCategoryName = product.ProductCategory.Name;
            //        item.ProductCode = product.Code;
            //        item.Unit = product.Unit;
            //        item.ProductType = product.ProductType.GetDescription();
            //    }
            //    result.Add(item);
            //});

            //result = result.OrderBy(t => t.ProductType).OrderByDescending(t => t.Quantity).ToList();
            //totalCount = result.Count();

            //if (filter != null)
            //{
            //    if (!string.IsNullOrEmpty(filter.ProductName))
            //        result = result.Where(t => t.ProductName.Contains(filter.ProductName)).ToList();
            //    if (!string.IsNullOrEmpty(filter.ProductCode))
            //        result = result.Where(t => t.ProductCode.Contains(filter.ProductCode)).ToList();
            //    //if (!string.IsNullOrEmpty(filter.ProductCodeName))
            //    //{
            //    //    result = result.Where(t => filter.ProductCodeName.Contains(t.ProductCode) || filter.ProductCodeName.Contains(t.ProductName)).ToList();
            //    //}
            //    if (filter.IsSaleWell.HasValue)
            //    {
            //        if (!(filter.StartDate.HasValue && filter.EndDate.HasValue))
            //        {
            //            throw new DomainException("参数错误");
            //        }
            //        var days = (filter.EndDate.Value - filter.StartDate.Value).Days;
            //        if (filter.IsSaleWell.Value)
            //        {
            //            result = result.Take(5).ToList();
            //        }
            //        else
            //        {
            //            IList<ProductRetailStatisticsDto> removeData = new List<ProductRetailStatisticsDto>();
            //            foreach (var t in result)
            //            {
            //                var unsaleProductSettingItem = UnsaleProductSettingItemRepo.GetInclude(p => p.UnsaleProductSetting, false).Where(p => p.MerchantID == AppContext.CurrentSession.MerchantID && p.UnsaleProductSetting.IsAvailable == true && p.ProductID == t.ProductID).FirstOrDefault();
            //                if (unsaleProductSettingItem != null)
            //                {
            //                    var quantitiesUnit = unsaleProductSettingItem.UnsaleProductSetting.Quantities * 1.0 / unsaleProductSettingItem.UnsaleProductSetting.PeriodDays;
            //                    var performenceUnit = unsaleProductSettingItem.UnsaleProductSetting.Performence / unsaleProductSettingItem.UnsaleProductSetting.PeriodDays;
            //                    if ((t.Quantity * 1.0 / days) > quantitiesUnit || (t.Money / days) > performenceUnit)
            //                    {
            //                        removeData.Add(t);
            //                    }
            //                }
            //                else
            //                {
            //                    removeData.Add(t);
            //                }
            //            }
            //            removeData.ForEach(t =>
            //            {
            //                result.Remove(t);
            //            });
            //            result = result.OrderBy(t => t.Quantity).ToList();
            //        }
            //    }
            //    totalCount = result.Count();
            //    if (filter.Start.HasValue && filter.Limit.HasValue)
            //        result = result.Skip(filter.Start.Value).Take(filter.Limit.Value).ToList();
            //}
            //return result;
            var result = new List<ProductRetailStatisticsDto>();

            var merchantids = new List<Guid>();
            if (filter.AllSubMerchantData)
            {
                var submerchants = MerchantRepo.GetQueryable(false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID).ToList();
                merchantids = submerchants.Select(t => t.ID).ToList();
            }
            else if (!string.IsNullOrEmpty(filter.MerchantCode))
            {
                var merchant = MerchantRepo.GetQueryable(false).FirstOrDefault(t => t.Code == filter.MerchantCode);
                if (merchant != null)
                    merchantids.Add(merchant.ID);
            }
            else
                merchantids.Add(AppContext.CurrentSession.MerchantID);

            var productQueryable = ProductRepo.GetInclude(t => t.ProductCategory, false).Where(t => merchantids.Contains(t.MerchantID));
            if (filter.ProductType.HasValue)
                productQueryable = productQueryable.Where(t => t.ProductType == filter.ProductType.Value);
            if (!string.IsNullOrEmpty(filter.ProductCodeName))
                productQueryable = productQueryable.Where(t => t.Code.Contains(filter.ProductCodeName) || t.Name.Contains(filter.ProductCodeName));
            var productList = productQueryable.ToList();
            var orderDividendQuery = OrderDividendRepo.GetQueryable(false).Where(t => merchantids.Contains(t.MerchantID));
            var TradeOrderIDs = orderDividendQuery.Select(t => t.TradeOrderID).Distinct();
            if (filter.StartDate.HasValue)
                TradeOrderIDs = orderDividendQuery.Where(t => t.CreatedDate >= filter.StartDate).Select(sl => sl.TradeOrderID).Distinct();
            if (filter.EndDate.HasValue)
                TradeOrderIDs = orderDividendQuery.Where(t => t.CreatedDate < filter.EndDate).Select(sl => sl.TradeOrderID).Distinct();
            if (filter.StartDate.HasValue && filter.EndDate.HasValue)
                TradeOrderIDs = orderDividendQuery.Where(t => t.CreatedDate >= filter.StartDate && t.CreatedDate < filter.EndDate).Select(sl => sl.TradeOrderID).Distinct();
            var orderItems = OrderItemRepo.GetQueryable(false).Where(t => merchantids.Contains(t.MerchantID) && TradeOrderIDs.Contains(t.OrderID)).ToList();
            var pickUpOrderItems = PickUpOrderItemRepo.GetQueryable(false).Where(t => merchantids.Contains(t.MerchantID) && TradeOrderIDs.Contains(t.PickUpOrderID)).ToList();
            productList.ForEach(t =>
            {
                var item = new ProductRetailStatisticsDto();
                item.ProductID = t.ID;
                item.ProductCode = t.Code;
                item.ProductName = t.Name;
                item.ProductCategoryName = t.ProductCategory.Name;
                item.ProductType = t.ProductType.GetDescription();
                var orderItemList = orderItems.Where(m => m.GoodsID == t.ID).ToList();
                var pickUpOrderItemList = pickUpOrderItems.Where(m => m.ProductID == t.ID).ToList();
                var orderItemQuantity = orderItemList.GroupBy(g => 1).Select(sl => sl.Sum(s => s.Quantity)).FirstOrDefault();
                var pickOrderItemQuantity = pickUpOrderItemList.GroupBy(g => 1).Select(sl => sl.Sum(s => s.Quantity)).FirstOrDefault();
                item.Quantity = orderItemQuantity + pickOrderItemQuantity;
                item.Unit = t.Unit;
                var orderItemMoney = orderItemList.GroupBy(g => 1).Select(sl => sl.Sum(s => s.Money)).FirstOrDefault();
                var pickOrderItemMoney = pickUpOrderItemList.GroupBy(g => 1).Select(sl => sl.Sum(s => s.Money)).FirstOrDefault();
                item.Money = orderItemMoney + pickOrderItemMoney;
                result.Add(item);
            });
            totalCount = result.Count();
            if (filter.Start.HasValue && filter.Limit.HasValue)
                result = result.OrderByDescending(t => t.Money).Skip(filter.Start.Value).Take(filter.Limit.Value).ToList();
            if (filter.OrderType == EProductRetailStatisticsFilterOrderType.Money)
                result = result.OrderByDescending(t => t.Money).ToList();
            else if (filter.OrderType == EProductRetailStatisticsFilterOrderType.Quantity)
                result = result.OrderByDescending(t => t.Quantity).ToList();
            return result;
        }

        /// <summary>
        /// 零售产品汇总图表统计
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public IEnumerable<ProductRetailStatisticsChartDataDto> ProductRetailStatisticsChartData(ProductRetailStatisticsChartDataFilter filter, ref int totalCount)
        {
            var result = new List<ProductRetailStatisticsChartDataDto>();
            if (!filter.ProductID.HasValue)
                throw new DomainException("参数错误");
            var product = ProductRepo.GetByKey(filter.ProductID.Value);
            var orderDividendQuery = OrderDividendRepo.GetQueryable(false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID && t.GoodsID == product.ID);
            if (filter.StartDate.HasValue)
                orderDividendQuery = orderDividendQuery.Where(t => t.CreatedDate >= filter.StartDate);
            if (filter.EndDate.HasValue)
                orderDividendQuery = orderDividendQuery.Where(t => t.CreatedDate < filter.EndDate);
            if (filter.StartDate.HasValue && filter.EndDate.HasValue)
                orderDividendQuery = orderDividendQuery.Where(t => t.CreatedDate >= filter.StartDate && t.CreatedDate < filter.EndDate);
            var TradeOrderIDs = orderDividendQuery.Select(t => t.TradeOrderID).Distinct();
            var orderItemList = OrderItemRepo.GetInclude(t => t.Order, false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID && TradeOrderIDs.Contains(t.OrderID)).ToList();
            var pickUpOrderItemList = PickUpOrderItemRepo.GetInclude(t=> t.PickUpOrder, false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID && TradeOrderIDs.Contains(t.PickUpOrderID)).ToList();

            var orderItemGroup = orderItemList.GroupBy(g => g.Order.CreatedDate.ToDateString()).Select(m => new ProductRetailStatisticsChartDataDto
            {
                TradeDate = m.Key,
                Quantity = m.Sum(s => s.Quantity),
                Money = m.Sum(s => s.Money),
            }).ToList();

            var pickUpOrderItemGroup = pickUpOrderItemList.GroupBy(g => g.PickUpOrder.CreatedDate.ToDateString()).Select(m => new ProductRetailStatisticsChartDataDto
            {
                TradeDate = m.Key,
                Quantity = m.Sum(s => s.Quantity),
                Money = m.Sum(s => s.Money),
            }).ToList();

            orderItemGroup.AddRange(pickUpOrderItemGroup);

            orderItemGroup.GroupBy(g => g.TradeDate).Select(m => new ProductRetailStatisticsChartDataDto
            {
                TradeDate = m.Key,
                Quantity = m.Sum(s=> s.Quantity),
                Money = m.Sum(s => s.Money),
            });

            //var orderDividendList = orderDividendQuery.ToList();
            //result = orderDividendList.GroupBy(t => t.CreatedDate.ToDateString()).Select(m => new ProductRetailStatisticsChartDataDto
            //{
            //    TradeDate = m.Key,
            //    Money = m.Sum(s=> s.Money),
            //    Quantity = m.Sum(s=> s.Quantity),
            //}).ToList();

            var tradeDates = orderItemGroup.Select(t => t.TradeDate).ToList();
            var startdate = Convert.ToDateTime(orderItemGroup.Select(t => t.TradeDate).Min());
            var enddate = DateTime.Now;
            if (filter.StartDate.HasValue)
            {
                startdate = filter.StartDate.Value;
            }
            if (startdate <= new DateTime(1971, 6, 1))
                startdate = enddate;
            if (filter.EndDate.HasValue)
            {
                enddate = filter.EndDate.Value.AddDays(1);
            }

            for (var i = startdate; i < enddate; i = i.AddDays(1))
            {
                var time = i.ToDateString();
                if (!tradeDates.Contains(time))
                {
                    orderItemGroup.Add(new ProductRetailStatisticsChartDataDto
                    {
                        TradeDate = i.ToDateString(),
                        Money = 0,
                        Quantity = 0,
                    });
                }
            }

            return orderItemGroup.OrderByDescending(t => t.TradeDate);
        }

        #region private method

        private IEnumerable<Member> GetMemberList(DateTime? starDate = null, DateTime? endDate = null)
        {
            var memberQueryable = MemberRepo.GetInclude(t => t.MemberPlateList, false).Where(m => m.MerchantID == AppContext.CurrentSession.MerchantID);
            if (starDate.HasValue && endDate.HasValue)
            {
                memberQueryable = memberQueryable.Where(t => t.CreatedDate >= starDate && t.CreatedDate < endDate);
            }
            return memberQueryable.ToList();
        }

        private DataAnalyseDto DataAnalyse(Guid memberID, DateTime? startDate = null, DateTime? endDate = null)
        {
            var result = new DataAnalyseDto();
            var member = MemberRepo.GetByKey(memberID);
            if (member != null)
            {
                result.MemberID = member.ID;
                result.MemberName = member.Name;
                result.MemberMobilePhone = member.MobilePhoneNo;
                result.TotalQuantity = 0;
                result.TotalMoney = 0;
                result.RegistDate = member.CreatedDate;

                var orderItemQueryable = OrderItemRepo.GetInclude(t => t.Order, false).Where(t => t.Order.MerchantID == AppContext.CurrentSession.MerchantID);
                var pickUpOrderItemQueryable = PickUpOrderItemRepo.GetIncludes(false, "PickUpOrder", "Product").Where(t => t.PickUpOrder.MerchantID == AppContext.CurrentSession.MerchantID);
                if (startDate.HasValue && endDate.HasValue)
                {
                    orderItemQueryable = orderItemQueryable.Where(t => t.Order.CreatedDate >= startDate && t.Order.CreatedDate < endDate);
                    pickUpOrderItemQueryable = pickUpOrderItemQueryable.Where(t => t.PickUpOrder.CreatedDate >= startDate && t.PickUpOrder.CreatedDate < endDate);
                }

                orderItemQueryable = orderItemQueryable.Where(t => t.Order.MemberID == member.ID || t.Order.OpenID == member.WeChatOpenID);
                string plateList = "";
                member.MemberPlateList.ForEach(p =>
                {
                    plateList += p.PlateNumber + "|";
                });
                pickUpOrderItemQueryable = pickUpOrderItemQueryable.Where(t => t.PickUpOrder.MemberID == member.ID || plateList.Contains(t.PickUpOrder.PlateNumber));

                var orderItemList = orderItemQueryable.Where(m => m.ProductType != EProductType.MemberCard).ToList();
                var orderItemGroup = orderItemList.GroupBy(g =>new { g.Order.Code, g.GoodsID }).Select(m => new
                {
                    TradeNo = m.Key.Code,
                    ProductID = m.Key.GoodsID,
                    ProductName = m.FirstOrDefault() != null ? m.FirstOrDefault().ProductName : "",
                    ProductCode = "",
                    Quantity = m.Sum(s => s.Quantity),
                    ProductType = m.FirstOrDefault() != null ? m.FirstOrDefault().ProductType : EProductType.Goods,
                    Money = m.Sum(s => s.Money),
                }).ToList();

                var pickUpOrderList = pickUpOrderItemQueryable.ToList();
                var pickUpOrderGroup = pickUpOrderList.GroupBy(g => new { g.PickUpOrder.Code, g.ProductID }).Select(m => new
                {
                    TradeNo = m.Key.Code,
                    m.Key.ProductID,
                    ProductName = m.FirstOrDefault() != null ? m.FirstOrDefault().ProductName : "",
                    ProductCode = m.FirstOrDefault() != null ? m.FirstOrDefault().ProductCode : "",
                    Quantity = m.Sum(s => s.Quantity),
                    ProductType = m.FirstOrDefault() != null ? m.FirstOrDefault().Product.ProductType : EProductType.Goods,
                    Money = m.Sum(s => s.Money),
                }).ToList();

                orderItemGroup.AddRange(pickUpOrderGroup);
                var productQueryable = ProductRepo.GetInclude(p => p.ProductCategory, false);
                orderItemGroup.ForEach(m =>
                {
                    var dataAnalyseItem = new DataAnalyseItemDto();
                    dataAnalyseItem.TradeNo = m.TradeNo;
                    dataAnalyseItem.ProductID = m.ProductID;
                    dataAnalyseItem.ProductName = m.ProductName;
                    dataAnalyseItem.TotalQuantity = m.Quantity;
                    dataAnalyseItem.TotalMoney = m.Money;
                    result.TotalQuantity += m.Quantity;
                    result.TotalMoney += m.Money;
                    result.DataAnalyseItemDtos.Add(dataAnalyseItem);
                });
                result.DataAnalyseItemDtos = result.DataAnalyseItemDtos.OrderByDescending(t => t.TradeNo).ToList();
                return result;
            }
            else
            {
                throw new DomainException("会员不存在");
            }


        }

        private IEnumerable<DataAnalyseDto> NewMemberDataAnalyse(DateTime startDate, DateTime endDate)
        {
            var result = new List<DataAnalyseDto>();
            var memberList = GetMemberList(startDate, endDate);
            memberList.ForEach(t =>
            {
                result.Add(DataAnalyse(t.ID));
            });
            return result;
        }

        private IEnumerable<DataAnalyseDto> DataAnalyse(DateTime startDate, DateTime endDate)
        {
            var result = new List<DataAnalyseDto>();
            var memberList = GetMemberList();
            memberList.ForEach(t =>
            {
                result.Add(DataAnalyse(t.ID, startDate, endDate));
            });
            return result;
        }

        #endregion

        /// <summary>
        /// 数据分析
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public IEnumerable<DataAnalyseDto> DataAnalyseList(DataAnalyseFilter filter, ref int totalCount)
        {
            var result = new List<DataAnalyseDto>();
            var now = DateTime.Now;
            var yesteday = now.AddDays(-1);
            var currentOneMonthFirstDay = new DateTime(now.Year, now.Month, 1);
            var currentOneYearFirstMonth = new DateTime(now.Year, 1, 1);
            var previousOneMonthFirstDay = new DateTime(now.Year, now.Month-1, 1);
            var previousThreeMonthFirstDay = new DateTime(now.Year, now.Month - 3, 1);
            var previousSixMonthFirstDay = new DateTime(now.Year, now.Month - 6, 1);
            var previousOneYearFirstDay = new DateTime(now.Year -1, 1, 1);
            var previousOneYear = now.AddYears(-1);
            //新增会员入口
            if (filter.TimeSelect.HasValue)
            {
                if (filter.TimeSelect == ETimeSelect.ByDay)
                {
                    result.AddRange(NewMemberDataAnalyse(yesteday, now));
                }
                if (filter.TimeSelect == ETimeSelect.ByMonth)
                {
                    result.AddRange(NewMemberDataAnalyse(previousOneMonthFirstDay, now));
                }
            }

            //会员类型入口
            if (filter.MemberType.HasValue)
            {

                var resulttemp = DataAnalyse(previousOneMonthFirstDay, currentOneMonthFirstDay);
                if (filter.MemberType.Value == EMemberType.NomalMember)
                {
                    resulttemp.ForEach(t =>
                    {
                        if (t.TotalMoney < 2000)
                        {
                            result.Add(t);
                        }
                    });
                }
                else if (filter.MemberType.Value == EMemberType.SilverMember)
                {
                    resulttemp.ForEach(t =>
                    {
                        if (t.TotalMoney >= 2000 && t.TotalMoney < 5000)
                        {
                            result.Add(t);
                        }
                    });
                }
                else if (filter.MemberType.Value == EMemberType.GoldMember)
                {
                    resulttemp.ForEach(t =>
                    {
                        if (t.TotalMoney >= 5000 && t.TotalMoney < 10000)
                        {
                            result.Add(t);
                        }
                    });
                }
                else if (filter.MemberType.Value == EMemberType.PlatinumMember)
                {
                    resulttemp.ForEach(t =>
                    {
                        if (t.TotalMoney >= 10000)
                        {
                            result.Add(t);
                        }
                    });
                }
            }

            //大客户分析
            if (filter.BigMember.HasValue)
            {
                var resulttemp = DataAnalyse(previousOneYear, now);
                var resulttemp2 = new List<DataAnalyseDto>();
                if (filter.BigMember == EBigMember.Nomal)
                {
                    resulttemp.ForEach(t =>
                    {
                        if (t.TotalMoney > 10000)
                        {
                            resulttemp2.Add(t);
                        }
                    });
                }
                if (filter.BigMember == EBigMember.Rich)
                {
                    resulttemp.ForEach(t =>
                    {
                        if (t.TotalMoney > 20000)
                        {
                            resulttemp2.Add(t);
                        }
                    });
                }

                var dataAnalyseItemList = new List<DataAnalyseItemDto>();
                resulttemp2.ForEach(t =>
                {
                    dataAnalyseItemList.AddRange(t.DataAnalyseItemDtos);
                });
                var ids = dataAnalyseItemList.Select(t => t.ProductID).Distinct();
                var productList = ProductRepo.GetQueryable(false).Where(t => ids.Contains(t.ID)).ToList();
                productList.ForEach(t =>
                {
                    var dataAnalyse = new DataAnalyseDto
                    {
                        MemberName = t.Name
                    };
                    foreach (var item in dataAnalyseItemList)
                    {
                        if (item.ProductID == t.ID)
                        {
                            dataAnalyse.TotalQuantity += item.TotalQuantity;
                            dataAnalyse.TotalMoney += item.TotalMoney;
                        }
                    }
                    result.Add(dataAnalyse);
                });
        }

            //会员忠诚度分析
            if (filter.LoyalMember.HasValue)
            {
                var resulttemp = DataAnalyse(previousOneMonthFirstDay, now);
                if (filter.LoyalMember == ELoyalMember.NomalLoyal)
                {
                    resulttemp.ForEach(t =>
                    {
                        if (t.TotalQuantity == 1)
                        {
                            result.Add(t);
                        }
                    });
                }
                else if (filter.LoyalMember.Value == ELoyalMember.GoodLoyal)
                {
                    resulttemp.ForEach(t =>
                    {
                        if (t.TotalQuantity == 2)
                        {
                            result.Add(t);
                        }
                    });
                }
                else if (filter.LoyalMember.Value == ELoyalMember.AbsoluteLoyalty)
                {
                    resulttemp.ForEach(t =>
                    {
                        if (t.TotalQuantity >= 3)
                        {
                            result.Add(t);
                        }
                    });
                }
            }

            //流失客户分析
            if (filter.LoseMember.HasValue)
            {
                if (filter.LoseMember == ELoseMember.NomalLose)
                {
                    var removeMembersIDs = DataAnalyse(previousOneYearFirstDay, now).Select(t => t.MemberID).Distinct().ToList();
                    removeMembersIDs.AddRange(DataAnalyse(previousSixMonthFirstDay, now).Select(t => t.MemberID).Distinct().ToList());
                    var resulttemp = DataAnalyse(previousSixMonthFirstDay, now);
                    resulttemp.ForEach(t =>
                    {
                        if (!removeMembersIDs.Contains(t.MemberID))
                        {
                            if (t.TotalQuantity < 1)
                            {
                                result.Add(t);
                            }
                        }
                    });
                }
                if (filter.LoseMember == ELoseMember.SevereLose)
                {
                    var removeMembersIDs = DataAnalyse(previousOneYearFirstDay, now).Select(t => t.MemberID).Distinct().ToList();
                    var resulttemp = DataAnalyse(previousSixMonthFirstDay, now);
                    resulttemp.ForEach(t =>
                    {
                        if (!removeMembersIDs.Contains(t.MemberID)) {
                            if (t.TotalQuantity < 1)
                            {
                                result.Add(t);
                            }
                        }
                    });
                }
                if (filter.LoseMember == ELoseMember.AbsoluteLose)
                {
                    var resulttemp = DataAnalyse(previousOneYearFirstDay, now);
                    resulttemp.ForEach(t =>
                    {
                        if (t.TotalQuantity < 1)
                        {
                            result.Add(t);
                        }
                    });
                }
            }
            totalCount = result.Count();
            if(filter.Start.HasValue && filter.Limit.HasValue)
                result = result.OrderByDescending(t=>t.TotalMoney).Skip(filter.Start.Value).Take(filter.Limit.Value).ToList();
            result = result.OrderByDescending(t => t.TotalMoney).ToList();
            return result;
        }

        /// <summary>
        /// 畅销/滞销产品
        /// </summary>
        /// <returns></returns>
        public IEnumerable<UnsaleProductHistoryDto> UnsaleProductHistory(UnsaleProductHistoryFilter filter, ref int totalCount)
        {
            var queryable = UnsaleProductHistoryRepo.GetQueryable(false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID);
            if (!string.IsNullOrEmpty(filter.Code))
                queryable = queryable.Where(t => t.Code.Contains(filter.Code));
            if (!string.IsNullOrEmpty(filter.Keyword))
                queryable = queryable.Where(t => t.ProductCode.Contains(filter.Keyword) || t.ProductName.Contains(filter.Keyword));
            if (filter.ProductType.HasValue)
                queryable = queryable.Where(t => t.ProductType == filter.ProductType);
            if (filter.Status.HasValue)
                queryable = queryable.Where(t => t.Status == filter.Status);
            totalCount = queryable.Count();
            return queryable.MapTo<UnsaleProductHistoryDto>().OrderByDescending(t => t.Code).OrderByDescending(t => t.Quantity).ToList();
        }

        public IEnumerable<ConsumeHistoryDto> GetConsumeHistory(ConsumeHistoryFilter filter, ref int totalCount)
        {
            var result = new List<ConsumeHistoryDto>();

            var pickUpOrderQueryable = PickUpOrderRepo.GetQueryable(false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID);
            var orderQueryable = OrderRepo.GetQueryable(false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID);
            var consumeHistoryQueryable = ConsumeHistoryRepo.GetQueryable(false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID);

            var memberQueryable = MemberRepo.GetInclude(t => t.MemberPlateList, false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID);

            pickUpOrderQueryable.ToList().ForEach(t =>
            {
                result.Add(new ConsumeHistoryDto
                {
                    Name = "",
                    PlateNumber = t.PlateNumber,
                    TradeNo = t.Code,
                    TradeMoney = t.Money,
                    Source = EHistorySource.PickUpOrder,
                    CreatedDate = t.CreatedDate,
                });
            });

            orderQueryable.ToList().ForEach(t =>
            {
                var member = memberQueryable.FirstOrDefault(m => m.ID == t.MemberID || m.WeChatOpenID == t.OpenID);
                result.Add(new ConsumeHistoryDto
                {
                    Name = member != null ? member.Name : "",
                    MobilePhoneNo = member != null ? member.MobilePhoneNo : "",
                    PlateNumber = "",
                    TradeNo = t.Code,
                    TradeMoney = t.Money,
                    Source = EHistorySource.ShopOrder,
                    CreatedDate = t.CreatedDate,
                });
            });

            consumeHistoryQueryable.ToList().ForEach(t =>
            {
                result.Add(new ConsumeHistoryDto
                {
                    Name = t.Name,
                    PlateNumber = t.PlateNumber,
                    TradeNo = t.TradeNo,
                    MobilePhoneNo = t.MobilePhoneNo,
                    Consumption = t.Consumption,
                    TradeCount = t.TradeCount,
                    Unit = t.Unit,
                    Price = t.Price,
                    TradeMoney = t.TradeMoney,
                    BasePrice = t.BasePrice,
                    GrossProfit = t.GrossProfit,
                    Remark = t.Remark,
                    DepartmentName = t.DepartmentName,
                    Source = EHistorySource.HistoryData,
                    CreatedDate = t.CreatedDate
                });
            });

            totalCount = result.Count();

            if (!string.IsNullOrEmpty(filter.Name))
                result = result.Where(t => t.Name.Contains(filter.Name)).ToList();
            if (!string.IsNullOrEmpty(filter.MobilePhoneNo))
                result = result.Where(t => (t.MobilePhoneNo ?? string.Empty).Contains(filter.MobilePhoneNo)).ToList();
            if (!string.IsNullOrEmpty(filter.Keyword))
                result = result.Where(t => t.Name.Contains(filter.Keyword) || (t.MobilePhoneNo ?? string.Empty).Contains(filter.Keyword)).ToList();
            if (!string.IsNullOrEmpty(filter.PlateNumber))
                result = result.Where(t => (t.PlateNumber ?? string.Empty).Contains(filter.PlateNumber)).ToList();
            if (!string.IsNullOrEmpty(filter.TradeNo))
                result = result.Where(t => t.TradeNo.Contains(filter.TradeNo)).ToList();
            if (filter.StartTime.HasValue)
                result = result.Where(t => t.CreatedDate >= filter.StartTime.Value).ToList();
            if (filter.EndTime.HasValue)
            {
                var endTime = filter.EndTime.Value.AddDays(1);
                result = result.Where(t => t.CreatedDate < endTime).ToList();
            }
            if (filter.Source.HasValue)
                result = result.Where(t => t.Source == filter.Source.Value).ToList();
            totalCount = result.Count();
            if (filter.Start.HasValue && filter.Limit.HasValue)
                result = result.Skip(filter.Start.Value).Take(filter.Limit.Value).ToList();

            return result.OrderByDescending(t => t.CreatedDate).ToList();
        }

        public OpenAccountReportingDto GetOpenAccountReporting()
        {
            var result = new OpenAccountReportingDto();
            var resultitem = new List<TurnoverDto>();

            var starttime = DateTime.Now.AddDays(-6).Date;
            var now = DateTime.Now.Date;
            var agentdepartmentlist = AgentDepartmentRepo.GetQueryable(false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID && t.CreatedDate > starttime).ToList();

            while (starttime <= now)
            {
                var agentdepartments = agentdepartmentlist.Where(t => t.CreatedDate.Date == starttime.Date).ToList();
                resultitem.Add(new TurnoverDto
                {
                    Unit = starttime.Date.Day,
                    Turnover = agentdepartments.Count(),
                });
                starttime = starttime.AddDays(1);
            }

            result.TotalOpenAccount = resultitem;
            return result;
        }

        public MonthOpenAccountPerformanceDto GetMonthOpenAccountPerformance(DateTime date)
        {
            if (date == null)
                throw new DomainException("参数错误");
            var result = new MonthOpenAccountPerformanceDto();

            var starttime = new DateTime(date.Year, date.Month, 1);
            var endtime = starttime.AddMonths(1);
            var agentdepartmentlist = AgentDepartmentRepo.GetQueryable(false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID && t.CreatedDate >= starttime && t.CreatedDate < endtime).ToList();

            result.TotalOpenAccountCount = agentdepartmentlist.Count();

            return result;
        }

        /// <summary>
        /// 批量导入
        /// </summary>
        /// <param name="consumeHistories"></param>
        /// <returns></returns>
        public bool ImportConsumeHistoryData(IEnumerable<ConsumeHistory> consumeHistories)
        {
            var index = 1;
            consumeHistories.ForEach(t =>
            {
                var consumeHistoryList = ConsumeHistoryRepo.GetQueryable(false)
                    .Where(m => m.Name == t.Name && m.PlateNumber == t.PlateNumber && m.TradeNo == t.TradeNo && m.MobilePhoneNo == t.MobilePhoneNo)
                    .Where(m => m.Consumption == t.Consumption && m.TradeCount == t.TradeCount && m.Price == t.Price && t.TradeMoney == t.TradeMoney)
                    .Where(m => m.BasePrice == t.BasePrice && m.GrossProfit == t.GrossProfit && m.Remark == t.Remark && m.DepartmentName == t.DepartmentName)
                    .Where(m => m.CreatedDate == t.CreatedDate).ToList();
                if (consumeHistoryList.Count() > 0)
                    throw new DomainException("批量导入会员失败" + "第" + index + "行数据已存在.");
                t.ID = Util.NewID();
                t.MerchantID = AppContext.CurrentSession.MerchantID;
                t.CreatedUserID = AppContext.CurrentSession.UserID;
                t.CreatedUser = AppContext.CurrentSession.UserName;
                index++;
            });
            return ConsumeHistoryRepo.AddRange(consumeHistories).Count() > 0;
        }

        /// <summary>
        /// 营业报表
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public IEnumerable<OperationStatementDto> GetOperationStatement(OperationStatementFilter filter, out int totalCount)
        {
            var operationStatementDtos = new List<OperationStatementDto>();
            var operationStatementDetailDtoList = GetAllOperationStatementDetail(filter);
            operationStatementDtos = operationStatementDetailDtoList.GroupBy(t => t.CreatedDate.ToDateString()).Select(t => new OperationStatementDto
            {
                Code = t.Key,
                TotalInCome = t.Sum(s => s.BudgetType == EBudgetType.InCome ? s.Money : 0),
                TotalOutCome = t.Sum(s => s.BudgetType == EBudgetType.OutCome ? s.Money : 0),
            }).ToList();

            var codes = operationStatementDtos.Select(t => t.Code).ToList();
            var startdate = Convert.ToDateTime(operationStatementDtos.Select(t => t.Code).Min());
            var enddate = DateTime.Now;
            if (filter.StartDate.HasValue)
            {
                startdate = filter.StartDate.Value;
            }
            if (filter.EndDate.HasValue)
            {
                enddate = filter.EndDate.Value.AddDays(1);
            }

            for (var i = startdate; i < enddate; i = i.AddDays(1)) {
                var time = i.ToDateString();
                if (!codes.Contains(time))
                {
                    operationStatementDtos.Add(new OperationStatementDto
                    {
                        Code = i.ToDateString(),
                        TotalInCome = 0,
                        TotalOutCome = 0,
                    });
                }
            }

            totalCount = operationStatementDtos.Count();
            if(filter.Start.HasValue && filter.Limit.HasValue)
                operationStatementDtos = operationStatementDtos.OrderByDescending(t => t.Code).Skip(filter.Start.Value).Take(filter.Limit.Value).ToList();
            return operationStatementDtos.OrderByDescending(t => t.Code);
        }

        /// <summary>
        /// 获取会员总数
        /// </summary>
        /// <returns></returns>
        public int GetMemberTotalCount()
        {
            var memberList = MemberRepo.GetQueryable(false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID).ToList();
            if (memberList != null)
            {
                return memberList.Count();
            }
            else {
                return 0;
            }
        }

        /// <summary>
        /// 营业报表 详情
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public IEnumerable<OperationStatementDetailDto> GetOperationStatementDetail(OperationStatementFilter filter, out int totalCount)
        {
            var operationStatementDetailDtoList = GetAllOperationStatementDetail(filter);
            if (filter.StartDate.HasValue)
                operationStatementDetailDtoList = operationStatementDetailDtoList.Where(t => t.CreatedDate.ToDateString() == filter.StartDate.Value.ToDateString()).ToList();
            if (!string.IsNullOrEmpty(filter.TradeNo))
                operationStatementDetailDtoList = operationStatementDetailDtoList.Where(t => t.TradeNo.Contains(filter.TradeNo.ToUpper())).ToList();
            if (filter.ResourceType.HasValue)
                operationStatementDetailDtoList = operationStatementDetailDtoList.Where(t => t.ResourceType == filter.ResourceType).ToList();
            if (filter.BudgetType.HasValue)
                operationStatementDetailDtoList = operationStatementDetailDtoList.Where(t => t.BudgetType == filter.BudgetType).ToList();
            totalCount = operationStatementDetailDtoList.Count();
            if(filter.Start.HasValue && filter.Limit.HasValue)
                operationStatementDetailDtoList = operationStatementDetailDtoList.OrderByDescending(t => t.CreatedDate).Skip(filter.Start.Value).Take(filter.Limit.Value).ToList();
            return operationStatementDetailDtoList.OrderByDescending(t => t.CreatedDate);
        }

        private IEnumerable<OperationStatementDetailDto> GetAllOperationStatementDetail(OperationStatementFilter filter)
        {
            var operationStatementDetailDtoList = new List<OperationStatementDetailDto>();
            var orderList = OrderRepo.GetInclude(t => t.OrderItemList, false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID && t.Status != EOrderStatus.UnPay).ToList();
            var pickUpOrderList = PickUpOrderRepo.GetInclude(t => t.PickUpOrderItemList, false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID && t.Status != EPickUpOrderStatus.UnPay).ToList();
            var reimbursementList = ReimbursementRepo.GetQueryable(false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID && t.Status == EReimbursementApproveStatus.Approved).ToList();
            var dailyexpenseList = DailyExpenseRepo.GetQueryable(false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID).ToList();
            if (filter.StartDate.HasValue)
            {
                orderList = orderList.Where(t => t.CreatedDate >= filter.StartDate).ToList();
                pickUpOrderList = pickUpOrderList.Where(t => t.CreatedDate >= filter.StartDate).ToList();
                reimbursementList = reimbursementList.Where(t => t.CreatedDate >= filter.StartDate).ToList();
                dailyexpenseList = dailyexpenseList.Where(t => t.ExpenseDate >= filter.StartDate).ToList();
            }
            if (filter.EndDate.HasValue)
            {
                var endDay = filter.EndDate.Value.AddDays(1);
                orderList = orderList.Where(t => t.CreatedDate < endDay).ToList();
                pickUpOrderList = pickUpOrderList.Where(t => t.CreatedDate < endDay).ToList();
                reimbursementList = reimbursementList.Where(t => t.CreatedDate < endDay).ToList();
                dailyexpenseList = dailyexpenseList.Where(t => t.ExpenseDate < endDay).ToList();
            }

            //商城订单
            orderList.ForEach(t =>
            {
                //收入
                var operationStatementDetailInCome = new OperationStatementDetailDto
                {
                    TradeNo = t.Code,
                    BudgetType = EBudgetType.InCome,
                    ResourceType = EResourceType.Order,
                    Money = t.ReceivedMoney,
                    CreatedDate = t.CreatedDate,
                };
                operationStatementDetailDtoList.Add(operationStatementDetailInCome);

                //支出
                var operationStatementDetailOutCome = new OperationStatementDetailDto
                {
                    TradeNo = t.Code,
                    BudgetType = EBudgetType.OutCome,
                    ResourceType = EResourceType.Order,
                    Money = t.CostMoney,
                    CreatedDate = t.CreatedDate,
                };
                operationStatementDetailDtoList.Add(operationStatementDetailOutCome);
            });

            //接车单
            pickUpOrderList.ForEach(t =>
            {
                //收入
                var operationStatementDetailInCome = new OperationStatementDetailDto
                {
                    TradeNo = t.Code,
                    BudgetType = EBudgetType.InCome,
                    ResourceType = EResourceType.PickupOrder,
                    Money = t.ReceivedMoney,
                    CreatedDate = t.CreatedDate,
                };
                operationStatementDetailDtoList.Add(operationStatementDetailInCome);
                //支出
                var operationStatementDetailOutCome = new OperationStatementDetailDto
                {
                    TradeNo = t.Code,
                    BudgetType = EBudgetType.OutCome,
                    ResourceType = EResourceType.PickupOrder,
                    Money = t.CostMoney,
                    CreatedDate = t.CreatedDate,
                };
                operationStatementDetailDtoList.Add(operationStatementDetailOutCome);
            });

            //报销单
            reimbursementList.ForEach(t =>
            {
                //支出
                var operationStatementDetailOutCome = new OperationStatementDetailDto
                {
                    TradeNo = t.Code,
                    BudgetType = EBudgetType.OutCome,
                    ResourceType = EResourceType.ReceiptOrder,
                    Money = t.Money,
                    CreatedDate = t.CreatedDate,
                };
                operationStatementDetailDtoList.Add(operationStatementDetailOutCome);
            });

            //日常支出
            dailyexpenseList.ForEach(t =>
            {
                var dailyexpenseOutCome = new OperationStatementDetailDto
                {
                    TradeNo = t.ExpenseDate.ToDateString(),
                    BudgetType = EBudgetType.OutCome,
                    ResourceType = EResourceType.DailyExpenseOrder,
                    Money = t.Money,
                    CreatedDate = t.CreatedDate,
                };
                operationStatementDetailDtoList.Add(dailyexpenseOutCome);
            });
            return operationStatementDetailDtoList;
        }
    }
}
