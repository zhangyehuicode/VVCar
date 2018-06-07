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

        IRepository<Order> OrderRepo { get => ServiceLocator.Instance.GetService<IRepository<Order>>(); }

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

            var pickuporderqueryable = PickUpOrderRepo.GetQueryable(false).Where(t => t.StaffID == user.ID && t.MerchantID == AppContext.CurrentSession.MerchantID).ToList();
            var monthpickuporderlist = pickuporderqueryable.Where(t => t.CreatedDate >= starttime && t.CreatedDate < endtime).ToList();

            staffPerformance.MonthPerformance = monthpickuporderlist.GroupBy(g => 1).Select(t => t.Sum(s => s.Money)).FirstOrDefault();
            staffPerformance.TotalPerformance = pickuporderqueryable.GroupBy(g => 1).Select(t => t.Sum(s => s.Money)).FirstOrDefault();
            staffPerformance.CustomerServiceCount = pickuporderqueryable.Count();

            return staffPerformance;
        }
    }
}
