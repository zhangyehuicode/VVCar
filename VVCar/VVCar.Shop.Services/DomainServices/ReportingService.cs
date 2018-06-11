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

        IRepository<PickUpOrder> PickUpOrderRepo { get => ServiceLocator.Instance.GetService<IRepository<PickUpOrder>>(); }

        IRepository<PickUpOrderItem> PickUpOrderItemRepo { get => ServiceLocator.Instance.GetService<IRepository<PickUpOrderItem>>(); }

        IRepository<Order> OrderRepo { get => ServiceLocator.Instance.GetService<IRepository<Order>>(); }

        IRepository<OrderItem> OrderItemRepo { get => ServiceLocator.Instance.GetService<IRepository<OrderItem>>(); }

        IRepository<Product> ProductRepo { get => ServiceLocator.Instance.GetService<IRepository<Product>>(); }

        IRepository<User> UserRepo { get => ServiceLocator.Instance.GetService<IRepository<User>>(); }

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

            var totalturnover = result.PickUpOrderTurnover;
            totalturnover.ForEach(t =>
            {
                var sameday = result.ShopTurnover.Where(s => s.Unit == t.Unit).FirstOrDefault();
                if (sameday != null)
                    t.Turnover += sameday.Turnover;
            });

            result.TotalTurnover = totalturnover;
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
                    Turnover = pickuporders.GroupBy(g => 1).Select(t => t.Sum(s => s.Money)).FirstOrDefault(),
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
                    Turnover = orders.GroupBy(g => 1).Select(t => t.Sum(s => s.Money)).FirstOrDefault(),
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

            result.PickUpOrderTurnover = pickuporderlist.GroupBy(g => 1).Select(t => t.Sum(s => s.Money)).FirstOrDefault();
            result.ShopTurnover = orderlist.GroupBy(g => 1).Select(t => t.Sum(s => s.Money)).FirstOrDefault();
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

            var pickuporderqueryable = PickUpOrderRepo.GetIncludes(false, "PickUpOrderItemList", "PickUpOrderItemList.Product").Where(t => t.StaffID == user.ID && t.MerchantID == AppContext.CurrentSession.MerchantID).ToList();
            var monthpickuporderlist = pickuporderqueryable.Where(t => t.CreatedDate >= starttime && t.CreatedDate < endtime).ToList();

            staffPerformance.MonthPerformance = monthpickuporderlist.GroupBy(g => 1).Select(t => t.Sum(s => s.Money)).FirstOrDefault();
            staffPerformance.TotalPerformance = pickuporderqueryable.GroupBy(g => 1).Select(t => t.Sum(s => s.Money)).FirstOrDefault();
            staffPerformance.CustomerServiceCount = pickuporderqueryable.Count();
            staffPerformance.MonthCustomerServiceCount = monthpickuporderlist.Count();

            pickuporderqueryable.Select(t => t.PickUpOrderItemList).ForEach(t =>
            {
                staffPerformance.TotalCommission += t.GroupBy(g => 1).Select(item => item.Sum(s => s.Product.PriceSale * s.Product.CommissionRate / 100)).FirstOrDefault();
            });
            monthpickuporderlist.Select(t => t.PickUpOrderItemList).ForEach(t =>
            {
                staffPerformance.MonthCommission += t.GroupBy(g => 1).Select(item => item.Sum(s => s.Product.PriceSale * s.Product.CommissionRate / 100)).FirstOrDefault();
            });
            staffPerformance.BasicSalary = user.BasicSalary;

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
            var result = new List<StaffPerformance>();
            var userQueryable = UserRepo.GetQueryable(false).Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID);
            if (filter != null && !string.IsNullOrEmpty(filter.StaffName))
                userQueryable = userQueryable.Where(t => t.Name.Contains(filter.StaffName));
            var users = userQueryable.ToList();
            if (users != null && users.Count > 0)
            {
                var now = DateTime.Now;
                var monthStartTime = new DateTime(now.Year, now.Month, 1);
                var nextMonthStartTime = monthStartTime.AddMonths(1);
                var pickuporderQueryable = PickUpOrderRepo.GetIncludes(false, "PickUpOrderItemList", "PickUpOrderItemList.Product").Where(t => t.MerchantID == AppContext.CurrentSession.MerchantID);
                var currentpickuporderlist = pickuporderQueryable;
                var monthpickuporderlist = pickuporderQueryable.Where(t => t.CreatedDate >= monthStartTime && t.CreatedDate < nextMonthStartTime);
                if (filter != null)
                {
                    if (filter.StartDate.HasValue)
                        currentpickuporderlist = currentpickuporderlist.Where(t => t.CreatedDate >= filter.StartDate.Value);
                    if (filter.EndDate.HasValue)
                        currentpickuporderlist = currentpickuporderlist.Where(t => t.CreatedDate < filter.EndDate.Value);
                }
                users.ForEach(user =>
                {
                    var userpickuporder = pickuporderQueryable.Where(t => t.StaffID == user.ID).ToList();
                    var monthuserpickuporder = monthpickuporderlist.Where(t => t.StaffID == user.ID).ToList();
                    var currentuserpickuporder = currentpickuporderlist.Where(t => t.StaffID == user.ID).ToList();

                    var staffPerformance = new StaffPerformance();

                    staffPerformance.TotalPerformance = userpickuporder.GroupBy(g => 1).Select(t => t.Sum(s => s.Money)).FirstOrDefault();
                    staffPerformance.MonthPerformance = monthuserpickuporder.GroupBy(g => 1).Select(t => t.Sum(s => s.Money)).FirstOrDefault(); ;
                    staffPerformance.CurrentPerformance = currentuserpickuporder.GroupBy(g => 1).Select(t => t.Sum(s => s.Money)).FirstOrDefault();

                    staffPerformance.CustomerServiceCount = userpickuporder.Count();
                    staffPerformance.MonthCustomerServiceCount = monthuserpickuporder.Count();
                    staffPerformance.CurrentCustomerServiceCount = currentuserpickuporder.Count();

                    userpickuporder.Select(t => t.PickUpOrderItemList).ForEach(t =>
                    {
                        staffPerformance.TotalCommission += t.GroupBy(g => 1).Select(item => item.Sum(s => s.Product.PriceSale * s.Product.CommissionRate / 100)).FirstOrDefault();
                    });
                    monthuserpickuporder.Select(t => t.PickUpOrderItemList).ForEach(t =>
                    {
                        staffPerformance.MonthCommission += t.GroupBy(g => 1).Select(item => item.Sum(s => s.Product.PriceSale * s.Product.CommissionRate / 100)).FirstOrDefault();
                    });
                    currentuserpickuporder.Select(t => t.PickUpOrderItemList).ForEach(t =>
                    {
                        staffPerformance.CurrentCommission += t.GroupBy(g => 1).Select(item => item.Sum(s => s.Product.PriceSale * s.Product.CommissionRate / 100)).FirstOrDefault();
                    });

                    staffPerformance.BasicSalary = user.BasicSalary;
                    staffPerformance.Subsidy = 0;

                    staffPerformance.StaffID = user.ID;
                    staffPerformance.StaffName = user.Name;
                    staffPerformance.StaffCode = user.Code;

                    result.Add(staffPerformance);
                });
            }
            totalCount = result.Count();
            if (filter != null && filter.Start.HasValue && filter.Limit.HasValue)
                result = result.OrderByDescending(t => t.CurrentPerformance).Skip(filter.Start.Value).Take(filter.Limit.Value).ToList();
            return result.OrderByDescending(t => t.CurrentPerformance).ToList();
        }

        public IEnumerable<ProductRetailStatisticsDto> ProductRetailStatistics(ProductRetailStatisticsFilter filter, ref int totalCount)
        {
            var result = new List<ProductRetailStatisticsDto>();

            var orderItemQueryable = OrderItemRepo.GetQueryable(false);
            var pickUpOrderItemQueryable = PickUpOrderItemRepo.GetQueryable(false);

            if (filter != null)
            {
                if (filter.StartDate.HasValue)
                {
                    orderItemQueryable = orderItemQueryable.Where(t => t.Order.CreatedDate >= filter.StartDate.Value);
                    pickUpOrderItemQueryable = pickUpOrderItemQueryable.Where(t => t.PickUpOrder.CreatedDate >= filter.StartDate.Value);
                }
                if (filter.EndDate.HasValue)
                {
                    orderItemQueryable = orderItemQueryable.Where(t => t.Order.CreatedDate < filter.EndDate.Value);
                    pickUpOrderItemQueryable = pickUpOrderItemQueryable.Where(t => t.PickUpOrder.CreatedDate < filter.EndDate.Value);
                }
            }

            var orderItemList = orderItemQueryable.Where(t => t.Order.MerchantID == AppContext.CurrentSession.MerchantID && t.ProductType != Domain.Enums.EProductType.MemberCard).ToList();
            var orderItemGroup = orderItemList.GroupBy(g => g.GoodsID).Select(t => new
            {
                ProductID = t.Key,
                Quantity = t.Sum(s => s.Quantity),
                ProductName = t.FirstOrDefault() != null ? t.FirstOrDefault().ProductName : "",
                ProductCode = "",
                Money = t.Sum(s => s.Money),
            }).ToList();

            var pickUpOrderList = pickUpOrderItemQueryable.Where(t => t.PickUpOrder.MerchantID == AppContext.CurrentSession.MerchantID).ToList();
            var pickUpOrderGroup = pickUpOrderList.GroupBy(g => g.ProductID).Select(t => new
            {
                ProductID = t.Key,
                Quantity = t.Sum(s => s.Quantity),
                ProductName = t.FirstOrDefault() != null ? t.FirstOrDefault().ProductName : "",
                ProductCode = t.FirstOrDefault() != null ? t.FirstOrDefault().ProductCode : "",
                Money = t.Sum(s => s.Money),
            }).ToList();

            orderItemGroup.AddRange(pickUpOrderGroup);

            var productQueryable = ProductRepo.GetInclude(p => p.ProductCategory, false);
            orderItemGroup.ForEach(t =>
            {
                var item = new ProductRetailStatisticsDto();
                item.ProductID = t.ProductID;
                item.Quantity = t.Quantity;
                item.ProductName = t.ProductName;
                item.Money = t.Money;
                var product = productQueryable.Where(p => p.ID == t.ProductID).FirstOrDefault();
                if (product != null)
                {
                    item.ProductName = product.Name;
                    item.ProductCategoryName = product.ProductCategory.Name;
                    item.ProductCode = product.Code;
                    item.Unit = product.Unit;
                }
                result.Add(item);
            });

            result = result.OrderBy(t => t.ProductCode).ToList();
            totalCount = result.Count();

            if (filter != null)
            {
                if (!string.IsNullOrEmpty(filter.ProductName))
                    result = result.Where(t => t.ProductName.Contains(filter.ProductName)).ToList();
                if (!string.IsNullOrEmpty(filter.ProductCode))
                    result = result.Where(t => t.ProductCode.Contains(filter.ProductCode)).ToList();
                if (!string.IsNullOrEmpty(filter.ProductCodeName))
                    result = result.Where(t => t.ProductCode.Contains(filter.ProductCodeName) || t.ProductName.Contains(filter.ProductCodeName)).ToList();
                totalCount = result.Count();
                if (filter.Start.HasValue && filter.Limit.HasValue)
                    result = result.Skip(filter.Start.Value).Take(filter.Limit.Value).ToList();
            }

            return result;
        }
    }
}
