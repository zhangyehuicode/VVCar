using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YEF.Core.Caching
{
    /// <summary>
    /// 提供缓存操作
    /// </summary>
    public interface ICache
    {
        /// <summary>
        /// 获取缓存中的指定缓存项
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <returns>缓存项</returns>
        object Get(string key);

        /// <summary>
        /// 获取缓存中的指定缓存项
        /// </summary>
        /// <typeparam name="TValue">缓存项数据类型</typeparam>
        /// <param name="key">缓存键</param>
        /// <returns>缓存项</returns>
        TValue Get<TValue>(string key);

        /// <summary>
        /// 向缓存中插入缓存项
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存数据</param>
        void Set(string key, object value);

        /// <summary>
        /// 向缓存中插入缓存项，同时指定基于时间间隔的浮动过期信息
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存数据</param>
        /// <param name="slidingExpiration">浮动过期时间，在此时间内访问缓存，缓存将继续有效</param>
        void Set(string key, object value, TimeSpan slidingExpiration);

        /// <summary>
        /// 向缓存中插入缓存项，同时指定基于时间的过期详细信息
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存数据</param>
        /// <param name="absoluteExpiration">缓存项的固定的过期日期和时间</param>
        void Set(string key, object value, DateTimeOffset absoluteExpiration);

        /// <summary>
        /// 从缓存中移除缓存项
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <returns>缓存项</returns>
        object Remove(string key);
    }
}
