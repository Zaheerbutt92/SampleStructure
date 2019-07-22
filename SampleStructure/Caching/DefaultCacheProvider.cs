using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace Caching
{
    /// <summary>
    /// Class DefaultCacheProvider.
    /// </summary>
    public class DefaultCacheProvider : ICacheProvider
    {

        /// <summary>
        /// Gets the cache.
        /// </summary>
        /// <value>The cache.</value>
        private ObjectCache Cache => MemoryCache.Default;

        /// <summary>
        /// Gets the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>System.Object.</returns>
        public object Get(string key)
        {
            return Cache[key];
        }

        /// <summary>
        /// Sets the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="data">The data.</param>
        public void Set(string key, object data)
        {
            var policy = new CacheItemPolicy
            {
                //if between midnight and 5 AM, expire at 5 AM today. Else expire cache tomorrow at 5 AM.
                AbsoluteExpiration = DateTime.Now.Hour < 5 ? DateTime.Today.AddHours(5) : DateTime.Today.AddDays(1).AddHours(5)
            };
            //if between midnight and 5 AM, expire at 5 AM today. Else expire cache tomorrow at 5 AM.
            Cache.Add(new CacheItem(key, data), policy);
        }

        /// <summary>
        /// Sets the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="data">The data.</param>
        /// <param name="minutes">The minutes.</param>
        public void Set(string key, object data, int minutes)
        {
            var policy = new CacheItemPolicy
            {
                AbsoluteExpiration = DateTime.Now + TimeSpan.FromMinutes(minutes)
            };
            Cache.Add(new CacheItem(key, data), policy);
        }

        /// <summary>
        /// Determines whether the specified key is set.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns><c>true</c> if the specified key is set; otherwise, <c>false</c>.</returns>
        public bool IsSet(string key)
        {
            return (Cache[key] != null);
        }

        /// <summary>
        /// Invalidates the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        public void Invalidate(string key)
        {
            Cache.Remove(key);
        }
    }
}
