using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YEF.Core
{
    /// <summary>
    /// DateTime 扩展类
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// 将当前DateTime值转换成yyyy-MM-dd字符串。
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public static string ToDateString(this DateTime datetime)
        {
            return datetime.ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// 将当前DateTime值转换成yyyy-MM-dd HH:mm:ss字符串。
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public static string ToDateTimeString(this DateTime datetime)
        {
            return datetime.ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// 将当前DateTime值星期几中文字符串。
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public static string ToDayOfWeekString(this DateTime datetime)
        {
            return datetime.ToString("dddd", new System.Globalization.CultureInfo("zh-CN"));
        }
    }
}
