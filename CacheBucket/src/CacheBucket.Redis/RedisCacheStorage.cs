using System;
using CB.Core;
using ServiceStack.Redis;

namespace CacheBucket.Redis
{
    public class RedisCacheStorage : ICacheStorage
    {
        private readonly IRedisClientsManager _clientManager;

        public RedisCacheStorage(IRedisClientsManager clientManager)
        {
            if (clientManager == null)
            {
                throw new ArgumentNullException(nameof(clientManager));
            }

            _clientManager = clientManager;
        }
        public string Get(string key)
        {
            using (IRedisClient client = _clientManager.GetClient())
            {
                return client.GetValue(key);
            }
        }

        public void Remove(string key)
        {
            using (IRedisClient client = _clientManager.GetClient())
            {
                client.Remove(key);
            }
        }

        public void Set(string key, string value)
        {
            using (IRedisClient client = _clientManager.GetClient())
            {
                client.SetValue(key, value);
            }
        }
    }
}
