using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CacheHelper
{
    /// <summary>
    /// Implementation of <see cref="ICache"/>
    /// </summary>
    public class Cache : ICache
    {
        /// <summary>
        /// List of caches
        /// </summary>
        private IDictionary<string, MemoryCache> Caches { get; }

        /// <summary>
        /// Timeout for cache items
        /// </summary>
        private DateTimeOffset Timeout { get; }

        /// <summary>
        /// Create new instance of <see cref="Cache"/>
        /// </summary>
        /// <param name="cacheNames"></param>
        /// <param name="cacheExpirationSeconds"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public Cache(IEnumerable<string> cacheNames,
            int cacheExpirationSeconds = 300)
        {
            if (cacheNames == null) throw new ArgumentNullException(nameof(cacheNames));
            if (cacheNames.Count() < 1) throw new ArgumentException(Text.ZeroCaches);
            if (cacheNames.Count() != cacheNames.Distinct().Count()) throw new ArgumentException(Text.DuplicateCacheNames(cacheNames));
            if (cacheExpirationSeconds == 0) throw new ArgumentException(Text.ZeroTimeout);

            this.Caches = new Dictionary<string, MemoryCache>();

            this.Timeout = DateTimeOffset.Now.AddSeconds(seconds: cacheExpirationSeconds);

            this.InitializeCaches(cacheNames: cacheNames);
        }

        #region IDisposable

        /// <summary>
        /// Disposed
        /// </summary>
        private bool Disposed { get; set; } = false;

        /// <summary>
        /// Dispose
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.Disposed)
            {
                if (disposing)
                {
                    if (this.Caches != null)
                    {
                        foreach (var cache in this.Caches)
                        {
                            cache.Value.Dispose();
                        }

                        this.Caches.Clear();
                    }
                }

                this.Disposed = true;
            }
        }

        /// <summary>
        /// Finalizer
        /// </summary>
        ~Cache() => this.Dispose(disposing: false);

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            this.Dispose(disposing: true);

            GC.SuppressFinalize(this);
        }

        #endregion IDisposable

#pragma warning disable CS1591

        public void Add<T>(string cacheName,
            string key,
            T value)
        {
            if (string.IsNullOrWhiteSpace(cacheName)) throw new ArgumentNullException(nameof(cacheName));
            if (string.IsNullOrWhiteSpace(key)) throw new ArgumentNullException(nameof(key));
            if (value == null) throw new ArgumentNullException(nameof(value));

            var cache = this.FindCache(cacheName: cacheName);

            _ = cache.CreateEntry(key: key);

            _ = cache.Set<T>(key: key,
                value: value,
                absoluteExpiration: this.Timeout);
        }

        public void Delete(string cacheName,
            string key)
        {
            if (string.IsNullOrWhiteSpace(cacheName)) throw new ArgumentNullException(nameof(cacheName));
            if (string.IsNullOrWhiteSpace(key)) throw new ArgumentNullException(nameof(key));

            var cache = this.FindCache(cacheName: cacheName);

            cache.Remove(key: key);
        }

        public MemoryCache FindCache(string cacheName)
        {
            if (string.IsNullOrWhiteSpace(cacheName)) throw new ArgumentNullException(nameof(cacheName));

            var cache = this.Caches.Where(c => c.Key == cacheName).FirstOrDefault();

            if (default(KeyValuePair<string, MemoryCache>).Equals(cache)) throw new CacheNotFoundException(cacheName: cacheName);

            return cache.Value;
        }

        public T Get<T>(string cacheName,
            string key)
        {
            if (string.IsNullOrWhiteSpace(cacheName)) throw new ArgumentNullException(nameof(cacheName));
            if (string.IsNullOrWhiteSpace(key)) throw new ArgumentNullException(nameof(key));

            var cache = this.FindCache(cacheName: cacheName);

            _ = cache.TryGetValue<T>(key: key,
                value: out var value);

            return value;
        }

        public void InitializeCaches(IEnumerable<string> cacheNames)
        {
            if (cacheNames == null) throw new ArgumentNullException(nameof(cacheNames));

            this.Caches.Clear();

            foreach (var cacheName in cacheNames)
            {
                this.Caches.Add(key: cacheName,
                    value: new MemoryCache(optionsAccessor: new MemoryCacheOptions()));
            }
        }

        public void Replace<T>(string cacheName,
            string key,
            T value)
        {
            if (string.IsNullOrWhiteSpace(cacheName)) throw new ArgumentNullException(nameof(cacheName));
            if (string.IsNullOrWhiteSpace(key)) throw new ArgumentNullException(nameof(key));
            if (value == null) throw new ArgumentNullException(nameof(value));

            this.Add<T>(cacheName: cacheName,
                key: key,
                value: value);
        }

#pragma warning restore CS1591
    }
}