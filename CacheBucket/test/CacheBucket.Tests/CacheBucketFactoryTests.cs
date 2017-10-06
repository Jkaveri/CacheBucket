#region

using CB.Factory;
using CB.InMemory;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

#endregion

namespace CB.Tests
{
    public class CacheBucketFactoryTests
    {
        private const string USERPREFERENCE = "UserPreference";

        private IServiceCollection SetupServiceCollection()
        {
            var services = new ServiceCollection();
            services.AddCacheBucket()
                .AddStorage<InMemoryCacheStorage>()
                .AddBucket(USERPREFERENCE, typeof(InMemoryCacheStorage));
            return services;
        }

        [Fact]
        public void should_success_when_create_cache_bucket()
        {
            // Arrange
            var services = SetupServiceCollection();
            var sp = services.BuildServiceProvider();
            var bucketFactory = sp.GetService<CacheBucketFactory>();

            // Action
            var bucket = bucketFactory.Create(USERPREFERENCE);

            bucket.SetValue("InMemoryValue", "inmemory value");

            var value = bucket.GetValue("InMemoryValue");

            // Assertions
            value.Should().Be("inmemory value");
        }

        [Fact]
        public void should_return_value_when_access_to_the_inner_bucket()
        {
            // Arrange
            var services = SetupServiceCollection();
            var sp = services.BuildServiceProvider();
            var bucketFactory = sp.GetService<CacheBucketFactory>();

            // Action
            var bucket = bucketFactory.Create(USERPREFERENCE);
            bucket.In("User1").SetValue("A", "B");

            var innerBucket = bucketFactory.Create($"{USERPREFERENCE}:User1");

            var value = innerBucket.GetValue("A");

            // Assertions
            value.Should().Be("B");
        }
    }
}