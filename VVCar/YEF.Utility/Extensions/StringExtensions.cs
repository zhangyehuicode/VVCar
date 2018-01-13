using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    /// <summary>
    /// String 拓展类
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// 使用 decimal.TryParse 转换为等效的 decimal 类型
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static decimal ToDecimal(this string source)
        {
            if (string.IsNullOrEmpty(source))
                return 0;
            decimal result = 0;
            decimal.TryParse(source, out result);
            return result;
        }

        /// <summary>
        /// 使用 int.TryParse转换为等效的 int 类型
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static int ToInt32(this string source)
        {
            if (string.IsNullOrEmpty(source))
                return 0;
            int result = 0;
            int.TryParse(source, out result);
            return result;
        }

        /// <summary>
        /// 使用 double.TryParse 转换为等效的 double 类型
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static double ToDouble(this string source)
        {
            if (string.IsNullOrEmpty(source))
                return 0;
            double result = 0;
            double.TryParse(source, out result);
            return result;
        }
    }
}
