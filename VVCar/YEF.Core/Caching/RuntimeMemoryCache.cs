using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace YEF.Core.Caching
{
    public class RuntimeMemoryCache : ICache
    {
        private readonly ObjectCache _cache;

        public RuntimeMemoryCache()
        {
            this._cache = MemoryCache.Default;
        }

        #region ICache 成员

        public object Get(string key)
        {
            return _cache.Get(key);
        }

        public TValue Get<TValue>(string key)
        {
            var value = this.Get(key);
            if (value == null)
                return default(TValue);
            return (TValue)value;
        }

        public void Set(string key, object value)
        {
            CacheItemPolicy policy = new CacheItemPolicy();
            _cache.Set(key, value, policy);
        }

        public void Set(string key, object value, TimeSpan slidingExpiration)
        {
            _cache.Set(key, value, new CacheItemPolicy { SlidingExpiration = slidingExpiration });
        }

        public void Set(string key, object value, DateTimeOffset absoluteExpiration)
        {
            _cache.Set(key, value, absoluteExpiration);
        }

        public object Remove(string key)
        {
            return _cache.Remove(key);
        }

        #endregion
    }
}
