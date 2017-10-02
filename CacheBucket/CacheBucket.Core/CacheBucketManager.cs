#region

using System;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace CB.Core
{
    public class CacheBucketManager
    {
        public static CacheBucketFluentRegister With(Func<ICacheStorage> factory)
        {
            return new CacheBucketFluentRegister(factory);
        }

        public static CacheBucketFluentRegister With<TCacheStorage>() where TCacheStorage : ICacheStorage, new()
        {
            return new CacheBucketFluentRegister(() => Activator.CreateInstance<TCacheStorage>());
        }

        public static void AddCacheStorageFactory<T>(T factory) where T: ICacheStorageFactory
        {
            CacheStorageManager.Instance.AddStorageFactory(factory);
        }
    }

    public class CacheBucketFluentRegister
    {
        private readonly Func<ICacheStorage> _factory;

        internal CacheBucketFluentRegister(Func<ICacheStorage> factory)
        {
            _factory = factory;
        }

        public CacheBucketFluentRegister Register(string bucketName)
        {
            CacheStorageManager.Instance.AddStorageFactory(new MatchedNameStorageFactory(bucketName, _factory));

            return this;
        }
    }
}