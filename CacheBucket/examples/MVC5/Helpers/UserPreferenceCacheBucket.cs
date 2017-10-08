using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CB.Core;
using CB.InMemory;

namespace WebApplication.Helpers
{
    public class UserPreferenceCacheBucket:CacheBucket
    {
        public const string NAME = "UserPreference";

        public UserPreferenceCacheBucket(InMemoryCacheStorage cacheStorage) : base(NAME, cacheStorage)
        {
        }
    }
}