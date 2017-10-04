#region

using System;
using System.Collections.Concurrent;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace CB.Core
{
    public static class ServiceCollectionExtensions
    {
        public static CacheBucketFluentConfiguration<TBucket> AddBucket<TBucket>(this IServiceCollection services)
            where TBucket : CacheBucket
        {
            return new CacheBucketFluentConfiguration<TBucket>(services);
        }
    }

    public class CacheBucketFluentConfiguration<TBucket> where TBucket : CacheBucket
    {
        private readonly IServiceCollection _services;

        public CacheBucketFluentConfiguration(IServiceCollection services)
        {
            _services = services;
            services.AddSingleton<TBucket>();
        }

        public void AddStorage<TStorage>() where TStorage : class, ICacheStorage
        {
            _services.AddSingleton<TStorage>();
        }
    }
}