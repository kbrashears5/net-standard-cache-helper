using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;

namespace CacheHelper
{
    /// <summary>
    /// Mock implementation of <see cref="ICache"/>
    /// </summary>
    public class Cache_Mock : ICache
    {
        /// <summary>
        /// Create new instance of <see cref="Cache_Mock"/>
        /// </summary>
        public Cache_Mock()
        {
        }

        #region IDisposable

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            return;
        }

        #endregion IDisposable

#pragma warning disable CS1591

        public void Add<T>(string cacheName,
            string key,
            T value)
        {
            return;
        }

        public void Delete(string cacheName,
            string key)
        {
            return;
        }

        public MemoryCache FindCache(string cacheName)
        {
            return null;
        }

        public T Get<T>(string cacheName,
            string key)
        {
            return Activator.CreateInstance<T>();
        }

        public void InitializeCaches(IEnumerable<string> cacheNames)
        {
            return;
        }

        public void Replace<T>(string cacheName,
            string key,
            T value)
        {
            return;
        }

#pragma warning restore CS1591
    }
}