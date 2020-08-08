using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;

namespace CacheHelper
{
    /// <summary>
    /// Functions to interact with caches
    /// </summary>
    public interface ICache : IDisposable
    {
        /// <summary>
        /// Add item to given cache
        /// </summary>
        /// <param name="cacheName"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <exception cref="ArgumentNullException"></exception>
        void Add<T>(string cacheName,
            string key,
            T value);

        /// <summary>
        /// Delete item from given cache
        /// </summary>
        /// <param name="cacheName"></param>
        /// <param name="key"></param>
        /// <exception cref="ArgumentNullException"></exception>
        void Delete(string cacheName,
            string key);

        /// <summary>
        /// Find given cache
        /// </summary>
        /// <param name="cacheName"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="CacheNotFoundException"></exception>
        MemoryCache FindCache(string cacheName);

        /// <summary>
        /// Get item from given cache
        /// </summary>
        /// <param name="cacheName"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        T Get<T>(string cacheName,
            string key);

        /// <summary>
        /// Initialize caches
        /// </summary>
        /// <param name="cacheNames"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exception"></exception>
        void InitializeCaches(IEnumerable<string> cacheNames);

        /// <summary>
        /// Replace item in a given cache
        /// </summary>
        /// <param name="cacheName"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <exception cref="ArgumentNullException"></exception>
        void Replace<T>(string cacheName,
            string key,
            T value);
    }
}