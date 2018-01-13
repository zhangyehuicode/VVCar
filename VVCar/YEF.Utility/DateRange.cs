using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YEF.Utility
{
    /// <summary>
    /// 时间段值
    /// </summary>
    public static class DateRange
    {
        /// <summary>
        /// 今日
        /// </summary>
        public static Tuple<DateTime, DateTime> Today
        {
            get
            {
                var start = DateTime.Today;
                var end = start.AddDays(1).AddSeconds(-1);
                return new Tuple<DateTime, DateTime>(start, end);
            }
        }

        /// <summary>
        /// 昨日
        /// </summary>
        public static Tuple<DateTime, DateTime> Yesterday
        {
            get
            {
                var today = DateTime.Today;
                var start = today.AddDays(-1);
                var end = today.AddSeconds(-1);
                return new Tuple<DateTime, DateTime>(start, end);
            }
        }

        /// <summary>
        /// 本周
        /// </summary>
        public static Tuple<DateTime, DateTime> ThisWeek
        {
            get
            {
                var today = DateTime.Today;
                int dayOfWeek = (int)today.DayOfWeek;
                if (dayOfWeek == 0)
                    dayOfWeek = 7;
                var start = today.AddDays(1 - dayOfWeek);
                var end = start.AddDays(7).AddSeconds(-1);
                return new Tuple<DateTime, DateTime>(start, end);
            }
        }

        /// <summary>
        /// 上周
        /// </summary>
        public static Tuple<DateTime, DateTime> LastWeek
        {
            get
            {
                var lastWeek = DateTime.Today.AddDays(-7);
                int dayOfWeek = (int)lastWeek.DayOfWeek;
                if (dayOfWeek == 0)
                    dayOfWeek = 7;
                var start = lastWeek.AddDays(1 - dayOfWeek);
                var end = start.AddDays(7).AddSeconds(-1);
                return new Tuple<DateTime, DateTime>(start, end);
            }
        }

        /// <summary>
        /// 本月
        /// </summary>
        public static Tuple<DateTime, DateTime> ThisMonth
        {
            get
            {
                var today = DateTime.Today;
                var start = new DateTime(today.Year, today.Month, 1);
                var end = start.AddMonths(1).AddSeconds(-1);
                return new Tuple<DateTime, DateTime>(start, end);
            }
        }

        /// <summary>
        /// 上月
        /// </summary>
        public static Tuple<DateTime, DateTime> LastMonth
        {
            get
            {
                var lastMonth = DateTime.Today.AddMonths(-1);
                var start = new DateTime(lastMonth.Year, lastMonth.Month, 1);
                var end = start.AddMonths(1).AddSeconds(-1);
                return new Tuple<DateTime, DateTime>(start, end);
            }
        }

        /// <summary>
        /// 本季度
        /// </summary>
        public static Tuple<DateTime, DateTime> ThisQuarter
        {
            get
            {
                var today = DateTime.Today;
                var start = today.AddDays(1 - today.Day).AddMonths(0 - (today.Month - 1) % 3);
                var end = start.AddMonths(3).AddSeconds(-1);
                return new Tuple<DateTime, DateTime>(start, end);
            }
        }

        /// <summary>
        /// 上季度
        /// </summary>
        public static Tuple<DateTime, DateTime> LastQuarter
        {
            get
            {
                var lastQuarter = DateTime.Today.AddMonths(-3);
                var start = lastQuarter.AddDays(1 - lastQuarter.Day).AddMonths(0 - (lastQuarter.Month - 1) % 3);
                var end = start.AddMonths(3).AddSeconds(-1);
                return new Tuple<DateTime, DateTime>(start, end);
            }
        }

        /// <summary>
        /// 今年
        /// </summary>
        public static Tuple<DateTime, DateTime> ThisYear
        {
            get
            {
                var today = DateTime.Today;
                var start = new DateTime(today.Year, 1, 1);
                var end = new DateTime(today.Year, 12, 31, 23, 59, 59);
                return new Tuple<DateTime, DateTime>(start, end);
            }
        }

        /// <summary>
        /// 去年
        /// </summary>
        public static Tuple<DateTime, DateTime> LastYear
        {
            get
            {
                var lastYear = DateTime.Today.AddYears(-1);
                var start = new DateTime(lastYear.Year, 1, 1);
                var end = new DateTime(lastYear.Year, 12, 31, 23, 59, 59);
                return new Tuple<DateTime, DateTime>(start, end);
            }
        }
    }
}
