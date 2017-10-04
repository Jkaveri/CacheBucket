#region

using System;
using CB.Core;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace CB.Factory
{
    public static class ServiceCollectionExtensions
    {
        public static CacheBucketFluentConfig AddCacheBucket(this IServiceCollection services)
        {
            services.AddSingleton<CacheBucketFactory>();
            return new CacheBucketFluentConfig(services);
        }
    }

    public class CacheBucketFluentConfig
    {
        private readonly IServiceCollection _services;

        public CacheBucketFluentConfig(IServiceCollection services)
        {
            _services = services;
        }

        public CacheBucketFluentConfig AddBucket<TBucket>(CacheBucketOptions options) where TBucket : CacheBucket
        {
            _services.AddSingleton<TBucket>();
            return AddBucket(options);
        }

        public CacheBucketFluentConfig AddBucket(string bucketName, Type storageType)
        {
            return AddBucket(new CacheBucketOptions(bucketName, storageType));
        }

        public CacheBucketFluentConfig AddBucket(CacheBucketOptions options)
        {
            CacheStorageMapping.Instance.AddMapping(options.Name, options.StorageType);
            return this;
        }

        public CacheBucketFluentConfig AddStorage<TStorage>() where TStorage : class, ICacheStorage
        {
            _services.AddSingleton<TStorage>();
            return this;
        }
    }

    public class CacheBucketOptions
    {
        public CacheBucketOptions([NotNull] string name, [NotNull] Type storageType)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            StorageType = storageType ?? throw new ArgumentNullException(nameof(storageType));
        }

        public string Name { get; set; }
        public Type StorageType { get; set; }
    }
}