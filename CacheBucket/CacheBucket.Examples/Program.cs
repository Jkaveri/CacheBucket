#region

using System;
using CB.Core;
using CB.InMemory;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace CB.Examples
{
    public class A
    {
    }

    public class B
    {
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            var services = new ServiceCollection();

            services.AddBucket<UserPreferenceCacheBucket>()
                .AddStorage<InMemoryCacheStorage>();


            var sp = services.BuildServiceProvider();

            var bucket = sp.GetService<UserPreferenceCacheBucket>();

            bucket.SetValue("H", "O");
            Console.WriteLine(bucket.GetValue("H"));
            Console.Read();
        }
    }
}