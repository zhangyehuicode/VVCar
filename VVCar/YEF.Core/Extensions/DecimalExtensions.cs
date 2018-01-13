using System;

namespace YEF.Core
{
    /// <summary>
    /// 类型<see cref="System.Decimal"/>扩展方法类
    /// </summary>
    public static class DecimalExtensions
    {
        /// <summary>
        /// 转换成 0.# 显示格式
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public static string ToQuantityString(this Decimal source)
        {
            return source.ToString("0.#");
        }

        /// <summary>
        /// 转换成 0.## 显示格式
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public static string ToPriceString(this Decimal source)
        {
            return source.ToString("0.##");
        }
    }
}
