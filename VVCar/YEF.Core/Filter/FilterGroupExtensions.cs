using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace YEF.Core.Filter
{
    /// <summary>
    /// 类型<see cref="YEF.Core.Filter.FilterGroup"/>扩展方法类
    /// </summary>
    public static class FilterGroupExtensions
    {
        public static Expression<Func<T, bool>> BuildExpression<T>(this FilterGroup group)
        {
            return FilterHelper.GetExpression<T>(group);
        }
    }
}
