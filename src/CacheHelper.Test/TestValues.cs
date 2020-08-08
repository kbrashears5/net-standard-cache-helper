using System.Collections.Generic;

namespace CacheHelper.Test
{
    /// <summary>
    /// Test values for the <see cref="CacheHelper.Test"/> namespace
    /// </summary>
    internal static class TestValues
    {
#pragma warning disable CS1591

        internal static string CacheName { get; } = nameof(CacheName);

        internal static string Key { get; } = nameof(Key);

        internal static object Value { get; } = nameof(Value);

        internal static IEnumerable<string> CacheNames { get; } = new List<string> { CacheName };

        internal static ICache Cache { get; } = new Cache(CacheNames);

#pragma warning restore CS1591
    }
}