#region

using System;
using CB.Core;
using CB.InMemory;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace CB.Examples
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var services = new ServiceCollection();
            services.AddSingleton<InMemoryCacheStorage>();
            var sp = services.BuildServiceProvider();
           
            CacheBucketManager.With(() => sp.GetService<InMemoryCacheStorage>())
                .Register("UserPreference");


            const string userId = "123";
            var cacheBucket = new UserPreferenceCacheBucket();

            var userBucket = cacheBucket.In(userId);
            userBucket.SetValue("henry", "value");

            Console.WriteLine($"Hello World! {userBucket.GetValue("henry")}");
            Console.Read();
        }
    }
}