using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YEF.Core
{
    /// <summary>
    /// Linq 拓展方法类
    /// </summary>
    public static class LinqExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            Contract.Requires(action != null);
            foreach (var element in source)
                action(element);
        }
    }
}
