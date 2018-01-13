using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YEF.Utility
{
    public static class MathUtil
    {
        /// <summary>
        /// 取整,当前模式为向上取整
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static decimal GetIntValue(decimal value)
        {
            return Math.Ceiling(value);
        }
    }
}
