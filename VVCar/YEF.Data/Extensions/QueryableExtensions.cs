using System;
using System.Linq;

namespace YEF.Data
{
    public static class QueryableExtensions
    {
        /// <summary>
        /// 返回一个新查询，其中返回的实体将不会在 System.Data.Entity.DbContext 或 System.Data.Entity.Core.Objects.ObjectContext
        /// 中进行缓存。此方法通过调用基础查询对象的 AsNoTracking 方法来运行。如果基础查询对象没有 AsNoTracking 方法，则调用此方法将不会有任何影响。
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="source">源查询</param>
        /// <returns>应用 NoTracking 的新查询，如果不支持 NoTracking，则为源查询</returns>
        public static IQueryable<T> NoTracking<T>(this IQueryable<T> source) where T : class
        {
            return System.Data.Entity.QueryableExtensions.AsNoTracking<T>(source);
        }

        /// <summary>
        /// 返回一个新查询，其中返回的实体将不会在 System.Data.Entity.DbContext 或 System.Data.Entity.Core.Objects.ObjectContext
        /// 中进行缓存。此方法通过调用基础查询对象的 AsNoTracking 方法来运行。如果基础查询对象没有 AsNoTracking 方法，则调用此方法将不会有任何影响。
        /// </summary>
        /// <param name="source">源查询</param>
        /// <returns>应用 NoTracking 的新查询，如果不支持 NoTracking，则为源查询</returns>
        public static IQueryable NoTracking(this IQueryable source)
        {
            return System.Data.Entity.QueryableExtensions.AsNoTracking(source);
        }
    }
}
