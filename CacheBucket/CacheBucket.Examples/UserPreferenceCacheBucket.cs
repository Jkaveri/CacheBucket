using CB.Core;
using CB.InMemory;

namespace CB.Examples
{
    public class UserPreferenceCacheBucket:CacheBucket
    {
        public UserPreferenceCacheBucket(InMemoryCacheStorage cacheStorage) : base("UserPreference", cacheStorage)
        {
        }
    }
}