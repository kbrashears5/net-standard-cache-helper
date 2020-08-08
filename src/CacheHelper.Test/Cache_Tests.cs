using NetStandardTestHelper.Xunit;
using System;
using System.Collections.Generic;
using Xunit;

namespace CacheHelper.Test
{
    /// <summary>
    /// Test the <see cref="Cache"/> class
    /// </summary>
    public class Cache_Tests
    {
        #region Constructor

        /// <summary>
        /// Test that constructor throws for null cache names
        /// </summary>
        [Fact]
        public void Constructor_Null_CacheNames()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new Cache(cacheNames: null));

            TestHelper.AssertArgumentNullException(ex,
                "cacheNames");
        }

        /// <summary>
        /// Test that constructor throws for empty cache names list
        /// </summary>
        [Fact]
        public void Constructor_Empty_CacheNames()
        {
            var ex = Assert.Throws<ArgumentException>(() => new Cache(cacheNames: new List<string>()));

            TestHelper.AssertExceptionText(ex,
                CacheHelper.Text.ZeroCaches);
        }

        /// <summary>
        /// Test that constructor throws for duplicate names in cache names list
        /// </summary>
        [Fact]
        public void Constructor_DuplicateNames()
        {
            var list = new List<string>() { "name1", "name2", "name2" };

            var ex = Assert.Throws<ArgumentException>(() => new Cache(cacheNames: list));

            TestHelper.AssertExceptionText(ex,
                CacheHelper.Text.DuplicateCacheNames(list));
        }

        /// <summary>
        /// Test that constructor is created successfully
        /// </summary>
        [Fact]
        public void Constructor()
        {
            var cacheHelper = TestValues.Cache;

            Assert.NotNull(cacheHelper);
        }

        #endregion Constructor

        #region Add

        /// <summary>
        /// Test that <see cref="Cache.Add"/> throws for null cache name
        /// </summary>
        [Fact]
        public void Add_Null_Cache_Name()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => TestValues.Cache.Add(cacheName: NetStandardTestHelper.TestValues.StringEmpty,
                key: TestValues.Key,
                value: TestValues.Value));

            TestHelper.AssertArgumentNullException(ex,
                "cacheName");
        }

        /// <summary>
        /// Test that <see cref="Cache.Add"/> throws for null key
        /// </summary>
        [Fact]
        public void Add_Null_Key()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => TestValues.Cache.Add(cacheName: TestValues.CacheName,
                key: NetStandardTestHelper.TestValues.StringEmpty,
                value: TestValues.Value));

            TestHelper.AssertArgumentNullException(ex,
                "key");
        }

        /// <summary>
        /// Test that <see cref="Cache.Add"/> throws for null value
        /// </summary>
        [Fact]
        public void Add_Null_Value()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => TestValues.Cache.Add<object>(cacheName: TestValues.CacheName,
                key: TestValues.Key,
                value: null));

            TestHelper.AssertArgumentNullException(ex,
                "value");
        }

        /// <summary>
        /// Test that <see cref="Cache.Add"/> successfully adds to cache
        /// </summary>
        [Fact]
        public void Add()
        {
            TestValues.Cache.Add(cacheName: TestValues.CacheName,
                key: TestValues.Key,
                value: TestValues.Value);

            var cacheItem = TestValues.Cache.Get<string>(cacheName: TestValues.CacheName,
                key: TestValues.Key);

            Assert.Equal(TestValues.Value,
                cacheItem);
        }

        #endregion Add

        #region Get

        /// <summary>
        /// Test that <see cref="Cache.Get"/> throws for null cache name
        /// </summary>
        [Fact]
        public void Get_Null_Cache_Name()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => TestValues.Cache.Get<string>(cacheName: NetStandardTestHelper.TestValues.StringEmpty,
                key: TestValues.Key));

            TestHelper.AssertArgumentNullException(ex,
                "cacheName");
        }

        /// <summary>
        /// Test that <see cref="Cache.Get"/> throws for null key
        /// </summary>
        [Fact]
        public void Get_Null_Key()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => TestValues.Cache.Get<string>(cacheName: TestValues.CacheName,
                key: NetStandardTestHelper.TestValues.StringEmpty));

            TestHelper.AssertArgumentNullException(ex,
                "key");
        }

        /// <summary>
        /// Test that <see cref="Cache.Get"/> successfully gets from cache
        /// </summary>
        [Fact]
        public void Get()
        {
            TestValues.Cache.Add(cacheName: TestValues.CacheName,
                key: TestValues.Key,
                value: TestValues.Value);

            var cacheItem = TestValues.Cache.Get<string>(cacheName: TestValues.CacheName,
                key: TestValues.Key);

            Assert.Equal(TestValues.Value,
                cacheItem);
        }

        #endregion Get

        #region Delete

        /// <summary>
        /// Test that <see cref="Cache.Delete"/> throws for null cache name
        /// </summary>
        [Fact]
        public void Delete_Null_Cache_Name()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => TestValues.Cache.Delete(cacheName: NetStandardTestHelper.TestValues.StringEmpty,
                key: TestValues.Key));

            TestHelper.AssertArgumentNullException(ex,
                "cacheName");
        }

        /// <summary>
        /// Test that <see cref="Cache.Delete"/> throws for null key
        /// </summary>
        [Fact]
        public void Delete_Null_Key()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => TestValues.Cache.Delete(cacheName: TestValues.CacheName,
                key: NetStandardTestHelper.TestValues.StringEmpty));

            TestHelper.AssertArgumentNullException(ex,
                "key");
        }

        /// <summary>
        /// Test that <see cref="Cache.Delete"/> successfully deletes from cache
        /// </summary>
        [Fact]
        public void Delete()
        {
            TestValues.Cache.Add(cacheName: TestValues.CacheName,
                key: TestValues.Key,
                value: TestValues.Value);

            TestValues.Cache.Delete(cacheName: TestValues.CacheName,
                key: TestValues.Key);

            var cacheItem = TestValues.Cache.Get<string>(cacheName: TestValues.CacheName,
                key: TestValues.Key);

            Assert.Null(cacheItem);
        }

        #endregion Delete

        #region FindCache

        /// <summary>
        /// Test that <see cref="Cache.FindCache"/> throws for null cache name
        /// </summary>
        [Fact]
        public void FindCache_Null_CacheName()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => TestValues.Cache.FindCache(cacheName: NetStandardTestHelper.TestValues.StringEmpty));

            TestHelper.AssertArgumentNullException(ex,
                "cacheName");
        }

        /// <summary>
        /// Test that <see cref="Cache.FindCache"/> throws for not finding cache
        /// </summary>
        [Fact]
        public void FindCache_CacheNotFound()
        {
            var cache = new Cache(cacheNames: new List<string>() { "cache1", "cache2" });

            var badCacheName = "cache3";

            var ex = Assert.Throws<CacheNotFoundException>(() => TestValues.Cache.FindCache(cacheName: badCacheName));

            TestHelper.AssertExceptionText(ex,
                CacheHelper.Text.CacheNotFound(cacheName: badCacheName));
        }

        /// <summary>
        /// Test that <see cref="Cache.FindCache"/> finds cache
        /// </summary>
        [Fact]
        public void FindCache()
        {
            var cache = TestValues.Cache.FindCache(cacheName: TestValues.CacheName);

            Assert.NotNull(cache);
        }

        #endregion FindCache

        #region InitializeCaches

        /// <summary>
        /// Test that <see cref="Cache.InitializeCaches"/> throws for null cache names
        /// </summary>
        [Fact]
        public void InitializeCaches_Null_CacheNames()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => TestValues.Cache.InitializeCaches(cacheNames: null));

            TestHelper.AssertArgumentNullException(ex,
                "cacheNames");
        }

        #endregion InitializeCaches

        #region Replace

        /// <summary>
        /// Test that <see cref="Cache.Replace"/> throws for null cache name
        /// </summary>
        [Fact]
        public void Replace_Null_Cache_Name()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => TestValues.Cache.Replace(cacheName: NetStandardTestHelper.TestValues.StringEmpty,
                key: TestValues.Key,
                value: TestValues.Value));

            TestHelper.AssertArgumentNullException(ex,
                "cacheName");
        }

        /// <summary>
        /// Test that <see cref="Cache.Replace"/> throws for null key
        /// </summary>
        [Fact]
        public void Replace_Null_Key()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => TestValues.Cache.Replace(cacheName: TestValues.CacheName,
                key: NetStandardTestHelper.TestValues.StringEmpty,
                value: TestValues.Value));

            TestHelper.AssertArgumentNullException(ex,
                "key");
        }

        /// <summary>
        /// Test that <see cref="Cache.Replace"/> throws for null value
        /// </summary>
        [Fact]
        public void Replace_Null_Value()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => TestValues.Cache.Replace<object>(cacheName: TestValues.CacheName,
                key: TestValues.Key,
                value: null));

            TestHelper.AssertArgumentNullException(ex,
                "value");
        }

        /// <summary>
        /// Test that <see cref="Cache.Replace"/> successfully updates a cache item
        /// </summary>
        [Fact]
        public void Replace()
        {
            TestValues.Cache.Add(cacheName: TestValues.CacheName,
                key: TestValues.Key,
                value: TestValues.Value);

            var newValue = 17;

            TestValues.Cache.Replace(cacheName: TestValues.CacheName,
                key: TestValues.Key,
                value: newValue);

            var value = TestValues.Cache.Get<int>(cacheName: TestValues.CacheName,
                key: TestValues.Key);

            Assert.Equal(value,
                newValue);
        }

        #endregion Replace
    }
}