using System;

namespace CacheHelper
{
    /// <summary>
    /// Cache was not found
    /// </summary>
    public class CacheNotFoundException : Exception
    {
        /// <summary>
        /// Create new instance of <see cref="CacheNotFoundException"/>
        /// </summary>
        /// <param name="cacheName"></param>
        public CacheNotFoundException(string cacheName)
            : base(message: Text.CacheNotFound(cacheName: cacheName))
        {
        }
    }
}