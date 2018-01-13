using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YEF.Core
{
    /// <summary>
    /// 类型<see cref="System.Int32"/>扩展方法类
    /// </summary>
    public static class Int32Extensions
    {
        /// <summary>
        /// 返回一个新字符串，该字符串通过在此实例中的字符左侧填充指定的 Unicode 字符来达到指定的总长度，从而使这些字符右对齐。
        /// </summary>
        /// <param name="source"></param>
        /// <param name="totalWidth"></param>
        /// <param name="paddingChar"></param>
        /// <returns></returns>
        public static String PadLeft(this Int32 source, int totalWidth, char paddingChar)
        {
            return source.ToString().PadLeft(totalWidth, paddingChar);
        }

        /// <summary>
        /// 返回一个新字符串，该字符串通过在此字符串中的字符右侧填充指定的 Unicode 字符来达到指定的总长度，从而使这些字符左对齐。
        /// </summary>
        /// <param name="source"></param>
        /// <param name="totalWidth"></param>
        /// <param name="paddingChar"></param>
        /// <returns></returns>
        public static String PadRight(this Int32 source, int totalWidth, char paddingChar)
        {
            return source.ToString().PadRight(totalWidth, paddingChar);
        }
    }
}
