using System;
using System.Collections.Generic;

namespace CacheHelper
{
    /// <summary>
    /// Static text for the <see cref="CacheHelper"/> namespace
    /// </summary>
    public static class Text
    {
#pragma warning disable CS1591
        public static string Name { get; } = nameof(CacheHelper);

        public static string ZeroCaches { get; } = "Zero caches were provided";

        public static string ZeroTimeout { get; } = "Timeout cannot be 0";

        public static string DuplicateCacheNames(IEnumerable<string> cacheNames)
        {
            if (cacheNames == null) throw new ArgumentNullException(nameof(cacheNames));

            return $"Duplicate cache names were provided: [{string.Join(",", cacheNames)}]";
        }

        public static string CacheNotFound(string cacheName)
        {
            if (string.IsNullOrWhiteSpace(cacheName)) throw new ArgumentNullException(nameof(cacheName));

            return $"Cache not found: [{cacheName}]";
        }

#pragma warning restore CS1591
    }
}